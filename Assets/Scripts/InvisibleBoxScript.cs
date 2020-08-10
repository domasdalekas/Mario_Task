using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBoxScript : MonoBehaviour
{
    SpriteRenderer rend;
    BoxCollider2D collider2D;
    PlayerController playerController;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        collider2D = gameObject.GetComponent<BoxCollider2D>();
        rend.enabled = false;
        
       
    }
    // Update is called once per frame
    void Update()
    {
        if (IsPlayerBelow(player))
        {
            gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.transform.tag == "Player" && IsPlayerBelow(collision.gameObject))
            {
                collision.gameObject.GetComponent<PlayerController>().isJumping = false; //Mario can't jump higher
                rend.enabled = true;
            }
    }
    private bool IsPlayerBelow(GameObject go)
    {
        if ((go.transform.position.y + 1.4f < this.transform.position.y)) //if Mario is powered-up
            return true;
        if ((go.transform.position.y + 0.4f < this.transform.position.y) && !go.transform.GetComponent<PlayerController>().poweredUp)
            return true;
        return false;
    }
}
