using DG.Tweening;
using System;
using UnityEngine;

public class SickleController : MonoBehaviour
{
    [Space(10)]
    [Header("Elements")]
    [Space(10)]
    [SerializeField] private GameObject emptyBedPrefab;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Transform  tool;

    private bool isCutting;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter method called");
        if (other.CompareTag("Test"))
        {
            Transform wheat = other.transform.GetChild(0);
            if (wheat.localScale.x >= 0.3f) // check if wheat has fully grown
            {
                isCutting = true;

                CutWheat(other.gameObject);
            }
        }
    }

    private void CutWheat(GameObject wheat)
    {
        wheat.transform.GetChild(0).DOScale(0f, 0.5f).OnComplete(() =>
        {
            CreateEmptyBed(wheat.transform.position);

            CreateBlock(wheat.transform.position - new Vector3(0, 0, 0.3f));

            // Get a reference to the WheatField component
            WheatField wheatField = GameObject.FindObjectOfType<WheatField>();

            // Call the CollectWheat method in the WheatField component
            wheatField.CollectWheat(wheat);

           
        });
    }

    private void CreateEmptyBed(Vector3 position)
    {
        Instantiate(emptyBedPrefab, position, Quaternion.identity);
    }

    private void CreateBlock(Vector3 position)
    {
        Instantiate(blockPrefab, position, Quaternion.identity);
       
    }
   
}
