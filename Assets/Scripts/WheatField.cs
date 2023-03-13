using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatField : MonoBehaviour
{
    [Space(10)]
    [Header("Settings Counts wheats")]
    [Space(10)]
    [SerializeField] private int wheatCount = 0;
    [SerializeField] private int collectedCount = 0;
    [SerializeField] private float growthDuration = 10f;

    [Space(10)]
    [Header("Elements ")]
    [Space(10)]
    [SerializeField] private GameObject gardenBedPrefab;
    [SerializeField] private GameObject wheatPrefab;
    [SerializeField] private List<Transform> gardenTransforms;
    [SerializeField] private List<GameObject> gardenBeds = new List<GameObject>();
    

    private void Start()
    {
        foreach (Transform t in gardenTransforms)
        {
            GameObject bed = Instantiate(gardenBedPrefab, t.position, t.rotation);
            gardenBeds.Add(bed);
        }

        // Spawn all the wheat prefabs and keep track of their count
        foreach (GameObject bed in gardenBeds)
        {
            Transform wheatTransform = bed.transform.GetChild(0);
            GameObject wheat = Instantiate(wheatPrefab, wheatTransform.position, wheatTransform.rotation);
            wheat.SetActive(false);
            wheatCount++;
        }
    }

    public void StartGrowth(GameObject bed)
    {
        Transform wheat = bed.transform.GetChild(0);

        if (wheat.childCount > 0)
        {
            Debug.Log("Bed is not empty, cannot start growth");
            return;
        }

        wheat.localScale = Vector3.zero;

        wheat.DOScale(0.3f, growthDuration)
            .OnComplete(() =>
            {
                GameObject wheatPrefab = wheat.GetChild(0).gameObject;
                wheatPrefab.SetActive(true);
            });
    }

    public void CollectWheat(GameObject wheatPrefab)
    {
        collectedCount++;

        if (collectedCount == wheatCount)
        {
            Debug.Log("All wheat prefabs collected, spawning all wheat prefabs");
           
            foreach (GameObject bed in gardenBeds)
            {
                Transform wheatTransform = bed.transform.GetChild(0);
                GameObject wheat = Instantiate(wheatPrefab, wheatTransform.position, wheatTransform.rotation);
                
                wheat.SetActive(true);
                StartGrowth(wheat);
                collectedCount--;

            }
           
        }
    }
}
