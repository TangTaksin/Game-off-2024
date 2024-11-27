using UnityEngine;
using TMPro;

public class ItemNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI textUI;

    public delegate void DisplayEvent(string item_name);
    public static DisplayEvent OnItemClickEvent;

    private void OnEnable()
    {
        OnItemClickEvent += OnItemClick;
    }

    private void OnDisable()
    {
        OnItemClickEvent -= OnItemClick;
    }

    void OnItemClick(string item_name)
    {
        textUI.gameObject.SetActive(true);
        textUI.text = item_name;
    }
}
