using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
   [SerializeField] private float resetYThreshold = -10f;
   [SerializeField] private GameObject resetPoint;

   private void Update()
   {
      if (transform.position.y < resetYThreshold)
      {
         transform.position = resetPoint.transform.position;
      }
   }
}
