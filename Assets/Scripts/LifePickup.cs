using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickup : MonoBehaviour
{
    private AudioSource audioSource;
    private bool taken = false;

    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (taken) return;
        if (!other.CompareTag("Player")) return;

        taken = true;
        audioSource.Play();
        GameManager.instance.PlayerGuy.ResetHealth();
        Destroy(gameObject, 0.4f);
    }
}
