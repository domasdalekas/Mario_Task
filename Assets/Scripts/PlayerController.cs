using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float maxHorizontalSpeed = 12f;
    public float maxVerticalSpeed = 16f;
    public float movementForce = 50f;
    public float jumpVelocity = 15f;
    public float jumpTime = 0.5f;

    [Header("Rigidbody")]
    public float mass = 0.75f;
    public float linearDrag = 1.5f;
    public float gravityScale = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;

    [Header("Animators")]
    public RuntimeAnimatorController bigMarioAnimatorController;
    public RuntimeAnimatorController smallMarioAnimatorController;
    public RuntimeAnimatorController fireMario;

    [Header("Death Mechanics")]
    public float invulnerabilityTime = 2f;
    public float deathHeight = -10f;

    [Header("Sounds")]
    public AudioClip smallJumpSound;
    public AudioClip bigJumpSound;
    public AudioClip levelUpSound;
    public AudioClip deathSound;
    public AudioClip levelDownSound;
    public AudioClip additionalLifeSound;


    private AudioSource audioSource;

    private bool isFacingRight = true;
    private bool isTouchingGround = true;

    [HideInInspector]
    public bool isJumping = false;

    [HideInInspector]
    public bool poweredUp = false;

    [HideInInspector]
    public bool isDead = false;

    [HideInInspector]
    public bool isInvulnerable = false;

    private float movementInput = 0f;
    private float jumpTimeCounter = 0f;
    private float invulnerabilityTimer = 0f;

    private Rigidbody2D playerRigidbody2D;

    private CapsuleCollider2D playerCapsuleCollider2D;

    private Animator playerAnimator;

    private SpriteRenderer playerSpriteRenderer;

    public  PlayableDirector timeline;

    private PlayerController instance = null;
    

    public float groundPosition;

    public bool takeAwayControll = false; //taking away control so Mario would not stick to the side
    public bool isGameFinished = false;
    public bool playTimeline = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D.mass = mass;
        playerRigidbody2D.drag = linearDrag;
        playerRigidbody2D.gravityScale = gravityScale;
        playerCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    void FixedUpdate()
    {
      
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, new Vector2(0.4f, 0.1f), 0f, Vector2.down, groundCheckRadius, groundMask); //using this for a bigger and more accurate ground check
        isTouchingGround = (hit.collider != null) ? true : false;

        movementInput = Input.GetAxis("Horizontal");
       
        CheckIfStuck(); //Checks if Mario is trying to walk into the wall and get stuck
        if (!isDead)
        {
            if ((playerRigidbody2D.velocity.x > 0 && !isFacingRight) || (playerRigidbody2D.velocity.x < 0 && isFacingRight))
            {
                playerAnimator.SetBool("turning", true);
            }
            else
            {
                playerAnimator.SetBool("turning", false);
            }

            float movementForceMultiplier = Mathf.Max(maxHorizontalSpeed - Mathf.Abs(playerRigidbody2D.velocity.x), 1);

            playerRigidbody2D.AddForce(new Vector2(movementInput * movementForce * movementForceMultiplier, 0));

            playerRigidbody2D.velocity = new Vector2(Mathf.Clamp(playerRigidbody2D.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed), Mathf.Clamp(playerRigidbody2D.velocity.y, -maxVerticalSpeed, maxVerticalSpeed));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTouchingGround)
                {
                    //Play Jump sound
                    if (!poweredUp)
                        audioSource.PlayOneShot(smallJumpSound);
                    else
                        audioSource.PlayOneShot(bigJumpSound);

                    isJumping = true;
                    playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpVelocity);
                    jumpTimeCounter = jumpTime;
                }
            }


            if (jumpTimeCounter > 0 && isJumping)
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpTimeCounter -= Time.deltaTime;
                    {
                        playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpVelocity);
                    }
                }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
                jumpTimeCounter = 0;
            }

           
            playerAnimator.SetFloat("movementSpeed", Mathf.Abs(playerRigidbody2D.velocity.x));
            playerAnimator.SetBool("touchingGround", isTouchingGround);
        }

        if (movementInput > 0 && !isFacingRight)
        {
            FlipSprite();
        }
        else if (movementInput < 0 && isFacingRight)
        {
            FlipSprite();
        }
        if (transform.position.x <118f && transform.position.y <-1.5f && isGameFinished == true)
        {
            playTimeline = true; 
        }
        if (transform.position.x < 124f && isGameFinished == true && playTimeline==true)
        {
            
          StartCoroutine(MovePlayerTowardsCastle());
            
        }
    }


    private void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;

            float newAlpha = 1f;

            if (playerSpriteRenderer.color.a > 0.51f)
            {
                newAlpha = 0.5f;
            }


            if (invulnerabilityTimer < 0)
            {
                isInvulnerable = false;
                newAlpha = 1f;
            }


            playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r, playerSpriteRenderer.color.g, playerSpriteRenderer.color.b, newAlpha);

            if (playerRigidbody2D.position.y < deathHeight)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    public void OnGameFinished()
    {
        playerAnimator.SetBool("gameFinished", true);
        isGameFinished = true;
    }
    public void TimelinePlay()
    {
        if(playTimeline==true)
        {
            playerAnimator.SetBool("touchingGround", true);
            playerRigidbody2D.position = new Vector2(playerRigidbody2D.position.x + 1f, playerRigidbody2D.position.y);
            FlipSprite();
            playTimeline = false;
            
            
        }
    }
    public void PowerUp()
    {
        if (!poweredUp && playerAnimator.runtimeAnimatorController == smallMarioAnimatorController as RuntimeAnimatorController)
        {
            playerAnimator.runtimeAnimatorController = bigMarioAnimatorController as RuntimeAnimatorController;
            playerCapsuleCollider2D.offset = new Vector2(0, 0.5f);
            playerCapsuleCollider2D.size = new Vector2(0.9f, 2);
            poweredUp = true;
            audioSource.PlayOneShot(levelUpSound);
        }
        else if (poweredUp && playerAnimator.runtimeAnimatorController == bigMarioAnimatorController as RuntimeAnimatorController)
        {
            playerAnimator.runtimeAnimatorController = fireMario as RuntimeAnimatorController;
            audioSource.PlayOneShot(levelUpSound);
        }
        

    }
    public void OneLifeUp()
    {
        Debug.Log("1UP");
        audioSource.PlayOneShot(additionalLifeSound);
    }
    public void Die()
    {
        if (poweredUp && !isDead && !isInvulnerable)
        {
            audioSource.PlayOneShot(levelDownSound);
            playerAnimator.runtimeAnimatorController = smallMarioAnimatorController as RuntimeAnimatorController;
            playerCapsuleCollider2D.offset = new Vector2(0, 0f);
            playerCapsuleCollider2D.size = new Vector2(1, 1);
            poweredUp = false;
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityTime;
        }
        else if (!isInvulnerable)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(deathSound);
            playerRigidbody2D.velocity = new Vector2(0, jumpVelocity);
            playerAnimator.SetBool("dead", true);
            playerCapsuleCollider2D.enabled = false;
            isDead = true;
            FindObjectOfType<GameManager>().EndGame();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "FlagPole")
        {
            StartCoroutine(MovePlayerDownPoleEnum());
        }
    }
    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    private void CheckIfStuck()
    {
        //Taking away users control when player is not touching the ground and not moving to any direction
        if (!isTouchingGround && playerRigidbody2D.velocity == Vector2.zero)
            takeAwayControll = true;

        if (takeAwayControll)
            movementInput = 0;

        //if starts touching ground - give control back
        if (isTouchingGround)
            takeAwayControll = false;
    }
    public float GetGroundPosition()
    {
       return groundPosition = playerRigidbody2D.position.y;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        takeAwayControll = false; //give back control when it's no longer colliding with anything
    }
    

    IEnumerator MovePlayerDownPoleEnum()
    {
        playerRigidbody2D.isKinematic = true;
        while (transform.position.y >-1.3f)
        {
           
           transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f);
           yield return new WaitForSeconds(0.3f);
        }
        transform.position = new Vector2(gameObject.transform.position.x + 0.7f, gameObject.transform.position.y);
        FlipSprite();
        playerRigidbody2D.isKinematic = false;
    }
    IEnumerator MovePlayerTowardsCastle()
    {
        while (transform.position.x < 124f)
        {
            playerRigidbody2D.AddForce(new Vector2(transform.position.x * 9f, transform.position.y));
            yield return new WaitForSeconds(0.1f);
        }
        playerSpriteRenderer.enabled = false;
        FindObjectOfType<MoveFlag>().MoveFlagUp();
        yield return new WaitForSeconds(1);
        FindObjectOfType<Fireworks>().PlayFireworks();
    }

}

