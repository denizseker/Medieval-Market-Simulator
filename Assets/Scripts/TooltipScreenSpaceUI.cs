using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipScreenSpaceUI : MonoBehaviour
{

    public static TooltipScreenSpaceUI Instance { get; set; }


    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private Vector2 offset;
    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;
    private void Awake()
    {
        Instance = this;
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();
        HideTooltip();
    }


    private void SetText(string itemName, string itemDesc, Item.ItemType itemType, int itemPrice)
    {

        string tooltipText = $"Name: {itemName} \nType: {itemType} \nPrice: {itemPrice} \nDescription: {itemDesc}";

        textMeshPro.SetText(tooltipText);

        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 paddingSize = new Vector2(8, 8);


        backgroundRectTransform.sizeDelta = textSize + paddingSize;

        rectTransform.anchoredPosition = new Vector2(canvasRectTransform.rect.width, canvasRectTransform.rect.height) / 2 + offset;

    }

    private void ShowTooltip(string itemName, string itemDesc, Item.ItemType itemType, int itemPrice)
    {
        gameObject.SetActive(true);
        SetText(itemName, itemDesc, itemType, itemPrice);
    }
    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string itemName, string itemDesc, Item.ItemType itemType, int itemPrice)
    {
        Instance.ShowTooltip(itemName, itemDesc, itemType, itemPrice);
    }
    public static void HideTooltip_Static()
    {
        Instance.HideTooltip();
    }
}
