using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
public class Inventory : MonoBehaviour
{
   public XRRayInteractor rightRayInteractor;
   private GameObject previewTower;
   private bool isDragging;
   public List<GameObject> towerPrefabs = new List<GameObject>();
    public Text[] towerQuantityTexts = new Text[7];

    private int[] towerQuantities = new int[7];
    public void AddTower(int towerIndex)
    {
        towerQuantities[towerIndex]++;
        UpdateTowerQuantityText(towerIndex);
    }
    public void SpawnPhotonBlasterTower()
    {
        SpawnTower(0);
    }
    

    public void SpawnFlameThrowerTower()
    {
        SpawnTower(1);
    }
    public void SpawnFreezeRayTower()
    {
        SpawnTower(2);
    }

    public void SpawnGaussCannonTower()
    {
        SpawnTower(3);
    }
    public void SpawnIonCannonTower()
    {
        SpawnTower(4);
    }

    public void SpawnRocketLauncherTower()
    {
        SpawnTower(5);
    }
    public void SpawnSlicerTower()
    {
        SpawnTower(6);
    }
   
   private void SpawnTower(int towerIndex)
   {
      if (towerQuantities[towerIndex] > 0)
      {
         // Subtract 1 from the tower quantity
         towerQuantities[towerIndex]--;

         // Instantiate the tower prefab
         GameObject towerPrefab = towerPrefabs[towerIndex];
         Vector3 spawnPosition = rightRayInteractor.transform.position; // Get the controller's position
         Vector3 spawnOffset = rightRayInteractor.transform.forward * 3f; // Offset in the forward direction of the controller
         spawnPosition += spawnOffset; // Add the offset to the spawn position
         Quaternion spawnRotation = Quaternion.Euler(0, 0, 0); // Get the controller's rotation
         Instantiate(towerPrefab, spawnPosition, spawnRotation);

         // Update the tower quantity text
         UpdateTowerQuantityText(towerIndex);
      }
   }

   private void UpdateTowerQuantityText(int towerIndex)
    {
        towerQuantityTexts[towerIndex].text = "Quantity: " + towerQuantities[towerIndex].ToString();
    }
}
