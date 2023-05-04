using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerUI : MonoBehaviour
{
    public Canvas TowerUICanvas;
    public BoxCollider triggerCollider;
    public Transform playerCamera;
    public Text rangeText; // UI text component for range
    public Text fireRateText; // UI text component for fire rate
    public Text damageText; // UI text component for damage
    public List<float> rangeValues; // List of range values for each level
    public List<float> fireRateValues; // List of fire rate values for each level
    public List<float> damageValues;
    private bool inRange = false;
    public TowerBehavior towerBehavior;
    void Start(){
        TowerUICanvas.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            TowerUICanvas.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            TowerUICanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (inRange)
        {
            TowerUICanvas.transform.LookAt(playerCamera);
        }
    }
    void UpdateUIText()
    {
        int towerLevel = towerBehavior.towerLevel;

        if (towerLevel > 0 && towerLevel <= rangeValues.Count && towerLevel <= fireRateValues.Count && towerLevel <= damageValues.Count)
        {
            rangeText.text = "Range: " + rangeValues[towerLevel - 1];
            fireRateText.text = "Fire Rate: " + fireRateValues[towerLevel - 1];
            damageText.text = "Damage: " + damageValues[towerLevel - 1];
        }
    }
}
