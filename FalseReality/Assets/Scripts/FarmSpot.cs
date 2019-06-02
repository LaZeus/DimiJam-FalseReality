using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSpot : DimensionItem
{
    public enum FarmState
    {
        locked,
        empty,
        growing,
        grown
    }

    [Space]

    [SerializeField]
    private FarmState myState;

    [Space]

    [SerializeField]
    private SpriteRenderer myArt;

    // Start is called before the first frame update
    void Start()
    {
        InitializeState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeState()
    {
        switch (myDimension)
        {
            case Dimension.Normal:
                myArt.color = Color.gray;
                break;
            case Dimension.Red:
                myArt.color = Color.red;
                break;
            case Dimension.Green:
                myArt.color = Color.green;
                break;
            case Dimension.Blue:
                myArt.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
