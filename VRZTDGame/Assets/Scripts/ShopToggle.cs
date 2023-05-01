using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ShopToggle : MonoBehaviour
{
    public ActionBasedController rightController;
    public Canvas shopCanvas;
    public InputAction openShopAction;

    void Start()
    {
        // Ensure the shopCanvas is initially disabled
        shopCanvas.gameObject.SetActive(false);

        // Set up the "open shop" action
        openShopAction = new InputAction("Open Shop");
        openShopAction.AddBinding("<XRController>{RightHand}/PrimaryButton");
        openShopAction.Enable();

        // Set the activateAction property of the rightController to the openShopAction
        rightController.activateAction = new InputActionProperty(openShopAction);

        // Subscribe to the "performed" and "canceled" events
        openShopAction.performed += OnOpenShopPerformed;
        openShopAction.canceled += OnOpenShopCanceled;
    }

    void OnDestroy()
    {
        // Unsubscribe from the events when the script is destroyed
        openShopAction.performed -= OnOpenShopPerformed;
        openShopAction.canceled -= OnOpenShopCanceled;

        // Disable and dispose the "open shop" action
        openShopAction.Disable();
        openShopAction.Dispose();
    }

    private void OnOpenShopPerformed(InputAction.CallbackContext context)
    {
        // Enable the shopCanvas when the "open shop" action is performed
        shopCanvas.gameObject.SetActive(true);
    }

    private void OnOpenShopCanceled(InputAction.CallbackContext context)
    {
        // Disable the shopCanvas when the "open shop" action is canceled
        shopCanvas.gameObject.SetActive(false);
    }
}
