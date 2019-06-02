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
        InitializeDimension(myArt);
        PlantSeeds();
    }

    public void PlantSeeds()
    {
        if (myState == FarmState.empty)
            StartCoroutine(GrowSeeds(5,2)); // temp 5
    }

    private IEnumerator GrowSeeds(float duration, float goneBadDuration)
    {
        // start growing

        myState = FarmState.growing;

        Debug.Log(myState);

        float startTime = Time.time;

        while (Time.time - startTime < duration)
            yield return null;

        // crop is grown
        // crop starts going bad

        myState = FarmState.grown;
        Debug.Log(myState);

        startTime = Time.time;

        while (Time.time - startTime < goneBadDuration)
            yield return null;

        // crop has gone bad
        // crop falls

        myState = FarmState.empty;
        Debug.Log(myState);
    }

    /// <Realitystuff>
    /// 
    /// </Realitystuff>
    
    public void RealityChanged()
    {
        myState = FarmState.locked;
    }

    public void RealityActived()
    {
        Debug.Log("My reality exists");
        StopAllCoroutines();
        myState = FarmState.empty;
    }

    public void RealityDeactived()
    {
        Debug.Log("My reality doesn't exist");
        StopAllCoroutines();
        myState = FarmState.locked;
    }

    public Dimension GetFarmReality()
    {
        return myDimension;
    }

    public FarmState GetFarmState()
    {
        return myState;
    }
}
