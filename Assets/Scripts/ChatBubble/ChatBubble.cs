using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{

    public bool isTalking;

    //With item display
    public static ChatBubble Create(Transform parent,SO_Item item,string text)
    {
        Transform chatBubbleTransform = Instantiate(GameManager.Instance.chatBubble, parent);
        //offset for bubble on character
        chatBubbleTransform.localPosition = new Vector3(0,2,0);
        chatBubbleTransform.GetComponent<ChatBubble>().Setup(item, text);
        Destroy(chatBubbleTransform.gameObject, 5f);
        return chatBubbleTransform.GetComponent<ChatBubble>();
    }
    //Without item display
    public static ChatBubble Create(Transform parent, string text)
    {
        Transform chatBubbleTransform = Instantiate(GameManager.Instance.chatBubble, parent);
        //offset for bubble on character
        chatBubbleTransform.localPosition = new Vector3(0, 2, 0);
        chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);
        Destroy(chatBubbleTransform.gameObject, 5f);
        return chatBubbleTransform.GetComponent<ChatBubble>();
    }


    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;
    private TextMeshPro fakeTextMeshPro;


    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("ItemPNG").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        fakeTextMeshPro = transform.Find("FakeText").GetComponent<TextMeshPro>();
    }

    //With item display
    private void Setup(SO_Item _item, string text)
    {
        //Getting values from faketext so user cant see the full text in first frame.
        fakeTextMeshPro.SetText(text);
        fakeTextMeshPro.ForceMeshUpdate();
        Vector2 textSize = fakeTextMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(2f, 0.8f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(-2.6f, 0f);
        backgroundSpriteRenderer.transform.localPosition = new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;

        iconSpriteRenderer.sprite = _item._itemSprite;
        isTalking = true;
        TextWriter.AddWriter_Static(textMeshPro, text, 0.03f, true, true, () => {

            isTalking = false;
        });
    }
    //Without item display
    private void Setup(string text)
    {
        //Getting values from faketext so user cant see the full text in first frame.
        fakeTextMeshPro.SetText(text);
        fakeTextMeshPro.ForceMeshUpdate();
        Vector2 textSize = fakeTextMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(1.5f, 0.8f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(-1.9f, 0f);
        backgroundSpriteRenderer.transform.localPosition = new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;

        iconSpriteRenderer.gameObject.SetActive(false);
        isTalking = true;
        TextWriter.AddWriter_Static(textMeshPro, text, 0.05f, true, true, () => {

            isTalking = false;

        });
    }
}
