using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    [SerializeField] private float initHealth = 100f;

    private GuyRagdoll ragdoll;
    
    [HideInInspector] public float Health;   
    
    public bool IsDead { get; private set; }
    
    void Awake()
    {
        ragdoll = GetComponent<GuyRagdoll>();
        Health = initHealth;
        IsDead = false;
    }

    void Update()
    {
        Debug.Log(transform.parent.name +  " - health: " + Health);
        if (Health <= 0 && !IsDead)
        {
            IsDead = true;
            Kill();
        }
    }

    private void Kill()
    {
        ragdoll.Activate();
        Debug.Log("Killed");
    }
}
