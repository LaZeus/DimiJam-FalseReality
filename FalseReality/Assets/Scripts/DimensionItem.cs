using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionItem : MonoBehaviour
{
    public enum Dimension
    {
        Normal,
        Red,
        Green,
        Blue
    }

    public Dimension myDimension;

    protected void InitializeDimension(SpriteRenderer myArt)
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

    protected void InitializeDimension(SpriteRenderer myArt, Dimension affectingDimension)
    {
        switch (myDimension)
        {
            case Dimension.Normal:
                myArt.gameObject.layer = 9;
                break;
            case Dimension.Red:
                myArt.gameObject.layer = 10;
                break;
            case Dimension.Green:
                myArt.gameObject.layer = 11;
                break;
            case Dimension.Blue:
                myArt.color = Color.blue;
                break;
            default:
                break;
        }

        switch (affectingDimension)
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
                myArt.gameObject.layer = 12;
                break;
            default:
                break;
        }
    }
}
