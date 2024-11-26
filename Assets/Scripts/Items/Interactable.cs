using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnClick;

    private void OnMouseDown()
    {
        OnClick?.Invoke();
    }
}
