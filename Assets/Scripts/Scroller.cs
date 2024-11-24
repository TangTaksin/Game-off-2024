using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [Header("Scroll Settings")]
    [SerializeField] private RawImage _img; // Reference to the RawImage component
    [SerializeField, Tooltip("Horizontal scroll speed (pixels per second)")] private float _xSpeed = 0.1f; // Horizontal scroll speed
    [SerializeField, Tooltip("Vertical scroll speed (pixels per second)")] private float _ySpeed = 0.1f; // Vertical scroll speed

    private Vector2 _currentOffset; // Tracks the texture offset
    private bool _isScrolling = true; // Whether scrolling is enabled

    /// <summary>
    /// Properties to access and modify the scroll speeds dynamically.
    /// </summary>
    public float XSpeed
    {
        get => _xSpeed;
        set => _xSpeed = value;
    }

    public float YSpeed
    {
        get => _ySpeed;
        set => _ySpeed = value;
    }

    private void Awake()
    {
        ValidateRawImage();
    }

    private void Update()
    {
        if (_isScrolling)
        {
            UpdateScroll();
        }
    }

    /// <summary>
    /// Ensures the RawImage component is assigned and valid.
    /// </summary>
    private void ValidateRawImage()
    {
        if (_img == null)
        {
            Debug.LogError("Scroller: RawImage is not assigned. Please assign it in the Inspector.");
            enabled = false; // Disable the script to prevent further errors
        }
    }

    /// <summary>
    /// Updates the scrolling texture offset based on the speed values and time.
    /// </summary>
    private void UpdateScroll()
    {
        // Increment the offset
        _currentOffset += new Vector2(_xSpeed, _ySpeed) * Time.deltaTime;

        // Apply the offset to the RawImage's UV Rect
        _img.uvRect = new Rect(_currentOffset, _img.uvRect.size);
    }

    /// <summary>
    /// Toggles the scrolling state (on/off).
    /// </summary>
    public void ToggleScrolling()
    {
        _isScrolling = !_isScrolling;
    }

    /// <summary>
    /// Stops the scrolling of the texture.
    /// </summary>
    public void StopScrolling()
    {
        _isScrolling = false;
    }

    /// <summary>
    /// Resumes the scrolling of the texture.
    /// </summary>
    public void ResumeScrolling()
    {
        _isScrolling = true;
    }

    /// <summary>
    /// Dynamically sets the scroll speed.
    /// </summary>
    /// <param name="xSpeed">Horizontal scroll speed (pixels per second).</param>
    /// <param name="ySpeed">Vertical scroll speed (pixels per second).</param>
    public void SetScrollSpeed(float xSpeed, float ySpeed)
    {
        _xSpeed = xSpeed;
        _ySpeed = ySpeed;
    }
}
