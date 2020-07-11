using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private List<CombatArm> arms;
    [SerializeField] private List<Weapon> initWeapons;

    private Transform player;

    private float attackCooldown = 0;
    private static float ATTACK_COOLDOWN = 0.3f;
    
    void Start()
    {
        player = GameManager.instance.Player;

        for (int i = 0; i < initWeapons.Count; i++)
        {
            arms[i].SetWeapon(initWeapons[i]);
        }
    }
    
    void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;  
        }
    }

    public void Attack()
    {
        if (attackCooldown > 0) return;

        foreach (CombatArm arm in arms)
        {
            if (arm.IsAttacking) continue;
            arm.Attack();
            attackCooldown = ATTACK_COOLDOWN;
            break;
        }
    }
    
}
