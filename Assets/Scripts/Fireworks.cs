using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Fireworks : MonoBehaviour
{
    public GameObject firework1;
    public GameObject firework2;
    public GameObject firework3;
    private SpriteRenderer rend;
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    public void PlayFireworks()
    {
        StartCoroutine(ShootFireworks());
        
    }
    IEnumerator ShootFireworks()
    {
        rend.enabled = true;
        Instantiate(firework1);
        yield return new WaitForSeconds(1);
        firework2.GetComponent<SpriteRenderer>().enabled = true;
        Instantiate(firework2);
        yield return new WaitForSeconds(1);
        firework3.GetComponent<SpriteRenderer>().enabled = true;
        Instantiate(firework3);
        yield return new WaitForSeconds(1);
        
    }
    
   
}
