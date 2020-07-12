using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region singleton
    public static ScoreManager instance { get; private set; }

    void Awake()
    {
        instance = this;
    }
    #endregion
    
    private PlayerUI UI;
    
    private float multiplierSeconds = 10f;
    public int Score { get; private set; }
    private int multiplier = 1;

    private float currentMultiplierSeconds = 0f;
    
    void Start()
    {
        Score = 0;
        UI = GameManager.instance.Player.GetComponent<Player>().UI;
        UI.SetScore(Score);
    }

    public void AddMultiplier()
    {
        currentMultiplierSeconds = multiplierSeconds;
        UI.ShowMultiplier(multiplier, currentMultiplierSeconds);
        multiplier++;
    }
    
    public void AddScore(int _score, bool show = true)
    {
        int newScore = _score * multiplier;
        Score += newScore;
        if (show)
        {
            UI.ShowScore(newScore);
        }
        UI.SetScore(Score);
    }
    
    void Update()
    {
        if (currentMultiplierSeconds < 0)
        {
            multiplier = 1;
            return;
        }

        currentMultiplierSeconds -= Time.deltaTime;
    }
}
