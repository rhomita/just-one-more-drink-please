using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Leg : MonoBehaviour
{
    private TwoBoneIKConstraint ikConstraint;
    private Transform target;
    private Transform hint;
    
    public bool IsMoving { get; private set; }

    public Vector3 Target
    {
        get
        {
            return target.position;
        }
    }

    void Awake()
    {   
        ikConstraint = transform.GetComponent<TwoBoneIKConstraint>();
        
        target = new GameObject(transform.name +  "_Target").transform;
        target.position = transform.position;
        
        hint = new GameObject(transform.name +  "_Pole").transform;
        hint.position = transform.position;
        
        ikConstraint.data.target = target;
        ikConstraint.data.hint = hint;
    }

    public void ChangeTargetPosition(Vector3 newPosition, Vector3 hintOffset, float seconds)
    {
        StartCoroutine(ChangeTarget(newPosition, hintOffset, seconds));
    }

    private IEnumerator ChangeTarget(Vector3 newPosition, Vector3 hintOffset, float seconds)
    {
        IsMoving = true;
        Vector3 currentTargetPosition = target.position;
        Vector3 currentHintPosition = hint.position;
        Vector3 newHintPosition = newPosition + hintOffset;
        float secondsElapsed = 0f;
        while (secondsElapsed < seconds)
        {
            secondsElapsed += Time.deltaTime;
            target.position = Vector3.Lerp(currentTargetPosition, newPosition, secondsElapsed / seconds);
            hint.position = Vector3.Lerp(currentHintPosition, newHintPosition, secondsElapsed / seconds);
            yield return null;
        }

        IsMoving = false;
        yield return null;
    }
}
