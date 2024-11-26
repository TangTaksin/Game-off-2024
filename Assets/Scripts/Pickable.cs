using UnityEngine;

public class Pickable : MonoBehaviour
{
    Camera _cam;
    Rigidbody2D _rb;

    Vector3 mouseOffset;

    public bool moveViaRigidbody;

    private void Start()
    {
        if (!_cam)
            _cam = Camera.main;

        _rb = GetComponent<Rigidbody2D>();
    }

    #region Mouse Hover

    private void OnMouseEnter()
    {
        //print("Entered : " + gameObject.name);

    }

    private void OnMouseOver()
    {

    }

    private void OnMouseExit()
    {

    }

    #endregion

    #region Mouse Drag

    private void OnMouseDown()
    {
        var mouseWorldPosition = _cam.ScreenToWorldPoint(Input.mousePosition);

        mouseOffset = transform.position - mouseWorldPosition;

    }

    private void OnMouseDrag()
    {
        var mouseWorldPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        var movemet = mouseWorldPosition + mouseOffset;

        if (moveViaRigidbody && _rb)
            _rb.MovePosition(movemet);
        else
            transform.position = movemet;
    }

    private void OnMouseUp()
    {

    }

    #endregion
}
