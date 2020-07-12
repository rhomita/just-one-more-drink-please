using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Guy guy;
    private Rigidbody rb;
    [SerializeField]
    [Range(0.0f, 100.0f)]
    public float defenceRate;
    
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
        rb = transform.GetComponent<Rigidbody>();
    }
    

    private void OnCollisionEnter(Collision other)
    {
        Guy[] guys = other.transform.GetComponentsInParent<Guy>();
        if (guys.Length == 0 || guys[0] == Guy) return;
        
        float damage = other.impulse.magnitude / hitMagnitude;
        damage = damage - (defenceRate * damage) / 100;
        Guy.TakeDamage(damage);
    }
}
