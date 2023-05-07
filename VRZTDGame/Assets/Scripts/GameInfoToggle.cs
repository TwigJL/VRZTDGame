using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GameInfoToggle : MonoBehaviour
{
   public Canvas gameInfoCanvas;
   public InputAction openGameInfoAction;

   void Start()
   {
      // Ensure the gameInfoCanvas is initially disabled
      gameInfoCanvas.gameObject.SetActive(false);

      // Set up the "open game info" action
      openGameInfoAction = new InputAction("Open Game Info");
      openGameInfoAction.AddBinding("<XRController>{RightHand}/SecondaryButton");
      openGameInfoAction.Enable();

      // Subscribe to the "performed" and "canceled" events
      openGameInfoAction.performed += OnOpenGameInfoPerformed;
      openGameInfoAction.canceled += OnOpenGameInfoCanceled;

      // Set the activateUsage property to detect input from the secondary button
   }

   void OnDestroy()
   {
      // Unsubscribe from the events when the script is destroyed
      openGameInfoAction.performed -= OnOpenGameInfoPerformed;
      openGameInfoAction.canceled -= OnOpenGameInfoCanceled;

      // Disable and dispose the "open game info" action
      openGameInfoAction.Disable();
      openGameInfoAction.Dispose();
   }

   private void OnOpenGameInfoPerformed(InputAction.CallbackContext context)
   {
      // Enable the game information panel when the "open game info" action is performed
      gameInfoCanvas.gameObject.SetActive(true);
   }

   private void OnOpenGameInfoCanceled(InputAction.CallbackContext context)
   {
      // Disable the game information panel when the "open game info" action is canceled
      gameInfoCanvas.gameObject.SetActive(false);
   }
}
