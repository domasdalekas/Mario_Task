using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Flag : MonoBehaviour
{
    public PlayableDirector timeline;
    private bool animationExecuted = false;
   
    
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "Player" && !animationExecuted)
        {
            var t = collision.gameObject.GetComponent<PlayerController>();
            t.isGameFinished = true;
            
          
            var mario = collision.contacts[0];
            float x = mario.point.x;
            float y = mario.point.y;
           
           
            timeline.Play();
          
            var rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
           
            t.OnGameFinished();
            
            //t.transform.position = new Vector2(x, y - 0.2f);
            //rigidbody.transform.position = Vector2.Lerp(new Vector2(x, y), new Vector2(x, groundPosition), penki);

            //rigidbody.transform.position = Vector2.MoveTowards(new Vector2(x, y), new Vector2(x, groundPosition), (float)timeline.duration * 0.01f);

            //t.RunToCastle();
        }
    }
   
}
