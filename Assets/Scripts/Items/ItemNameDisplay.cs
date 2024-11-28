using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ItemNameDisplay : MonoBehaviour
{
    public Image bracket;
    RectTransform backet_tranform;
    Animator bracket_animator;

    public TextMeshProUGUI textUI;
    Camera main_cam;

    public float name_display_decay = .25f;
    float decay_timer;
    bool exited;

    private void OnEnable()
    {
        main_cam = Camera.main;
        bracket_animator = bracket.GetComponent<Animator>();
        backet_tranform = bracket.GetComponent<RectTransform>();
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
        {
            transform.position = Input.mousePosition;
            var boxwidth = backet_tranform.sizeDelta.x;
            backet_tranform.anchoredPosition = new Vector3(boxwidth / 2, backet_tranform.anchoredPosition.y);

            if (exited)
            {
                decay_timer -= Time.deltaTime;
                if (decay_timer <= 0)
                {
                    exited = false;
                    bracket_animator.Play("item_name_display_exit");
                }
            }
        }
    }

    void OnItemEnter(string item_name)
    {
        exited = false;

        bracket_animator.Play("item_name_display_entry");
        bracket.gameObject.SetActive(true);

        var remove = "(Clone)";
        var result = item_name.Replace(remove, "");
        textUI.text = result;
    }

    void OnItemExit(string item_name)
    {
        exited = true;
        decay_timer = name_display_decay;
    }
}
