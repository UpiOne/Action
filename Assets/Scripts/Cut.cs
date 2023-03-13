using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Cut : MonoBehaviour
{
    [Space(10)]
    [Header("Elements")]
    [Space(10)]
    [SerializeField] private Material mat;
    [SerializeField] private GameObject kesobj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Slice"))
        {
            mat = other.GetComponent<MeshRenderer>().material;
            kesobj = other.gameObject;
        }
    }

    void Update()
    {
       
            SlicedHull Kesilen = Kes(kesobj, mat);
            GameObject kesilenust = Kesilen.CreateUpperHull(kesobj, mat);
            kesilenust.AddComponent<MeshCollider>().convex = true;
            //kesilenust.AddComponent<Rigidbody>();
            kesilenust.layer = LayerMask.NameToLayer("Slice");
            GameObject kesilenalt = Kesilen.CreateLowerHull(kesobj, mat);
            kesilenalt.AddComponent<MeshCollider>().convex = true;
            kesilenalt.AddComponent<Rigidbody>();
            kesilenalt.layer = LayerMask.NameToLayer("Slice");
            Destroy(kesobj);
        
    }
	public SlicedHull Kes(GameObject obj, Material crossSectionMaterial = null)
	{
		return obj.Slice(transform.position, transform.up, crossSectionMaterial);
	}

}
