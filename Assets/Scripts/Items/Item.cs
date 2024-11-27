using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public bool disable_on_combine;

    PuzzlePiece current_parent_piece, new_parent_piece;
    bool left_current_piece;

    Vector3 last_pos;

    private void Awake()
    {
        var col = Physics2D.OverlapCircle(transform.position, .1f, LayerMask.GetMask("PuzzlePiece"));
        if (col)
        {
            current_parent_piece = col.GetComponent<PuzzlePiece>();
            current_parent_piece.MoveToThisContainer(gameObject);
        }
    }

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
        var piece = Physics2D.OverlapCircle(transform.position, .1f, LayerMask.GetMask("PuzzlePiece"));
        if (piece)
        {
            new_parent_piece = piece.GetComponent<PuzzlePiece>();
        }

        var isNeighbour = false;
        if (current_parent_piece)
            isNeighbour = current_parent_piece.GetNeighbourList().Contains(new_parent_piece);

        if (left_current_piece && !isNeighbour)
        {
            transform.position = last_pos;
        }
        else if (piece)
        {
            new_parent_piece.MoveToThisContainer(this.gameObject);
            current_parent_piece = new_parent_piece;
            left_current_piece = false;
        }

        if (left_current_piece)
            return;

        var detect_item = Physics2D.OverlapCircleAll(transform.position, .25f, LayerMask.GetMask("Item"));
        var print_items = "";
        foreach (var i in detect_item)
        {
            print_items += i.gameObject;
        }

        print(print_items);

        if (detect_item.Length > 1)
        {
            var inter = detect_item[0].GetComponent<Item_Interact>();
            if (inter)
            {
                inter.Interact(this);
            }

            var item = detect_item[0].GetComponent<Item>();
            if (item && item != this)
            {
                print("attempt to combine "+this+item);
                var result = CombinationManager.Combine(this, item);

                if (result)
                {
                    result.transform.position = item.transform.position;
                    Instantiate(result);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (current_parent_piece && collision.gameObject == current_parent_piece.gameObject)
        {
            left_current_piece = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (current_parent_piece && collision.gameObject == current_parent_piece.gameObject)
        {
            left_current_piece = true;
        }
    }
}
