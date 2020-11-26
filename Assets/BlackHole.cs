using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public enum TriggerType {Enter, Exit};

    [SerializeField] Transform teleportTo;
    [SerializeField] string filterTag = "Player";
    [SerializeField] TriggerType type;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (type != TriggerType.Enter)
        {
            return;
        }

        if (tag == string.Empty || other.CompareTag(filterTag))
        {
            other.transform.position = teleportTo.position;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (type != TriggerType.Exit)
        {
            return;
        }

        if (tag == string.Empty || other.CompareTag(filterTag))
        {
            other.transform.position = teleportTo.position;
        }
    }

}
