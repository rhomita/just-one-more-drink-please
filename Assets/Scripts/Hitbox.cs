using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Random = UnityEngine.Random;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Guy guy;
    private Rigidbody rb;
    [SerializeField]
    [Range(0.0f, 100.0f)]
    public float defenceRate;

    [SerializeField] private List<AudioClip> hitClips;

    [SerializeField] private ParticleSystem bloodParticles;

    private AudioSource audioSource;
    private float hitMagnitude = 4f;

    public Guy Guy
    {
        get
        {
            return guy;
        }
    }

    void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
        rb = transform.GetComponent<Rigidbody>();
    }
    

    private void OnCollisionEnter(Collision other)
    {
        Guy[] guys = other.transform.GetComponentsInParent<Guy>();
        if (guys.Length == 0 || guys[0] == Guy) return;
        
        float damage = other.impulse.magnitude / hitMagnitude;
        damage = damage - (defenceRate * damage) / 100;
        Guy.TakeDamage(damage);
        int random = Random.Range(0, hitClips.Count);

        if (other.contacts.Length > 0)
        {
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, other.contacts[0].normal);
            Instantiate(bloodParticles, other.contacts[0].point, rotation);
        }
        audioSource.PlayOneShot(hitClips[random]);
    }
}
