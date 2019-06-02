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

    private PlayerDetection playerDetection;

    // Start is called before the first frame update
    void Start()
    {
        realityManager = FindObjectOfType<RealityManager>();
        InitializeDimension(myArt,affectingReality);

        playerDetection = GetComponent<PlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTriggered();
    }

    private void CheckIfTriggered()
    {
        if (playerDetection.IsClose)
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
}
