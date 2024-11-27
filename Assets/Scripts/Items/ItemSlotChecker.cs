using UnityEngine;
using UnityEngine.Events;

public class ItemSlotChecker : MonoBehaviour
{
    public ItemSlot[] slots;
    public UnityEvent OnAllFilled;

    private void Start()
    {
        ItemSlot.OnSlotFilled += SlotCheck;
    }

    private void OnDisable()
    {
        ItemSlot.OnSlotFilled -= SlotCheck;
    }

    void SlotCheck()
    {
        var filledCount = 0;

        foreach (var s in slots)
        {
            if (s.hasItem)
                filledCount++;
        }

        if (filledCount >= slots.Length)
        {
            OnAllFilled?.Invoke();
        }
    }
}
