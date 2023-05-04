using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerUI : MonoBehaviour
{
    public Canvas TowerUICanvas;
    public Transform playerCamera;
    public Text rangeText; // UI text component for range
    public Text fireRateText; // UI text component for fire rate
    public Text damageText; // UI text component for damage
    public List<string> rangeValues; // List of range values for each level
    public List<string> fireRateValues; // List of fire rate values for each level
    public List<string> damageValues;
    
    public TowerBehavior towerBehavior;
    public TowerSpace towerSpace;
    void Start(){
        TowerUICanvas.gameObject.SetActive(false);
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        if (towerSpace.playerInTowerSpace)
        {
            TowerUICanvas.gameObject.SetActive(true);
            Vector3 lookDirection = playerCamera.position - TowerUICanvas.transform.position;
            lookDirection.y = 0; // Keep the y value of the lookDirection to 0 if you want the canvas to stay at the same height
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            TowerUICanvas.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + 180, 0); // Rotate the canvas 180 degrees around the Y-axis
            UpdateUIText();
        }else{
            TowerUICanvas.gameObject.SetActive(false);
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
