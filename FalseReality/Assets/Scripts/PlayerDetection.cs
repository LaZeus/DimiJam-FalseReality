using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public delegate void Actions();

    public Actions onDetection;
    public Actions stoppedDetection;

    public bool IsClose { get; set; }

    [SerializeField]
    private GameObject indicator;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            IsClose = true;
            if (indicator != null) indicator.SetActive(true);
            onDetection?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            IsClose = false;
            if (indicator != null) indicator.SetActive(false);
            stoppedDetection?.Invoke();
        }
    }

    public void DestroyIndicator()
    {
        Destroy(indicator);
    }
}
