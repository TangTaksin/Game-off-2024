using UnityEngine;
using UnityEngine.Events;

public class Item_Interact : MonoBehaviour
{
    public UnityEvent OnInteract;
    public string ItemToDetect;

    public void Interact(Item item)
    {
        if (item.itemName == ItemToDetect)
        {
            print("interact");
            OnInteract?.Invoke();
        }
    }
}
