using UnityEngine;

public class Item : MonoBehaviour
{
    PuzzlePiece current_parent_piece, new_parent_piece;
    bool left_current_piece;
    Vector3 last_pos;

    private void OnMouseDown()
    {
        // check current piece
        var col = Physics2D.OverlapCircle(transform.position, .1f, LayerMask.GetMask("PuzzlePiece"));
        if (col)
            current_parent_piece = col.GetComponent<PuzzlePiece>();

        last_pos = transform.position;
    }

    private void OnMouseUp()
    {
        var col = Physics2D.OverlapCircle(transform.position, .1f, LayerMask.GetMask("PuzzlePiece"));
        if (col)
            new_parent_piece = col.GetComponent<PuzzlePiece>();

        var isNeighbour = current_parent_piece.GetNeighbourList().Contains(new_parent_piece);
        print(isNeighbour);

        if (left_current_piece && !isNeighbour)
            transform.position = last_pos;
        else
        {
            new_parent_piece.MoveToThisContainer(this.gameObject);
            current_parent_piece = new_parent_piece;
            left_current_piece = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (current_parent_piece && collision.gameObject == current_parent_piece.gameObject)
        {
            print("return to current piece");
            left_current_piece = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (current_parent_piece && collision.gameObject == current_parent_piece.gameObject)
        {
            print("left current piece");
            left_current_piece = true;
        }
    }

}
