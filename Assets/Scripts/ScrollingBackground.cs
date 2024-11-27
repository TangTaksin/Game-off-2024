using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeedX = 0.1f; // Horizontal scroll speed
    [SerializeField] private float scrollSpeedY = 0.1f; // Vertical scroll speed
    private Renderer backgroundRenderer;

    private void Start()
    {
        // Get the Renderer component attached to the GameObject
        backgroundRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Calculate the new offset based on time and scroll speed
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;

        // Apply the offset to the material's main texture
        backgroundRenderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
