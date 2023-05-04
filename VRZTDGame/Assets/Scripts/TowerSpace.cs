using UnityEngine;

public class TowerSpace : MonoBehaviour
{
   public bool playerInTowerSpace = false;
   public GameObject Range;
   private void Start()
   {
      MeshRenderer meshRenderer = Range.GetComponent<MeshRenderer>();
      meshRenderer.enabled = false;
   }
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         MeshRenderer meshRenderer = Range.GetComponent<MeshRenderer>();
         meshRenderer.enabled = true;
         playerInTowerSpace = true;
         // Add your logic for when the player enters the trigger
         Debug.Log("Player entered the trigger.");

      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         MeshRenderer meshRenderer = Range.GetComponent<MeshRenderer>();
         meshRenderer.enabled = false;
         playerInTowerSpace = false;
         // Add your logic for when the player exits the trigger
         Debug.Log("Player exited the trigger.");
      }
   }
}
