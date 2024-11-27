using UnityEngine;
using System.Collections.Generic;

public class PuzzlePiece : MonoBehaviour
{
    bool isLift;

    Transform recent_parent_target;
    Transform snap_destination;
    public Transform item_container;

    List<PuzzlePiece> neighbour_pieces = new List<PuzzlePiece>();

    Rigidbody2D rigid2d;

    private void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        if (!item_container)
            item_container = GameObject.Find("content").transform;
    }

    private void OnMouseDown()
    {
        isLift = true;
        //snap_destination = null;
        if (neighbour_pieces.Count > 0)
        {
            AudioManager.Instance.PlaySFXClone(AudioManager.Instance.jigsawRemoveSfx);
        }
    }

    private void OnMouseUp()
    {
        isLift = false;
        if (snap_destination != null)
        {
            transform.position = snap_destination.position;
            AudioManager.Instance.PlaySFXClone(AudioManager.Instance.jigsawSnapSfx);

        }

        //snap_destination = null;
    }

    public void RecieveTriggerData(PuzzlePieceConponents comp)
    {
        if (comp)
        {
            recent_parent_target = comp.GetParent().transform;
            snap_destination = comp.SnapPoint;
        }
        else
        {
            recent_parent_target = null;
            snap_destination = null;
        }
    }

    public List<PuzzlePiece> GetNeighbourList()
    {
        return neighbour_pieces;
    }

    public void AddNeighbour(PuzzlePiece piece)
    {
        if (!neighbour_pieces.Contains(piece))
        {
            neighbour_pieces.Add(piece);
        }
    }

    public void RemoveNeighbour(PuzzlePiece piece)
    {
        
        if (neighbour_pieces.Contains(piece))
        {
            neighbour_pieces.Remove(piece); 
        }
    }

    public void MoveToThisContainer(GameObject _item)
    {
        _item.transform.SetParent(item_container);
    }
}
