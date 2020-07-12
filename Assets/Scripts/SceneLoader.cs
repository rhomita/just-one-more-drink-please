using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void GoToScene(string scene)
    {
        StartCoroutine(GoScene(scene));
    }

    IEnumerator GoScene(string scene)
    {
        animator.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
