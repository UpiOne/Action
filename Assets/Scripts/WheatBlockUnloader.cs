using UnityEngine;
using DG.Tweening;

public class WheatBlockUnloader : MonoBehaviour
{
    [Space(10)]
    [Header("Settings")]
    [Space(10)]
    [SerializeField] private float unloadDelay = 0.2f;
    [SerializeField] private float maxSpeedBlocks = 10f;

    [Space(10)]
    [Header("Target tranform for sell blocks")]
    [Space(10)]
    [SerializeField] private Transform targetTransformBlocks;
  

    private StackBlocks stacker;

    private void Start()
    {
        stacker = FindObjectOfType<StackBlocks>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Current stack count: " + stacker.currentStackCount);
        if (other.gameObject.CompareTag("WheatBlock"))
        {
            GameObject wheatBlock = other.gameObject;
            Debug.Log("Detected wheat block: " + wheatBlock.name);

            // Disable the collider of the wheat block
            wheatBlock.GetComponent<Collider>().enabled = false;

            // Calculate the duration based on the distance to the target and the speed
            float distance = Vector3.Distance(wheatBlock.transform.position, targetTransformBlocks.transform.position);
            float duration = distance / maxSpeedBlocks;

            // Move the wheat block towards the target transform using DOTween.To
            Vector3 startPosition = wheatBlock.transform.position;
            DOTween.To(() => startPosition, x => wheatBlock.transform.position = x, targetTransformBlocks.position, duration)
                   .OnComplete(() =>
                   {
                       // Enable the collider of the wheat block
                       wheatBlock.GetComponent<Collider>().enabled = true;

                       // Reset the stack count to 0
                       stacker.currentStackCount = 0;
                       UIManager.Instance.blockCountText.text = stacker.currentStackCount + "/" + stacker.maxStackCount;
                       Debug.Log("Current stack count: " + stacker.currentStackCount);
                   });
        }
    }
}
