using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private float offsetDef = 0.085f;
    public bool isTalking;

    //With item display
    public static ChatBubble Create(Transform parent,SO_Item item,string text)
    {
        Transform chatBubbleTransform = Instantiate(GameManager.Instance.chatBubble, parent);
        //offset for bubble on character
        chatBubbleTransform.localPosition = new Vector3(0,2,0);
        chatBubbleTransform.GetComponent<ChatBubble>().Setup(item, text,chatBubbleTransform);
        Destroy(chatBubbleTransform.gameObject, 5f);
        return chatBubbleTransform.GetComponent<ChatBubble>();
    }
    //Without item display
    public static ChatBubble Create(Transform parent, string text)
    {
        Transform chatBubbleTransform = Instantiate(GameManager.Instance.chatBubble, parent);
        //offset for bubble on character
        chatBubbleTransform.localPosition = new Vector3(0, 2, 0);
        chatBubbleTransform.GetComponent<ChatBubble>().Setup(text, chatBubbleTransform);
        Destroy(chatBubbleTransform.gameObject, 5f);
        return chatBubbleTransform.GetComponent<ChatBubble>();
    }


    public SpriteRenderer backgroundSpriteRenderer;
    public SpriteRenderer iconSpriteRenderer;
    public TextMeshPro textMeshPro;
    public TextMeshPro fakeTextMeshPro;

    //With item display
    private void Setup(SO_Item _item, string text,Transform bubble)
    {
        //Getting values from faketext so user cant see the full text in first frame.
        fakeTextMeshPro.SetText(text);
        fakeTextMeshPro.ForceMeshUpdate();
        Vector2 textSize = fakeTextMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2((iconSpriteRenderer.size.x * iconSpriteRenderer.transform.localScale.x) + offsetDef, 0.1f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(-iconSpriteRenderer.size.x / 2 * iconSpriteRenderer.transform.localScale.x, 0);
        backgroundSpriteRenderer.transform.localPosition = new Vector3((backgroundSpriteRenderer.size.x - (iconSpriteRenderer.size.x * iconSpriteRenderer.transform.localScale.x) - (offsetDef / 2)) / 2f, 0f) + offset;

        //Adjusting bubble position to middle of the character
        bubble.localPosition = new Vector3(-backgroundSpriteRenderer.transform.localPosition.x, bubble.localPosition.y, bubble.localPosition.z);
        iconSpriteRenderer.sprite = _item._itemSprite;
        //Setting chid objects parent to bacground. LookatCamera script rotating background
        iconSpriteRenderer.gameObject.transform.SetParent(backgroundSpriteRenderer.transform);
        textMeshPro.gameObject.transform.SetParent(backgroundSpriteRenderer.transform);
        fakeTextMeshPro.gameObject.transform.SetParent(backgroundSpriteRenderer.transform);

        isTalking = true;
        TextWriter.AddWriter_Static(textMeshPro, text, 0.03f, true, true, () => {

            isTalking = false;
        });
    }
    //Without item display
    private void Setup(string text,Transform bubble)
    {
        //Getting values from faketext so user cant see the full text in first frame.
        fakeTextMeshPro.SetText(text);
        fakeTextMeshPro.ForceMeshUpdate();
        Vector2 textSize = fakeTextMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(offsetDef, 0.1f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(-offsetDef, 0);
        backgroundSpriteRenderer.transform.localPosition = new Vector3((backgroundSpriteRenderer.size.x / 2f) + 0.03f, 0f) + offset;

        //Adjusting bubble position to middle of the character
        bubble.localPosition = new Vector3(-backgroundSpriteRenderer.transform.localPosition.x, bubble.localPosition.y, bubble.localPosition.z);

        //Setting chid objects parent to bacground. LookatCamera script rotating background
        iconSpriteRenderer.gameObject.transform.SetParent(backgroundSpriteRenderer.transform);
        textMeshPro.gameObject.transform.SetParent(backgroundSpriteRenderer.transform);
        fakeTextMeshPro.gameObject.transform.SetParent(backgroundSpriteRenderer.transform);

        iconSpriteRenderer.gameObject.SetActive(false);


        isTalking = true;
        TextWriter.AddWriter_Static(textMeshPro, text, 0.05f, true, true, () => {

            isTalking = false;

        });
    }
}
