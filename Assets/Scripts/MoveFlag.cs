using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlag : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
       
    }
    public void MoveFlagUp()
    {
        while (transform.position.y < 2f)
        {
            Debug.Log(transform.position = new Vector2(gameObject.transform.position.x, 0.5f));
        }
     }
}
