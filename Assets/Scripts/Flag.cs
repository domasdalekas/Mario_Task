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
            
            // animationExecuted = true;
            var mario = collision.contacts[0];
            float x = mario.point.x;
            float y = mario.point.y;
           
           
            timeline.Play();
          
          
            t.OnGameFinished();
            StartCoroutine(TimeWait());
            t.RunToCastle();
        }
    }
    IEnumerator TimeWait()
    {
        yield return new WaitForSeconds(2);
        
    }
}
