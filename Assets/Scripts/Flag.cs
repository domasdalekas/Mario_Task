using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Flag : MonoBehaviour
{
    public PlayableDirector timeline;
    private bool animationExecuted = false;
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "Player" && !animationExecuted)
        {
            var t = collision.gameObject.GetComponent<PlayerController>();
            var spriteControl = collision.gameObject.GetComponent<SpriteRenderer>();
            t.isGameFinished = true;
            var mario = collision.contacts[0];
            float y = mario.point.y;
            timeline.Play();
            var rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            t.OnGameFinished();
            
            rigidbody.isKinematic = true;
            t.MovePlayerDownPole();
            //FindObjectOfType<MoveFlag>().MoveFlagUp();
            //FindObjectOfType<Fireworks>().PlayFireworks();
               
        }
    }
   
    
}
