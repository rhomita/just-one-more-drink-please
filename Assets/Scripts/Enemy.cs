using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Guy guy;
    [SerializeField] private FloatingHealthBarUI healthBarUi;

    private EnemyCombat combat;
    private EnemyMovement movement;

    private bool idle = true;

    private static int SCORE = 1000;

    public Guy Guy
    {
        get
        {
            return guy;
        }
    }
    
    void Start()
    {
        combat = transform.GetComponent<EnemyCombat>();
        movement = transform.GetComponent<EnemyMovement>();
        guy.onKill += OnKill;
        guy.onTakeDamage += OnTakeDamage;
        
        healthBarUi.SetMaxHealth((int) guy.Health);
        healthBarUi.SetHealth((int) guy.Health);
        
        healthBarUi.gameObject.SetActive(false);
        combat.enabled = false;
        movement.enabled = false;
    }

    void OnTakeDamage(float currentHealth)
    {
        if (Guy.IsDead) return;
        if (idle)
        {
            idle = false;
            combat.enabled = true;
            movement.enabled = true;
            healthBarUi.gameObject.SetActive(true);
        }
        healthBarUi.SetHealth((int) currentHealth);
    }
    
    void OnKill()
    {
        ScoreManager.instance.AddScore(SCORE);
        ScoreManager.instance.AddMultiplier();
        healthBarUi.gameObject.SetActive(false);
        movement.enabled = false;
        combat.enabled = false;
    }
}
