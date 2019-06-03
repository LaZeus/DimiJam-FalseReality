using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityManager : MonoBehaviour
{ 
    public List<DimensionItem.Dimension> activeRealities = new List<DimensionItem.Dimension>();

    [Space]

    [SerializeField]
    [ContextMenuItem("Find Farms", "FindAllFarms")]
    private FarmSpot[] farmSpots;

    [Space]

    [SerializeField]
    private GameObject redCamera;
    
    [SerializeField]
    private GameObject greenCamera;
    
    [SerializeField]
    private GameObject blueCamera;

    private void FindAllFarms()
    {
        farmSpots = FindObjectsOfType<FarmSpot>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ManageCameras();
        UpdateFarms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddReality(DimensionItem.Dimension newReality)
    {
        if (!activeRealities.Contains(newReality))
            activeRealities.Add(newReality);

        ManageCameras();
        UpdateFarms();
    }

    public void RemoveReality(DimensionItem.Dimension newReality)
    {
        if (activeRealities.Contains(newReality))
            activeRealities.Remove(newReality);

        ManageCameras();
        UpdateFarms();
    }

    public void ChangeReality(DimensionItem.Dimension newReality)
    {
        if (activeRealities.Count > 0)
            activeRealities.RemoveRange(0,activeRealities.Count);

        activeRealities.Add(newReality);

        ManageCameras();
        UpdateFarms();
    }

    private void ManageCameras()
    {
        redCamera.SetActive(activeRealities.Contains(DimensionItem.Dimension.Red));
        greenCamera.SetActive(activeRealities.Contains(DimensionItem.Dimension.Green));
        blueCamera.SetActive(activeRealities.Contains(DimensionItem.Dimension.Blue));
    }

    private void UpdateFarms()
    {
        foreach (FarmSpot farm in farmSpots)
        {
            // check if it's active and needs to be deactivated
            if (!activeRealities.Contains(farm.GetFarmReality()) && farm.GetFarmReality() != DimensionItem.Dimension.Normal)
                farm.RealityDeactived();
            // check if it's locked and activate it
            else if (farm.GetFarmState() == FarmSpot.FarmState.locked)
                farm.RealityActived();
        }
    }
}
