using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TowerPlacer : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public GameObject selectedTowerPrefab;

    private GameObject temporaryTower;

    private void OnEnable()
    {
        rayInteractor.selectEntered.AddListener(OnSelectEntered);
        rayInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        rayInteractor.selectEntered.RemoveListener(OnSelectEntered);
        rayInteractor.selectExited.RemoveListener(OnSelectExited);
    }

    private void Update()
    {
        if (temporaryTower != null)
        {
            RaycastHit hit;
            if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                temporaryTower.transform.position = hit.point;
            }
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (selectedTowerPrefab != null)
        {
            RaycastHit hit;
            if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                temporaryTower = Instantiate(selectedTowerPrefab, hit.point, Quaternion.identity);
            }
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (temporaryTower != null)
        {
            RaycastHit hit;
            if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                temporaryTower.transform.position = hit.point;
                selectedTowerPrefab = null;
            }
            temporaryTower = null;
        }
    }

    public void SetSelectedTowerPrefab(GameObject towerPrefab)
    {
        selectedTowerPrefab = towerPrefab;
    }
}
