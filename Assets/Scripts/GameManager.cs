using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Guy playerGuy;
    [SerializeField] private Transform cam;
    
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
    
    public static GameManager instance { get; private set; }

    #region singleton
    void Awake()
    {
        instance = this;
    }

    #endregion
    
    
    
    
}
