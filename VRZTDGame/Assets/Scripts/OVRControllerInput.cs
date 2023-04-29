using UnityEngine;
using OculusSampleFramework;
using Oculus.Interaction.Input;

public class OVRControllerInput : MonoBehaviour
{
    public OVRInput.Controller controller;
    public Hand hand;
    public TowerPlacementManager towerPlacementManager;

    void Update()
    {
        // Check for primary index trigger press
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            if (towerPlacementManager.placementMode)
            {
                towerPlacementManager.ConfirmTowerPlacement();
            }
        }
    }
}
