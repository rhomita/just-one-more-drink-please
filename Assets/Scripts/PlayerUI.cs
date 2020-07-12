using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;

    [SerializeField] private Text score;
    [SerializeField] private Text newScore;
    [SerializeField] private Animator scoreAnimator;
    
    [SerializeField] private Text multiplierText;
    [SerializeField] private Animator multiplierAnimator;

    private Coroutine multiplierCoroutine;
    
    public void InitHealth(float health)
    {
        healthSlider.maxValue = (int) health;
    }
    
    public void InitStamina(float stamina)
    {
        staminaSlider.maxValue = (int) stamina;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = (int) health;
    }
    
    public void SetStamina(float stamina)
    {
        staminaSlider.value = (int) stamina;
    }

    public void SetScore(int _score)
    {
        score.text = _score.ToString();
    }

    public void ShowScore(int _score)
    {
        newScore.text = _score.ToString();
        scoreAnimator.SetTrigger("Show");
    }

    public void ShowMultiplier(int multiplier, float duration)
    {
        multiplierText.text = "x " + multiplier.ToString();
        if (multiplierCoroutine != null)
        {
            StopCoroutine(multiplierCoroutine);
        }
        multiplierCoroutine = StartCoroutine(ShowMultiplier(duration));
    }

    private IEnumerator ShowMultiplier(float duration)
    {
        multiplierAnimator.SetTrigger("Show");
        yield return new WaitForSeconds(duration - 1);
        multiplierAnimator.SetTrigger("Hide");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
