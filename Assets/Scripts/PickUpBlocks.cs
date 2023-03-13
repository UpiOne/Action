using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PickUpBlocks : MonoBehaviour
{
    public StackBlocks stacker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WheatBlock") && !stacker.IsStackFull())
        {
            stacker.AddWheatBlock(other.gameObject);
            other.gameObject.SetActive(true);
            // Disable the rotation animation
            RotateBlock rotateBlock = other.gameObject.GetComponent<RotateBlock>();
            if (rotateBlock != null)
            {
                rotateBlock.enabled = false;
            }

            // Stop any running DoTween animations
            Transform transform = other.gameObject.GetComponent<Transform>();
            if (transform != null)
            {
                transform.DOKill();
            }
        }
    }
}
