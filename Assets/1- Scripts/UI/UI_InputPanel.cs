using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputPanel : MonoBehaviour
{

    public static UI_InputPanel Instance;

    private void Awake()
    {
        Instance = this;
        HideInputPanel();
    }

    public RectTransform itemPNG;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI costValueText;
    public TMP_InputField priceInput;
    public Button okayButton;


    private void ShowInputPanel(Item _item)
    {
        PlayerController.Instance.ActivateUIMode();
        itemPNG.GetComponent<Image>().sprite = _item._SOItem._itemSprite;
        itemNameText.SetText(_item.itemName);
        costValueText.SetText("0");
        priceInput.SetTextWithoutNotify(_item.itemPrice.ToString());
        gameObject.SetActive(true);
    }
    private void HideInputPanel()
    {
        gameObject.SetActive(false);
    }

    public static void ShowInputPanel_Static(Item _item)
    {
        Instance.ShowInputPanel(_item);
    }
    public static void HideInputPanel_Static()
    {
        Instance.HideInputPanel();
    }

    public void ButtonClicked()
    {
        PlayerController.Instance.DeactivateUIMode();
        gameObject.SetActive(false);
    }
}
