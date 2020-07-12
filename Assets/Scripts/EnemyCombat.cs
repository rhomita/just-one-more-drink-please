using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private List<CombatArm> arms;
    [SerializeField] private List<Weapon> initWeapons;
    
    private float attackCooldown = 0;
    private static float ATTACK_COOLDOWN = 2f;
    private bool isAttackCooldown = true;
    
    private float armCooldown = 0;
    private static float ARM_COOLDOWN = 0.3f;
    
    void Start()
    {
        for (int i = 0; i < initWeapons.Count; i++)
        {
            arms[i].SetWeapon(initWeapons[i]);
        }
    }
    
    void Update()
    {
        if (armCooldown > 0)
        {
            armCooldown -= Time.deltaTime;  
        }

        if (attackCooldown <= 0)
        {
            isAttackCooldown = !isAttackCooldown;
            attackCooldown = ATTACK_COOLDOWN;
        }
        
        if (attackCooldown > 0 && isAttackCooldown)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (isAttackCooldown) return;
        if (armCooldown > 0) return;

        attackCooldown -= ARM_COOLDOWN;
        
        foreach (CombatArm arm in arms)
        {
            if (arm.IsAttacking) continue;
            arm.Attack();
            armCooldown = ARM_COOLDOWN;
            break;
        }
    }
    
}
