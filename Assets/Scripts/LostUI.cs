using UnityEngine;
using UnityEngine.UI;

public class LostUI : MonoBehaviour
{
    [SerializeField] private Text score; 
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        score.text = ScoreManager.instance.Score.ToString();
        gameObject.SetActive(true);
    }
}