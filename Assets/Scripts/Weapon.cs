using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Rigidbody rb;
    public bool IsGrabbed = false;

    [SerializeField] private AudioClip grabClip;
    [SerializeField] private AudioClip dropClip;
    
    private AudioSource audioSource;
    
    void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
        rb = transform.GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
    
    public void Grab(Transform parent)
    {
        audioSource.PlayOneShot(grabClip);
        IsGrabbed = true;
        rb.isKinematic = true;
        transform.parent = parent;
        transform.localRotation = quaternion.identity;
        transform.localPosition = Vector3.zero;
        
    }

    public void Drop()
    {
        IsGrabbed = false;
        transform.parent = null;
        rb.isKinematic = false;
        StartCoroutine(DropSound());
    }

    IEnumerator DropSound()
    {
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(dropClip);
    }
}
