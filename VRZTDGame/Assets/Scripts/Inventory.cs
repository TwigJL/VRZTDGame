using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
public class Inventory : MonoBehaviour
{
   public XRRayInteractor rightRayInteractor;
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
         Vector3 spawnPosition = GetRaycastIntersectionPosition(); // Get the intersection point of the raycast
         Quaternion spawnRotation = Quaternion.identity;
         Instantiate(towerPrefab, spawnPosition, spawnRotation);

         // Update the tower quantity text
         UpdateTowerQuantityText(towerIndex);
      }
   }

   private Vector3 GetRaycastIntersectionPosition()
   {
      RaycastHit hit;
      Vector3 rayOrigin = rightRayInteractor.transform.position;
      Vector3 rayDirection = rightRayInteractor.transform.forward;

      if (Physics.Raycast(rayOrigin, rayDirection, out hit))
      {
         return hit.point;
      }
      else
      {
         return rightRayInteractor.transform.position;
      }
   }




   private void UpdateTowerQuantityText(int towerIndex)
    {
        towerQuantityTexts[towerIndex].text = "Quantity: " + towerQuantities[towerIndex].ToString();
    }
}
