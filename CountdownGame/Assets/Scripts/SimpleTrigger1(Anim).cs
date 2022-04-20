using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrigger1 : MonoBehaviour
{
    public Collider2D col;

    private void Start()
    {
       
        col = gameObject.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            
        }
        
    }


}
