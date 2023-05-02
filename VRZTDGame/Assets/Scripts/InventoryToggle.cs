using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    public ActionBasedController leftController;
    public Canvas inventoryCanvas;
    public InputAction openInventoryAction;

    void Start()
    {
        // Ensure the inventoryCanvas is initially disabled
        inventoryCanvas.gameObject.SetActive(false);

        // Set up the "open inventory" action
        openInventoryAction = new InputAction("Open Inventory");
        openInventoryAction.AddBinding("<XRController>{LeftHand}/PrimaryButton");
        openInventoryAction.Enable();

        // Set the activateAction property of the leftController to the openInventoryAction
        leftController.activateAction = new InputActionProperty(openInventoryAction);

        // Subscribe to the "performed" and "canceled" events
        openInventoryAction.performed += OnOpenInventoryPerformed;
        openInventoryAction.canceled += OnOpenInventoryCanceled;
    }

    void OnDestroy()
    {
        // Unsubscribe from the events when the script is destroyed
        openInventoryAction.performed -= OnOpenInventoryPerformed;
        openInventoryAction.canceled -= OnOpenInventoryCanceled;

        // Disable and dispose the "open inventory" action
        openInventoryAction.Disable();
        openInventoryAction.Dispose();
    }

    private void OnOpenInventoryPerformed(InputAction.CallbackContext context)
    {
        // Enable the inventoryCanvas when the "open inventory" action is performed
        inventoryCanvas.gameObject.SetActive(true);
    }

    private void OnOpenInventoryCanceled(InputAction.CallbackContext context)
    {
        // Disable the inventoryCanvas when the "open inventory" action is canceled
        inventoryCanvas.gameObject.SetActive(false);
    }
}
