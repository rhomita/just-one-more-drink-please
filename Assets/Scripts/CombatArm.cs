using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CombatArm : MonoBehaviour
{
    [SerializeField] private Transform hand;
    [SerializeField] private Transform target;
    [SerializeField] private Transform targetAttack;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private Transform head;
    
    private float springStrength = 1500f;
    private Rigidbody springRb;
    private SpringJoint springJoint;

    private float secondsAfterLastHit = 0;
    private static float SECONDS_TO_RESTART = 4f;
    
    public bool IsAttacking { get; private set; }
    
    void Awake()
    {
        springRb = target.GetComponent<Rigidbody>();
        IsAttacking = false;
    }

    void Start()
    {
        springJoint = hand.gameObject.AddComponent<SpringJoint>();
        //springJoint.connectedBody = springRb;
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = Vector3.zero;
        springJoint.spring = 0;

    }

    void Update()
    {
        if (secondsAfterLastHit < 0)
        {
            springJoint.connectedBody = null;
            springJoint.spring = 0;
            return;
        }

        secondsAfterLastHit -= Time.deltaTime;
    }
    
    public void Attack()
    {
        if (IsAttacking) return;
        StartCoroutine(AttackCoroutine(targetAttack.localPosition));
        
        /*
        Collider[] colliders = Physics.OverlapSphere(targetAttack.position, 1f, targetMask);
        if (colliders.Length == 0)
        {
            StartCoroutine(AttackCoroutine(targetAttack.localPosition));
            return;
        }

        Vector3 closestPosition = targetAttack.position;
        float closestDistance = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            if (collider.transform == head) continue;

            Vector3 position = target.position;
            Vector3 closestPoint = collider.ClosestPoint(position);
            float distance = (closestPoint - position).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = closestPoint;
            }
        }
        StartCoroutine(AttackCoroutine(transform.InverseTransformPoint(closestPosition)));
        */
    }
    
    private IEnumerator AttackCoroutine(Vector3 attackPosition)
    {
        IsAttacking = true;
        secondsAfterLastHit = SECONDS_TO_RESTART;
        if (springJoint.connectedBody == null)
        {
            springJoint.connectedBody = springRb;
            springJoint.spring = springStrength;
        }
        
        Vector3 initPosition = target.localPosition;
        target.localPosition = attackPosition;
        yield return new WaitForSeconds(0.3f);
        target.localPosition = initPosition;
        yield return new WaitForSeconds(0.15f);
        IsAttacking = false;
    }
    
}
