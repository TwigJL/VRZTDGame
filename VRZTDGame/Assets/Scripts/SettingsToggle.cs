using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class SettingsToggle : MonoBehaviour
{
   public Canvas settingsCanvas;
   public InputAction openSettingsAction;

   void Start()
   {
      // Ensure the settingsCanvas is initially disabled
      settingsCanvas.gameObject.SetActive(false);

      // Set up the "open settings" action
      openSettingsAction = new InputAction("Open Settings");
      openSettingsAction.AddBinding("<XRController>{LeftHand}/SecondaryButton");
      openSettingsAction.Enable();

      // Subscribe to the "performed" and "canceled" events
      openSettingsAction.performed += OnOpenSettingsPerformed;
      openSettingsAction.canceled += OnOpenSettingsCanceled;
   }

   void OnDestroy()
   {
      // Unsubscribe from the events when the script is destroyed
      openSettingsAction.performed -= OnOpenSettingsPerformed;
      openSettingsAction.canceled -= OnOpenSettingsCanceled;

      // Disable and dispose the "open settings" action
      openSettingsAction.Disable();
      openSettingsAction.Dispose();
   }

   private void OnOpenSettingsPerformed(InputAction.CallbackContext context)
   {
      // Enable the settingsCanvas when the "open settings" action is performed
      settingsCanvas.gameObject.SetActive(true);
   }

   private void OnOpenSettingsCanceled(InputAction.CallbackContext context)
   {
      // Disable the settingsCanvas when the "open settings" action is canceled
      settingsCanvas.gameObject.SetActive(false);
   }
}
