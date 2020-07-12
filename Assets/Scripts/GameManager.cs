using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Guy playerGuy;
    [SerializeField] private Transform cam;
    [SerializeField] private LostUI lostUI;
    
    public Transform Player {
        get
        {
            return player;
        }
    }

    public Guy PlayerGuy
    {
        get
        {
            return playerGuy;
        }
    }

    public Transform Camera
    {
        get
        {
            return cam;
        }
    }
    
    #region singleton
    public static GameManager instance { get; private set; }

    void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        playerGuy.onKill += () =>
        {
            player.GetComponent<Player>().UI.Hide();
            lostUI.Show();
        };
    }
    
}
