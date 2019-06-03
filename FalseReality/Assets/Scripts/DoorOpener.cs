using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private Seed.SeedType preferedType;

    [SerializeField]
    private int seedsToOpenDoor;

    [SerializeField]
    private GameObject door;

    [Space]

    [SerializeField]
    private SpriteRenderer myArt;

    private PlayerDetection playerDetection;
    private InventoryManager inventory;


    // Start is called before the first frame update
    void Start()
    {
        playerDetection = GetComponent<PlayerDetection>();
        inventory = FindObjectOfType<InventoryManager>();
        SetColor();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if (playerDetection.IsClose && inventory.CanPlantSeed((int)preferedType) && door != null)
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.PlantSeed((int)preferedType);
                seedsToOpenDoor--;
                if (seedsToOpenDoor <= 0) OpenDoor();
            }
    }

    private void OpenDoor()
    {
        Destroy(door);
        door = null;
        playerDetection.DestroyIndicator();
    }

    private void SetColor()
    {
        switch (preferedType)
        {
            case Seed.SeedType.TypeA:
                myArt.color = Color.cyan;
                break;
            case Seed.SeedType.TybeB:
                myArt.color = Color.yellow;
                break;
            case Seed.SeedType.TypeC:
                myArt.color = Color.magenta;
                break;
            default:
                break;
        }

    }
}
