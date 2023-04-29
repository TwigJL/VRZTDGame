using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionButton : MonoBehaviour
{
    public TowerPlacementManager towerPlacementManager;
    public GameObject towerPrefab;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SelectTower);
    }

    void SelectTower()
    {
        towerPlacementManager.SelectTower(towerPrefab);
    }
}
