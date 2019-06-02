using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Slider growthBar;

    [SerializeField]
    private Image sliderFill;

    private InventoryManager inventory;
    private UIManager uiManager;
    private SeedsStatsManager seedsStats;

    private bool isClose = false;

    // Start is called before the first frame update
    private void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
        uiManager = FindObjectOfType<UIManager>();
        seedsStats = FindObjectOfType<SeedsStatsManager>();

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
                    uiManager.ShowPlantUI(this);
                }
                else if (myState == FarmState.grown) // can pick up
                {
                    // harvest
                    myState = FarmState.empty;
                    inventory.GetCrop(currentSeed);
                    currentSeed = Seed.SeedType.Null;
                    StopGrowth();
                }
            }
    }

    public void PlantSeeds(int seed)
    {
        currentSeed = (Seed.SeedType)seed;

        Seed.SeedStats currentSeedStats = seedsStats.GetSeedStats(currentSeed);

        float growingDuration = currentSeedStats.growthTime;
        float goBadDuration = currentSeedStats.goneBadTime;
        if (myState == FarmState.empty)
            StartCoroutine(GrowSeeds(growingDuration, goBadDuration));
    }

    private IEnumerator GrowSeeds(float duration, float goneBadDuration)
    {
        // start growing

        myState = FarmState.growing;

        Debug.Log(myState);

        growthBar.gameObject.SetActive(true);
        sliderFill.color = Color.green;

        float startTime = Time.time;
        float timeDifference;

        do
        {
            timeDifference = Time.time - startTime;
            growthBar.value = timeDifference / duration;
            yield return null;

        } while (timeDifference < duration);

        // crop is grown
        // crop starts going bad

        myState = FarmState.grown;
        Debug.Log(myState);

        sliderFill.color = Color.red;

        startTime = Time.time;
        do
        {
            timeDifference = Time.time - startTime;
            growthBar.value = timeDifference / duration;
            yield return null;

        } while (timeDifference < goneBadDuration);

        // crop has gone bad
        // crop falls

        myState = FarmState.empty;
        Debug.Log(myState);
    }

    private void StopGrowth()
    {
        StopAllCoroutines();
        growthBar.gameObject.SetActive(false);
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
        StopGrowth();
        myState = FarmState.empty;
    }

    public void RealityDeactived()
    {
        StopGrowth();
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
