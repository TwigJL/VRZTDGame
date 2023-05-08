using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TypingArea : MonoBehaviour
{
   public XRBaseController leftController;
   public XRBaseController rightController;
   public float activationDistance = 0.5f;

   public XRPokeInteractor leftPokeInteractor;
   public XRPokeInteractor rightPokeInteractor;
   public XRGrabInteractable keyboardInteractable;

   void Update()
   {
      float leftHandDistance = Vector3.Distance(leftController.transform.position, transform.position);
      float rightHandDistance = Vector3.Distance(rightController.transform.position, transform.position);

      if (leftHandDistance <= activationDistance || rightHandDistance <= activationDistance)
      {
         keyboardInteractable.interactionLayers |= (1 << LayerMask.NameToLayer("PokeInteractable"));
         leftPokeInteractor.enabled = true;
         rightPokeInteractor.enabled = true;
      }
      else
      {
         keyboardInteractable.interactionLayers &= ~(1 << LayerMask.NameToLayer("PokeInteractable"));
         leftPokeInteractor.enabled = false;
         rightPokeInteractor.enabled = false;
      }
   }
}
