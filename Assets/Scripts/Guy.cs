using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    [SerializeField] private float initHealth = 100f;

    private GuyRagdoll ragdoll;
    private float secondsToDestroy = 5f;
    
    public float Health { get; private set; }
    
    public delegate void OnKill();
    public OnKill onKill;
    
    public delegate void OnTakeDamage(float currentHealth);
    public OnTakeDamage onTakeDamage;
    
    public bool IsDead { get; private set; }
    
    void Awake()
    {
        ragdoll = GetComponent<GuyRagdoll>();
        Health = initHealth;
        IsDead = false;
    }

    void Update()
    {
        if (IsDead) return;
        
        if (Health <= 0 && !IsDead)
        {
            Kill();
        }
    }

    private void Kill()
    {
        if (IsDead) return;
        IsDead = true;
        ragdoll.Activate();
        StartCoroutine(DestroyAfter(secondsToDestroy));
        onKill?.Invoke();
    }

    private IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(transform.parent.gameObject);
    }

    public void TakeDamage(float _damage)
    {
        if (_damage > 5)
        {
            ScoreManager.instance.AddScore(10);
        }
        Health -= _damage;
        onTakeDamage?.Invoke(Health);
    }
}
