using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public List<GameObject> towerPrefabs;
    public TowerPlacer towerPlacer;
    public int startingCurrency;
    public int currentCurrency;

    private void Start()
    {
        currentCurrency = startingCurrency;
    }

    public void SelectTower(int index)
    {
        if (index >= 0 && index < towerPrefabs.Count)
        {
            towerPlacer.SetSelectedTowerPrefab(towerPrefabs[index]);
        }
    }

    public bool CanAffordTower(int cost)
    {
        return currentCurrency >= cost;
    }

    public void PurchaseTower(int cost)
    {
        if (CanAffordTower(cost))
        {
            currentCurrency -= cost;
        }
    }

    public void SellTower(int sellValue)
    {
        currentCurrency += sellValue;
    }
}
