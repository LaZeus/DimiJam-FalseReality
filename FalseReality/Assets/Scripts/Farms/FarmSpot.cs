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

    [SerializeField]
    private Seed.SeedType currentSeed;

    [Space]

    [SerializeField]
    private SpriteRenderer myArt;

    private InventoryManager inventory;
    private UIManager uiManager;

    private bool isClose = false;

    // Start is called before the first frame update
    private void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
        uiManager = FindObjectOfType<UIManager>();
        currentSeed = Seed.SeedType.Null;
        InitializeDimension(myArt);
    }

    private void Update()
    {
        CheckForPlayerInteraction();
    }

    private void CheckForPlayerInteraction()
    {
        if (isClose && myState != FarmState.locked)
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (myState == FarmState.empty) // can plant
                {
                    // plant
                    // show plant UI
                    uiManager.HidePlantUI();
                    uiManager.ShowPlantUI();
                }
                else if (myState == FarmState.grown) // can pick up
                {
                    // harvest
                    inventory.GetCrop(currentSeed);
                    currentSeed = Seed.SeedType.Null;
                    StopAllCoroutines();
                }
            }
    }

    public void PlantSeeds(float growingDuration, float goBadDuration)
    {
        if (myState == FarmState.empty)
            StartCoroutine(GrowSeeds(growingDuration, goBadDuration));
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
            isClose = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            isClose = false;
            uiManager.HidePlantUI();
        }
    }

    #region realityStuff

    public void RealityChanged()
    {
        myState = FarmState.locked;
    }

    public void RealityActived()
    {
        StopAllCoroutines();
        myState = FarmState.empty;
    }

    public void RealityDeactived()
    {
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
    #endregion
}
