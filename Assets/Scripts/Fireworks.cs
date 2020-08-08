using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Fireworks : MonoBehaviour
{
    public PlayableDirector timeline;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayFireworks()
    {
        StartCoroutine(ShootFireworks());
    }
    IEnumerator ShootFireworks()
    {
        for(int i = 0; i < 3; i++)
        {
            timeline.Play();
            transform.position = new Vector2(gameObject.transform.position.x+2f, gameObject.transform.position.y + 2f);
            yield return new WaitForSeconds(1f);
        }
    }

}
