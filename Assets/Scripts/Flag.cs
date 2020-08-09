using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Flag : MonoBehaviour
{
    public PlayableDirector timeline;
    public BoxCollider2D box;
    private void FixedUpdate()
    {
        FindObjectOfType<PlayerController>().TimelinePlay();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "Player")
        {
            
            var t = collision.gameObject.GetComponent<PlayerController>();
            t.isGameFinished = true;
            timeline.Play();
            t.OnGameFinished();
            box.enabled = false;
            

        }
    }
   
    
}
