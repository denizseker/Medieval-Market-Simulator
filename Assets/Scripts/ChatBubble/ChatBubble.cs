using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private const float OffsetDef = 0.085f;
    private const float TextWriteSpeedWithItem = 0.03f;
    private const float TextWriteSpeedWithoutItem = 0.02f;
    private static readonly Vector3 BubbleOffset = new Vector3(0, 2, 0);

    public bool isTalking;
    private System.Action onDestroyCallback;

    public SpriteRenderer backgroundSpriteRenderer;
    public SpriteRenderer iconSpriteRenderer;
    public TextMeshPro textMeshPro;
    public TextMeshPro fakeTextMeshPro;

    // With item display
    public static ChatBubble Create(Transform parent, SO_Item item, string text, float destroyDelay, System.Action onDestroyCallback = null)
    {
        var chatBubbleTransform = Instantiate(GameManager.Instance.chatBubble, parent);
        chatBubbleTransform.localPosition = BubbleOffset;
        var chatBubble = chatBubbleTransform.GetComponent<ChatBubble>();
        chatBubble.Setup(item, text, destroyDelay, onDestroyCallback);
        return chatBubble;
    }

    // Without item display
    public static ChatBubble Create(Transform parent, string text, float destroyDelay, System.Action onDestroyCallback = null)
    {
        var chatBubbleTransform = Instantiate(GameManager.Instance.chatBubble, parent);
        chatBubbleTransform.localPosition = BubbleOffset;
        var chatBubble = chatBubbleTransform.GetComponent<ChatBubble>();
        chatBubble.Setup(text,destroyDelay, onDestroyCallback);
        return chatBubble;
    }

    // With item display
    private void Setup(SO_Item item, string text, float destroyDelay,System.Action onDestroyCallback)
    {
        this.onDestroyCallback = onDestroyCallback;
        SetupFakeText(text);
        var textSize = fakeTextMeshPro.GetRenderedValues(false);
        var padding = new Vector2((iconSpriteRenderer.size.x * iconSpriteRenderer.transform.localScale.x) + OffsetDef, 0.1f);
        backgroundSpriteRenderer.size = textSize + padding;

        var offset = new Vector3(-iconSpriteRenderer.size.x / 2 * iconSpriteRenderer.transform.localScale.x, 0);
        backgroundSpriteRenderer.transform.localPosition = new Vector3((backgroundSpriteRenderer.size.x - (iconSpriteRenderer.size.x * iconSpriteRenderer.transform.localScale.x) - (OffsetDef / 2)) / 2f, 0f) + offset;

        AdjustBubblePosition();

        iconSpriteRenderer.sprite = item._itemSprite;
        SetChildObjectsParent();

        isTalking = true;
        TextWriter.AddWriter_Static(textMeshPro, text, TextWriteSpeedWithItem, true, true, () =>
        {
            isTalking = false;
            StartCoroutine(DestroyAfterDelay(destroyDelay));
        });
    }

    // Without item display
    private void Setup(string text,float destroyDelay, System.Action onDestroyCallback)
    {
        this.onDestroyCallback = onDestroyCallback;
        SetupFakeText(text);
        var textSize = fakeTextMeshPro.GetRenderedValues(false);
        var padding = new Vector2(OffsetDef, 0.1f);
        backgroundSpriteRenderer.size = textSize + padding;

        var offset = new Vector3(-OffsetDef, 0);
        backgroundSpriteRenderer.transform.localPosition = new Vector3((backgroundSpriteRenderer.size.x / 2f) + 0.03f, 0f) + offset;

        AdjustBubblePosition();

        SetChildObjectsParent();
        iconSpriteRenderer.gameObject.SetActive(false);

        isTalking = true;
        TextWriter.AddWriter_Static(textMeshPro, text, TextWriteSpeedWithoutItem, true, true, () =>
        {
            isTalking = false;
            StartCoroutine(DestroyAfterDelay(destroyDelay));
        });
    }

    private void SetupFakeText(string text)
    {
        fakeTextMeshPro.SetText(text);
        fakeTextMeshPro.ForceMeshUpdate();
    }

    private void AdjustBubblePosition()
    {
        transform.localPosition = new Vector3(-backgroundSpriteRenderer.transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

    private void SetChildObjectsParent()
    {
        iconSpriteRenderer.gameObject.transform.SetParent(backgroundSpriteRenderer.transform);
        textMeshPro.gameObject.transform.SetParent(backgroundSpriteRenderer.transform);
        fakeTextMeshPro.gameObject.transform.SetParent(backgroundSpriteRenderer.transform);
    }

    private IEnumerator DestroyAfterDelay(float destroyDelay)
    {
        yield return new WaitForSeconds(destroyDelay);
        onDestroyCallback?.Invoke();
        Destroy(gameObject);
    }
}
