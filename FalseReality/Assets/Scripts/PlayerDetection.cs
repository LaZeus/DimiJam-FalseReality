using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public delegate void Actions();

    public Actions onDetection;
    public Actions stoppedDetection;

    public bool IsClose { get; set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            IsClose = true;
            onDetection?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            IsClose = false;
            stoppedDetection?.Invoke();
        }
    }
}
