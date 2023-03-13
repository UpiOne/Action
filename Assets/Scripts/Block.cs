using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Space(10)]
    [Header("Settings")]
    [Space(10)]
    [SerializeField] private float sellDelay = 2f;
    public int coinValue = 15;

    private bool isSold = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isSold && other.CompareTag("Barn"))
        {
            isSold = true;
            StartCoroutine(SellBlock());
        }
    }

    private IEnumerator SellBlock()
    {
        yield return new WaitForSeconds(sellDelay);

        SellManager.Instance.SellBlock(this);
    }
}
