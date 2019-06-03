using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmSpot : DimensionItem
{
    public enum FarmState
    {
        empty,
        growing,
        grown,
        locked
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
    private Sprite[] farmSprites;

    [Space]

    [SerializeField]
    private Slider growthBar;

    [SerializeField]
    private Image sliderFill;

    private InventoryManager inventory;
    private UIManager uiManager;
    private SeedsStatsManager seedsStats;

    private PlayerDetection playerDetection;

    // Start is called before the first frame update
    private void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
        uiManager = FindObjectOfType<UIManager>();
        seedsStats = FindObjectOfType<SeedsStatsManager>();

        if (playerDetection == null) PlayerDetectionInit();

        currentSeed = Seed.SeedType.Null;
        InitializeDimension(myArt);
    }

    private void Update()
    {
        CheckForPlayerInteraction();
    }

    private void PlayerDetectionInit()
    {
        playerDetection = GetComponent<PlayerDetection>();
        playerDetection.stoppedDetection = HideUI;
    }

    private void HideUI()
    {
        uiManager.HidePlantUI();
    }

    private void ChangeSpriteDependingOnState(int state)
    {
        myArt.sprite = farmSprites[state];
    }

    private void CheckForPlayerInteraction()
    {
        if (playerDetection.IsClose && myState != FarmState.locked)
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
        ChangeSpriteDependingOnState((int)myState);

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
        ChangeSpriteDependingOnState((int)myState);
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
        ChangeSpriteDependingOnState((int)myState);
        growthBar.gameObject.SetActive(false);
        Debug.Log(myState);
    }

    private void StopGrowth()
    {
        myState = FarmState.empty;
        ChangeSpriteDependingOnState((int)myState);
        StopAllCoroutines();
        growthBar.gameObject.SetActive(false);
    }

    #region realityStuff

    public void RealityChanged()
    {
        myState = FarmState.locked;
    }

    public void RealityActived()
    {
        if (playerDetection == null) PlayerDetectionInit();
        StopGrowth();
        myState = FarmState.empty;
        playerDetection.activated = true;
    }

    public void RealityDeactived()
    {
        if (playerDetection == null) PlayerDetectionInit();
        StopGrowth();
        myState = FarmState.locked;
        playerDetection.activated = false;
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
