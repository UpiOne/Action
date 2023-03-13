using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatBlockStack : MonoBehaviour
{
    [Space(10)]
    [Header("Settings")]
    [Space(10)]
  
    [SerializeField] private float swayAngle = 10f;
    [SerializeField] private float swayDuration = 0.5f;
    [Space(10)]
    [SerializeField] private Transform characterTransform;

    private Vector3 initialRotation;

    void Start()
    {
        initialRotation = transform.localEulerAngles;
    }

    void Update()
    {
        float characterMovement = Mathf.Abs(characterTransform.position.x) + Mathf.Abs(characterTransform.position.z);
        float swayAmount = Mathf.Clamp01(characterMovement / 10f);
        float swayRotation = Mathf.Lerp(-swayAngle, swayAngle, swayAmount);

        transform.DOLocalRotate(initialRotation + new Vector3(swayRotation, 0f, swayRotation), swayDuration);
    }
}
