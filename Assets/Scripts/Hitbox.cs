using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Guy guy;
    private Rigidbody rb;

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
    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Guy[] guys = other.transform.GetComponentsInParent<Guy>();
        if (guys.Length == 0 || guys[0] == Guy) return;
        
        // TODO maybe knock out?
        Guy.Health -= other.impulse.magnitude / hitMagnitude;
    }
}
