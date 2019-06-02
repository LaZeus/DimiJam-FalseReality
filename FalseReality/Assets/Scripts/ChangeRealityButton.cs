using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRealityButton : DimensionItem
{
    [SerializeField]
    private Dimension affectingReality;

    [Space]

    [SerializeField]
    private SpriteRenderer myArt;

    private RealityManager realityManager;

    private bool canBeTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        realityManager = FindObjectOfType<RealityManager>();
        InitializeDimension(myArt,affectingReality);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTriggered();
    }

    private void CheckIfTriggered()
    {
        if (canBeTriggered)
            if (Input.GetKeyDown(KeyCode.E))
                ActivateButton();
    }

    private void ActivateButton()
    {
        Debug.Log("TaDa");
        realityManager.ChangeReality(affectingReality);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.transform.name);
        if (col.transform.tag == "Player")
            canBeTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
            canBeTriggered = false;
    }
}
