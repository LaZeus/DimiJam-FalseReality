using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private UIManager uiManager;

    private PlayerDetection playerDetection;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerDetection = GetComponent<PlayerDetection>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if (playerDetection.IsClose)
            if (Input.GetKeyDown(KeyCode.E))
            {
                uiManager.StartTrades();
            }
    }
}
