using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    [Space(10)]
    [Header("Settings")]
    [Space(10)]
    [SerializeField] private float radius;
    [SerializeField] private Image joystickZoneImage;
    [SerializeField] private Image joystickImage;

    private bool touchStart = false;

    private RectTransform touchArea;
  
    private Vector2 startPosition;
    private Vector2 touchPosition;
    private Vector2 joystickPosition;

    public Vector2 Movement { get; private set; }

    private void Awake()
    {
        ChangeVisibility(false);
        touchArea = GetComponent<RectTransform>();
        radius = joystickZoneImage.rectTransform.sizeDelta.x;

    }
    private void Start()
    {
        joystickImage.rectTransform.position = startPosition;
    }
    private void Update()
    {

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (!touchStart && RectTransformUtility.RectangleContainsScreenPoint(touchArea, touch.position))
            {
                touchStart = true;
            }

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    joystickImage.rectTransform.position = startPosition;
                    startPosition = touch.position;
                    ChangeVisibility(true);
                    joystickZoneImage.rectTransform.position = touch.position;
                    break;

                case TouchPhase.Moved:

                    touchPosition = touch.position;
                    joystickPosition = Vector2.ClampMagnitude(touchPosition - startPosition, radius);
                    Movement = joystickPosition.normalized;
                    joystickImage.rectTransform.position = joystickPosition + startPosition;
                    break;
            }

        }
        else
        {

            ChangeVisibility(false);
            touchStart = false;
            Movement = Vector3.zero;

        }
    }

    private void ChangeVisibility(bool Visibility)
    {
        joystickZoneImage.enabled = Visibility;
        joystickImage.enabled = Visibility;
    }

    private void OnDisable()
    {
        Movement = Vector2.zero;
    }
}
