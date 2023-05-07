using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonVR : MonoBehaviour
{
   public GameObject button;
   public UnityEvent onPress;
   public UnityEvent onRelease;
   XRBaseInteractor presser;
   AudioSource sound;
   bool isPressed;

   void Start()
   {
      sound = GetComponent<AudioSource>();
      isPressed = false;
   }

   private void OnTriggerEnter(Collider other)
   {
      XRBaseInteractor interactor = other.GetComponent<XRBaseInteractor>();
      if (interactor != null && interactor is XRPokeInteractor && !isPressed)
      {
         button.transform.localPosition = new Vector3(0, 0.003f, 0);
         presser = interactor;
         onPress.Invoke();
         sound.Play();
         isPressed = true;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      XRBaseInteractor interactor = other.GetComponent<XRBaseInteractor>();
      if (interactor != null && interactor == presser)
      {
         button.transform.localPosition = new Vector3(0, 0.015f, 0);
         onRelease.Invoke();
         isPressed = false;
      }
   }

   public void SpawnSphere()
   {
      GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
      sphere.transform.localPosition = new Vector3(0, 1, 2);
      sphere.AddComponent<Rigidbody>();
   }

}
