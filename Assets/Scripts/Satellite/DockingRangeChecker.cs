using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingRangeChecker : MonoBehaviour
{
    public bool isInDockingRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInDockingRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isInDockingRange = false;
        }
    }
}
