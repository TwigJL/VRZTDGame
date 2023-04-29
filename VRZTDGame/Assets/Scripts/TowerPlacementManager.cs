using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    public GameObject towerPreviewPrefab;
    public LayerMask terrainLayer;

    private GameObject currentTowerPreview;
    private GameObject selectedTowerPrefab;
    private Transform vrPointer;

    public bool placementMode { get; private set; }

    void Start()
    {
        placementMode = false;
        vrPointer = Camera.main.transform;
    }

    void Update()
    {
        if (placementMode)
        {
            RaycastHit hit;
            if (Physics.Raycast(vrPointer.position, vrPointer.forward, out hit, Mathf.Infinity, terrainLayer))
            {
                if (currentTowerPreview == null)
                {
                    currentTowerPreview = Instantiate(towerPreviewPrefab, hit.point, Quaternion.identity);
                }
                else
                {
                    currentTowerPreview.transform.position = hit.point;
                }
            }
        }
    }

    public void SelectTower(GameObject towerPrefab)
    {
        selectedTowerPrefab = towerPrefab;
        placementMode = true;
    }

    public void ConfirmTowerPlacement()
    {
        if (currentTowerPreview != null)
        {
            Instantiate(selectedTowerPrefab, currentTowerPreview.transform.position, Quaternion.identity);
            Destroy(currentTowerPreview);
            placementMode = false;
        }
    }
}
