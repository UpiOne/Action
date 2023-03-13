using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackBlocks : MonoBehaviour
{
    public static StackBlocks Instance;

    [Space(10)]
    [Header("Settings")]
    [Space(10)]
    [SerializeField] private Transform stackTransform;
    [Space(10)]
    public int maxStackCount = 40;
    public int currentStackCount = 0;

    [SerializeField] private int stackRows = 3;
    [SerializeField] private int stackColumns = 3;
    [SerializeField] private float stackSpacing = 0.1f;
   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddWheatBlock(GameObject wheatBlock)
    {
        // Check if the stack is full
        if (currentStackCount >= maxStackCount)
        {
            Debug.LogWarning("Cannot add apple block to full stack");
            return;
        }
        // Check if current stack is empty and reset if necessary
        if (currentStackCount == 0)
        {
            Debug.Log("Current stack is empty, resetting...");
            currentStackCount = 0;
        }
        // Add the apple block to the stack
        currentStackCount++;


        int currentRow = (currentStackCount - 1) / stackColumns; // calculate row from bottom to top
        int currentColumn = (currentStackCount - 1) % stackColumns; // calculate column from left to right

        Vector3 stackPosition = new Vector3(currentColumn * stackSpacing - ((stackColumns - 1) * stackSpacing) / 2f, currentRow * stackSpacing, 0f);

        wheatBlock.transform.SetParent(stackTransform);
        wheatBlock.transform.localPosition = stackPosition;
        wheatBlock.transform.localRotation = Quaternion.identity;

        wheatBlock.transform.DOLocalMove(stackPosition, 0.5f);
        Debug.Log("Added apple block: " + wheatBlock.name);
        UIManager.Instance.blockCountText.text = currentStackCount + "/" + maxStackCount;
        Debug.Log("Current stack count after adding: " + currentStackCount);

        if (currentStackCount == maxStackCount)
        {
            UIManager.Instance.MaxCountText.SetActive(true);
        }
        else
        {
            UIManager.Instance.MaxCountText.SetActive(false);
        }
    }
    public bool IsStackFull()
    {
        return currentStackCount >= maxStackCount;
    }
}
