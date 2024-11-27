using UnityEngine;
using UnityEngine.Events;

public class Item_Interact : MonoBehaviour
{
    public UnityEvent OnInteract;
    public string ItemToDetect;

    public bool Interact(Item item)
    {
        if (item.itemName == ItemToDetect)
        {
            print("interact");
            OnInteract?.Invoke();

            return true;
        }

        return false;
    }
}
