using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Guy playerGuy;
    [SerializeField] private Transform cam;
    [SerializeField] private LostUI lostUI;
    [SerializeField] private GameObject pauseUI;

    private PlayerUI playerUI;
    private Animator playerAnimator;
    private bool hasFinished = false;

    public Transform Player
    {
        get { return player; }
    }

    public Guy PlayerGuy
    {
        get { return playerGuy; }
    }

    public Transform Camera
    {
        get { return cam; }
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
        playerUI = player.GetComponent<Player>().UI;
        pauseUI.SetActive(false);

        playerGuy.onKill += () =>
        {
            hasFinished = true;
            playerUI.Toggle(false);
            lostUI.Show();
        };
    }

    public void ShowRound(int number)
    {
        playerUI.ShowRound(number);
    }
    
    void Update()
    {
        if (hasFinished) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(!pauseUI.activeSelf);
        }
    }

    public void TogglePause(bool enable)
    {
        playerUI.Toggle(!enable);
        pauseUI.SetActive(enable);
        Time.timeScale = !enable ? 1 : 0;
        ToggleCursor(enable);
    }
    
    private void ToggleCursor(bool enable)
    {
        Cursor.visible = enable;
        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
    }
}