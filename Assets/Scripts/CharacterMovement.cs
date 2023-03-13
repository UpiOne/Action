using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Space(10)]
    [Header("Settings for movement")]
    [Space(10)]
    [SerializeField] private float maxSpeed;
    [SerializeField] private Joystick joystick;
 
    [Space(10)]
    [Header("Elements for sickle")]
    [Space(10)]
    [SerializeField] private BoxCollider BoxColliderSickleEvent;
    [SerializeField] private GameObject SickleSwitching;

    private Animator animator;
    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    public void Start()
    {
        BoxColliderSickleEvent.enabled = false;
    }
    private void FixedUpdate()
    {
        var movement = joystick.Movement;

        if (movement != Vector2.zero)
        {
            ApplyVelocity(movement);
            ApplyRotation(movement);
            animator.SetBool("isMoving", true);
        }
        else
        {
            Idle();
            animator.SetBool("isMoving", false);
        }
    }

    private void Idle()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void ApplyRotation(Vector2 movement)
    {
        rigidbody.transform.localRotation = Quaternion.LookRotation(rigidbody.velocity);
    }

    private void ApplyVelocity(Vector2 movement)
    {
        rigidbody.velocity = new Vector3(movement.x, 0, movement.y) * maxSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            Transform wheat = other.transform.GetChild(0);
            if (wheat.localScale.x >= 0.3f) // check if wheat has fully grown
            {
                SickleSwitching.SetActive(true);
                animator.SetBool("IsCutting", true);
            }
        }
        if (other.CompareTag("Test1"))
        {
            SickleSwitching.SetActive(false);
        }
    }

    public void StartWheatCutting()
    {
        BoxColliderSickleEvent.enabled = true;
        Debug.Log("StartAtt");
    }
    public void EndWheatCutting()
    {
       
        BoxColliderSickleEvent.enabled = false;
        animator.SetBool("IsCutting", false);
        Debug.Log("EndAtt");
    }
}