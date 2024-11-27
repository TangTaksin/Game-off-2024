using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemNameDisplay : MonoBehaviour
{
    public Image bracket;
    Animator bracket_animator;

    public TextMeshProUGUI textUI;
    Camera main_cam;

    private void OnEnable()
    {
        main_cam = Camera.main;
        bracket_animator = bracket.GetComponent<Animator>();
        bracket.gameObject.SetActive(false);

        Item.OnItemEnterEvent += OnItemEnter;
        Item.OnItemExitEvent += OnItemExit;
    }

    private void OnDisable()
    {
        Item.OnItemEnterEvent -= OnItemEnter;
        Item.OnItemExitEvent -= OnItemExit;
    }

    private void Update()
    {
        if (textUI.gameObject.activeSelf)
            transform.position = Input.mousePosition;

    }

    void OnItemEnter(string item_name)
    {
        bracket.gameObject.SetActive(true);
        var remove = "(Clone)";
        var result = item_name.Replace(remove, "");
        textUI.text = result;
    }

    void OnItemExit(string item_name)
    {
        bracket_animator.Play("item_name_display_exit");
    }
}
