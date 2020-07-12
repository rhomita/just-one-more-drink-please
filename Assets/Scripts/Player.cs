using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Guy guy;
    [SerializeField] private PlayerUI ui;
    
    private PlayerCombat combat;
    private PlayerController controller;

    public PlayerUI UI
    {
        get
        {
            return ui;
        }
    }

    public Guy Guy
    {
        get
        {
            return guy;
        }
    }
    
    void Start()
    {
        combat = transform.GetComponent<PlayerCombat>();
        controller = transform.GetComponent<PlayerController>();

        guy.onKill += OnKill;
    }

    void OnKill()
    {
        controller.enabled = false;
        combat.enabled = false;
    }
}
