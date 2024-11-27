using UnityEngine;

public class CameraBound : MonoBehaviour
{
    Camera main_cam;
    BoxCollider2D box2D;

    private void Start()
    {
        main_cam = Camera.main;
        box2D = GetComponent<BoxCollider2D>();

        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        var width = Mathf.Abs(bottomLeft.x - topRight.x);
        var height = Mathf.Abs(bottomLeft.y - topRight.y);

        box2D.size = new Vector2(width, height);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print(collision.gameObject);
    }
}
