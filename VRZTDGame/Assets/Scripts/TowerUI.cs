using System.Collections;
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
    }

    void Update()
    {
        if (towerSpace.playerInTowerSpace)
        {
            TowerUICanvas.gameObject.SetActive(true);
            TowerUICanvas.transform.LookAt(playerCamera);
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
