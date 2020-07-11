using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Rigidbody rb;
    
    void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
    
    public void Grab(Transform parent)
    {
        rb.isKinematic = true;
        transform.parent = parent;
        transform.localRotation = quaternion.identity;
        transform.localPosition = Vector3.zero;
        
    }

    public void Drop()
    {
        transform.parent = null;
        rb.isKinematic = false;
    }
}
