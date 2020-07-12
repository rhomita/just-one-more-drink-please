using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Text description;
    [SerializeField] private SceneLoader sceneLoader;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        description.text = "";
        StartCoroutine(ShowTutorial());
    }

    IEnumerator ShowTutorial()
    {
        description.text = "Basic controls!";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);

        description.text = "Use 'WASD' to move.";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "Use 'Left shift' to run (it uses stamina)";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "Keep 'C' pressed to crouch (it could avoid attacks).";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "Right click to punch with the right hand and left click to punch with your left hand.";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(10);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "You can grab bottles that can be used as weapons with 'Q' and 'E'";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(8);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "You can drop them with 'G'";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "Middle mouse button + move to rotate the camera.";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(5);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "Use the scrollwheel to zoom in/out.";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "Gamepad might not work. :(";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "Objective";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "Collect as many points as you can by winning battles against guys.";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(7);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "There will be 'infinite' rounds.";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(7);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        description.text = "Have fun!";
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(10);
        animator.SetTrigger("Hide");
        yield return new WaitForSeconds(.9f);
        
        sceneLoader.GoToScene("Menu");
    }
}
