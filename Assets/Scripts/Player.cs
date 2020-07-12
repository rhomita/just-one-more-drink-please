using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Guy guy;
    
    private PlayerCombat combat;
    private PlayerController controller;
    
    void Start()
    {
        combat = transform.GetComponent<PlayerCombat>();
        controller = transform.GetComponent<PlayerController>();
    }

    void OnKill()
    {
        controller.enabled = false;
        combat.enabled = false;
    }
}
