using UnityEngine;

public class ResetTowerPosition : MonoBehaviour
{
   [SerializeField] private float resetYThreshold = -20f;
   private GameObject resetPoint;

   private void Start()
   {
      resetPoint = GameObject.FindGameObjectWithTag("ResetPoint");
      if (resetPoint == null)
      {
         Debug.LogError("No GameObject with the 'ResetPoint' tag found. Please assign the tag to the reset point GameObject.");
      }
   }

   private void Update()
   {
      if (resetPoint != null && transform.position.y < resetYThreshold)
      {
         transform.position = resetPoint.transform.position;
      }
   }
}
