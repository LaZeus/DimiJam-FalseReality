using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRealityButton : DimensionItem
{
    [SerializeField]
    private Dimension affectingReality;

    [SerializeField]
    private bool additiveReality;

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
        if (!realityManager.activeRealities.Contains(affectingReality))
            if (additiveReality)
                realityManager.AddReality(affectingReality);
            else
                realityManager.ChangeReality(affectingReality);
        else
            realityManager.RemoveReality(affectingReality);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
            canBeTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
            canBeTriggered = false;
    }
}
