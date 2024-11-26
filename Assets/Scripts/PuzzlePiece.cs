using UnityEngine;
using System.Collections.Generic;

public class PuzzlePiece : MonoBehaviour
{
    bool isLift;

    bool leftBound;
    Vector3 lastPos;

    Transform recent_parent_target;
    Transform snap_destination;
    public Transform item_container;

    List<PuzzlePiece> neighbour_pieces = new List<PuzzlePiece>();

    Rigidbody2D rigid2d;

    public delegate void PieceEvent(PuzzlePiece piece);
    public PieceEvent OnNeighbourAdd, OnNeighbourRemove;

    private void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        if (!item_container)
            item_container = GameObject.Find("content").transform;
    }

    private void OnMouseDown()
    {
        lastPos = transform.position;
        isLift = true;
        //snap_destination = null;
    }

    private void OnMouseUp()
    {
        isLift = false;
        if (snap_destination != null)
        {
            transform.position = snap_destination.position;
        }

        if (leftBound)
        transform.position = lastPos;
        //snap_destination = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bound"))
        {
            leftBound = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bound"))
        {
            leftBound = true;
        }
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
            GetNeighbour(piece);
            OnNeighbourAdd?.Invoke(piece);
        }
    }

    public void RemoveNeighbour(PuzzlePiece piece)
    {
        if (neighbour_pieces.Contains(piece))
        {
            neighbour_pieces.Remove(piece);

            OnNeighbourRemove?.Invoke(piece);
        }
    }

    public void MoveToThisContainer(GameObject _item)
    {
        _item.transform.SetParent(item_container);
    }

    void GetNeighbour(PuzzlePiece piece)
    {
        var other_neighbour = piece.neighbour_pieces;
        
    }
}