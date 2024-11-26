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

    private Rect _uvRect; // Cache the uvRect to avoid redundant property calls

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
        if (_img != null)
        {
            _uvRect = _img.uvRect; // Initialize the cached uvRect
        }
    }

    private void FixedUpdate()
    {
        if (_isScrolling)
        {
            UpdateScroll();
        }
    }

    private void ValidateRawImage()
    {
        if (_img == null)
        {
            Debug.LogError("Scroller: RawImage is not assigned. Please assign it in the Inspector.");
            enabled = false;
        }
    }

    private void UpdateScroll()
    {
        // Increment the offset
        _currentOffset.x += _xSpeed * Time.deltaTime;
        _currentOffset.y += _ySpeed * Time.deltaTime;

        // Only update uvRect if necessary
        _uvRect.position = _currentOffset;
        _img.uvRect = _uvRect;
    }

    public void ToggleScrolling() => _isScrolling = !_isScrolling;

    public void StopScrolling() => _isScrolling = false;

    public void ResumeScrolling() => _isScrolling = true;

    public void SetScrollSpeed(float xSpeed, float ySpeed)
    {
        _xSpeed = xSpeed;
        _ySpeed = ySpeed;
    }
}
