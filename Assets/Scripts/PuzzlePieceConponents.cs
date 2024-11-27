using UnityEngine;

public class PuzzlePieceConponents : MonoBehaviour
{
    public enum component_type
    {
        Indent,
        Tab
    }
    public component_type type;
    public string connect_code;
    public Transform SnapPoint;
    PuzzlePiece parent_piece;

    bool _code_matched;
    public bool code_matched
    {
        get { return _code_matched; }
        set
        {
            if (_code_matched != value)
            {
                _code_matched = value;
                OnCodeMatch(_code_matched);
            }
        }
    }

    GameObject detected_comp_obj;
    PuzzlePieceConponents detected_comp;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        parent_piece = GetComponentInParent<PuzzlePiece>();
    }

    private void Update()
    {
        sr.color = code_matched ? Color.green : Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var comp = collision.GetComponent<PuzzlePieceConponents>();
        if (comp)
        {
            detected_comp = comp;
            detected_comp_obj = detected_comp.gameObject;
        }
        else
        {
            detected_comp = null;
            detected_comp_obj = null;
        }

        CodeCheck();
    }

    void CodeCheck()
    {
        if ((detected_comp && detected_comp.type != type) &&
            detected_comp.connect_code == connect_code)
        {
            code_matched = true;
            parent_piece.AddNeighbour(detected_comp.GetParent());
        }
        else
            code_matched = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var comp = collision.GetComponent<PuzzlePieceConponents>();

        if (comp && comp == detected_comp)
        {
            code_matched = false;
            parent_piece.RemoveNeighbour(detected_comp.GetParent());
        }
    }

    void OnCodeMatch(bool value)
    {
        if (value)
            parent_piece.RecieveTriggerData(detected_comp);
        else
            parent_piece.RecieveTriggerData(null);
    }

    public PuzzlePiece GetParent()
    {
        return parent_piece;
    }
}
