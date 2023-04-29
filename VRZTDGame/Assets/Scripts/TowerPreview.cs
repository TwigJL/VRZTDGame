using UnityEngine;

public class TowerPreview : MonoBehaviour
{
    void Start()
    {
        // Make the tower preview transparent or ghosted
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
            color.a = 0.5f; // Adjust the alpha value for the desired transparency
            renderer.material.color = color;
        }
    }
}
