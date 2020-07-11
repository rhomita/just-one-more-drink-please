using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GuyRagdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody bodyRigidBody;
    [SerializeField] private List<CombatArm> arms;
    private Animator animator;
    private RigBuilder rigBuilder;
    private LegsMovement legsMovement;
    
    void Awake()
    {
        legsMovement = transform.GetComponent<LegsMovement>();
        animator = transform.GetComponent<Animator>();
        rigBuilder = transform.GetComponent<RigBuilder>();
    }
    
    public void Activate()
    {
        foreach(Leg leg in legsMovement.Legs)
        {
            Transform upperLeg = leg.transform;
            Rigidbody upperRb = AddJoint(upperLeg, bodyRigidBody);
            
            Transform lowerLeg = upperLeg.GetChild(0);
            AddJoint(lowerLeg, upperRb);
        }

        foreach (CombatArm arm in arms)
        {
            arm.SetWeapon();
            arm.ResetSpring();
            arm.enabled = false;
        }
        
        legsMovement.enabled = false;
        rigBuilder.enabled = false;
        animator.enabled = false;
        bodyRigidBody.isKinematic = false;
    }

    private Rigidbody AddJoint(Transform transform, Rigidbody connectedBody)
    {
        GameObject _gameObject = transform.gameObject;
        Rigidbody rb = _gameObject.AddComponent<Rigidbody>();
        ConfigurableJoint joint = _gameObject.AddComponent<ConfigurableJoint>();
        joint.connectedBody = connectedBody;
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.angularXMotion = ConfigurableJointMotion.Limited;
        joint.angularYMotion = ConfigurableJointMotion.Limited;
        joint.angularZMotion = ConfigurableJointMotion.Limited;
        return rb;
    }
}
