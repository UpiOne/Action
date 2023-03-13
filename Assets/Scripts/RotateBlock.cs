using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlock : MonoBehaviour
{
    private const float ROTATION_DURATION = 1.5f;
    private const float RAISE_DURATION = 1.5f;
    private const float RAISE_AMOUNT = 1.5f;

    private void Start()
    {
        DoAnimation();
    }
    void DoAnimation()
    {
        // Rotate the GameObject 360 degrees over 1 second
        transform.DORotate(new Vector3(0f, 360f, 0f), ROTATION_DURATION, RotateMode.FastBeyond360)
            // Raise the GameObject by 0.5 units over 0.5 seconds after the rotation completes
            .OnComplete(() => transform.DOMoveY(transform.position.y + RAISE_AMOUNT, RAISE_DURATION)).SetLoops(-1);
    }
}
