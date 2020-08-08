using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlag : MonoBehaviour
{

  
    public void MoveFlagUp()
    {
        StartCoroutine(Move());
     }
    IEnumerator Move()
    {
        while (transform.position.y < 2.3f)
        {
            transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.4f);
            yield return new WaitForSeconds(1f);
        }
       
    }
}
