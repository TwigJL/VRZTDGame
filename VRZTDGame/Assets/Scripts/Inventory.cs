using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Text[] towerQuantityTexts = new Text[7];

    private int[] towerQuantities = new int[7];

    public void AddTower(int towerIndex)
    {
        towerQuantities[towerIndex]++;
        UpdateTowerQuantityText(towerIndex);
    }

    private void UpdateTowerQuantityText(int towerIndex)
    {
        towerQuantityTexts[towerIndex].text = "Quantity: " + towerQuantities[towerIndex].ToString();
    }
}
