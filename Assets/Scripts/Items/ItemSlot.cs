using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public SpriteRenderer item_renderer;
    public bool hasItem;

    public delegate void SlotEvent();
    public static SlotEvent OnSlotFilled;

    private void Start()
    {
        item_renderer?.gameObject.SetActive(false);
    }

    public void SetItem()
    {
        print("Item Set");
        hasItem = true;
        item_renderer?.gameObject.SetActive(hasItem);
        OnSlotFilled?.Invoke();
    }
}
