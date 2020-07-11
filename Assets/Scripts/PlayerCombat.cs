using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private List<CombatArm> arms;

    [SerializeField] private LayerMask weaponMask;

    private float pickableRadius = 1f;
    private Weapon pickableWeapon = null;
    
    void Update()
    {
        CheckWeaponPickups();
        CheckInput();
    }

    private void CheckWeaponPickups()
    {
        pickableWeapon = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickableRadius, weaponMask);
        if (colliders.Length == 0) return;
        
        foreach (Collider collider in colliders)
        {
            Weapon weapon = collider.GetComponent<Weapon>();
            if (weapon == null || collider.transform.parent != null) continue;
            pickableWeapon = weapon;
            // TODO: show some UI
            break;
        }
    }
    
    private void CheckInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack(0);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Attack(1);
        }
        
        if (Input.GetKeyDown(KeyCode.E) && pickableWeapon != null)
        {
            arms[0].SetWeapon(pickableWeapon);
            pickableWeapon = null;
        }
        if (Input.GetKeyDown(KeyCode.Q) && pickableWeapon != null)
        {
            arms[1].SetWeapon(pickableWeapon);
            pickableWeapon = null;
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (CombatArm arm in arms)
            {
                if (arm.HasWeapon)
                {
                    arm.SetWeapon(null);
                    break;
                }
            }
        }
    }

    private void Attack(int armIndex)
    {
        arms[armIndex].Attack();
    }
}
