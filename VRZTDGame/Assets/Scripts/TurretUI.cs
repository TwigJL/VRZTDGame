using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUI : MonoBehaviour
{
    public Canvas turretCanvas;
    public BoxCollider triggerCollider;
    public Transform playerCamera;

    private bool inRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            turretCanvas.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            turretCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (inRange)
        {
            turretCanvas.transform.LookAt(playerCamera);
        }
    }
}
