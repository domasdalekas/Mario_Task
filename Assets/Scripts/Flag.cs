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
        float penki = 0.1f;
        if (collision.transform.tag == "Player" && !animationExecuted)
        {
            var t = collision.gameObject.GetComponent<PlayerController>();
            t.isGameFinished = true;
            // animationExecuted = true;
            var mario = collision.contacts[0];
            float x = mario.point.x;
            float y = mario.point.y;
            float groundPosition = -2.5f;
            timeline.Play();
          
            var rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
           
            t.OnGameFinished();
            //t.transform.position = Vector2.MoveTowards(new Vector2(x, y), new Vector2(x, groundPosition),(float)timeline.duration*0.01f);
            rigidbody.isKinematic = false;
            t.transform.position = Vector2.Lerp(new Vector2(x, y), new Vector2(x, groundPosition), penki);
            
            

        }
    }
}
