using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private List<CombatArm> arms;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack(0);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Attack(1);
        }
    }

    private void Attack(int armIndex)
    {
        arms[armIndex].Attack();
    }
}
