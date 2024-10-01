using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Deneme : MonoBehaviour
{
    void Start()
    {
        // Store the initial position
        Vector3 initialPosition = transform.position;

        // Create a new sequence
        Sequence sequence = DOTween.Sequence();

        // Add movement tween to the sequence
        sequence.Append(transform.DOMoveY(initialPosition.y + 0.05f, 1f).SetEase(Ease.InOutSine))
                .Append(transform.DOMoveY(initialPosition.y, 1f).SetEase(Ease.InOutSine))
                .SetLoops(-1, LoopType.Yoyo);

        // Add rotation tween to the sequence
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
    }
}
