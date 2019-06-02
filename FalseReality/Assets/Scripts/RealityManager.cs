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
    private GameObject RedCamera;
    
    [SerializeField]
    private GameObject GreenCamera;
    
    [SerializeField]
    private GameObject BlueCamera;

    private void FindAllFarms()
    {
        farmSpots = FindObjectsOfType<FarmSpot>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddReality(DimensionItem.Dimension newReality)
    {

    }

    public void ChangeReality(DimensionItem.Dimension newReality)
    {
        if (activeRealities.Count > 0)
            activeRealities.RemoveRange(0,activeRealities.Count);


        foreach (FarmSpot farm in farmSpots)
        {
            if (!activeRealities.Contains(farm.GetFarmReality()))
                farm.RealityDeactived();
        }
    }

    private void ManageCameras()
    {

    }
}
