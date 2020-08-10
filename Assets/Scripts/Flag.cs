using UnityEngine;
using UnityEngine.Playables;

public class Flag : MonoBehaviour
{
    public PlayableDirector timeline;
    public BoxCollider2D box;
    private void FixedUpdate()
    {
        FindObjectOfType<PlayerController>().TimelinePlay();
        box = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Player")
        {

            var t = collision.gameObject.GetComponent<PlayerController>();
            t.isGameFinished = true;
            //t.takeAwayControll = true;
            timeline.Play();
            t.OnGameFinished();
            box.enabled = false;
        }
    }


}
