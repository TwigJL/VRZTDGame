using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class XRCanvasInput : MonoBehaviour
{
    public XRController leftController;
    public XRController rightController;
    public Canvas leftHandCanvas;
    public Canvas rightHandCanvas;
    public GraphicRaycaster leftGraphicRaycaster;
    public GraphicRaycaster rightGraphicRaycaster;
    public EventSystem eventSystem;

    private InputDevice leftInputDevice;
    private InputDevice rightInputDevice;
    private Vector2 touchPosition;

    void Start()
    {
        leftInputDevice = leftController.inputDevice;
        rightInputDevice = rightController.inputDevice;
        leftHandCanvas.gameObject.SetActive(false);
        rightHandCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        // Activate/deactivate canvases based on primary button press
        bool isLeftPrimaryButtonPressed = false;
        bool isRightPrimaryButtonPressed = false;

        leftInputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isLeftPrimaryButtonPressed);
        rightInputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isRightPrimaryButtonPressed);

        leftHandCanvas.gameObject.SetActive(isLeftPrimaryButtonPressed);
        rightHandCanvas.gameObject.SetActive(isRightPrimaryButtonPressed);

        // Check if the left or right controller is currently selecting an UI element
        bool isLeftSelecting = false;
        bool isRightSelecting = false;

        leftInputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out touchPosition);
        isLeftSelecting = touchPosition != Vector2.zero;

        rightInputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out touchPosition);
        isRightSelecting = touchPosition != Vector2.zero;

        // Process UI interactions for left and right hands separately
        if (isLeftSelecting && leftHandCanvas.gameObject.activeSelf)
        {
            ProcessUIInteraction(leftController, leftGraphicRaycaster);
        }

        if (isRightSelecting && rightHandCanvas.gameObject.activeSelf)
        {
            ProcessUIInteraction(rightController, rightGraphicRaycaster);
        }
    }
    private void ProcessUIInteraction(XRController controller, GraphicRaycaster graphicRaycaster)
    {
        // Perform a raycast using the controller's pointer
        Ray ray = new Ray(controller.transform.position, controller.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has a canvas with a GraphicRaycaster component
            Canvas hitCanvas = hit.collider.gameObject.GetComponent<Canvas>();

            if (hitCanvas != null && hitCanvas.gameObject == graphicRaycaster.gameObject)
            {
                // Perform UI raycast
                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = touchPosition;
                List<RaycastResult> results = new List<RaycastResult>();
                graphicRaycaster.Raycast(pointerEventData, results);

                // Process the results
                foreach (RaycastResult result in results)
                {
                    // Check if the result has a selectable component (e.g., button)
                    Selectable selectable = result.gameObject.GetComponent<Selectable>();

                    if (selectable != null)
                    {
                        // Check if the primary button is pressed
                        bool isPrimaryButtonPressed = false;
                        controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isPrimaryButtonPressed);

                        if (isPrimaryButtonPressed)
                        {
                            // Trigger the selectable component (e.g., button click)
                            ExecuteEvents.Execute(result.gameObject, pointerEventData, ExecuteEvents.submitHandler);
                        }
                    }
                }
            }
        }
    }
}