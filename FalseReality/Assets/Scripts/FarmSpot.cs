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
        ChangeState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RealityChanged()
    {

    }

    private void ChangeState()
    {
        switch (myDimension)
        {
            case Dimension.Normal:
                myArt.color = Color.gray;
                myArt.gameObject.layer = 9;
                break;
            case Dimension.Red:
                myArt.color = Color.red;
                myArt.gameObject.layer = 10;
                break;
            case Dimension.Green:
                myArt.color = Color.green;
                myArt.gameObject.layer = 11;
                break;
            case Dimension.Blue:
                myArt.color = Color.blue;
                myArt.gameObject.layer = 12;
                break;
            default:
                break;
        }
    }

    public void RealityDeactived()
    {
        Debug.Log("My reality doesn't exist");
    }

    public Dimension GetFarmReality()
    {
        return myDimension;
    }
}
