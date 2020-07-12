using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Random = UnityEngine.Random;

public class CombatArm : MonoBehaviour
{
    [SerializeField] private Transform hand;
    [SerializeField] private Transform target;
    [SerializeField] private Transform targetIdle;
    [SerializeField] private Transform targetAttack;
    [SerializeField] private Transform targetWeaponIdle;
    [SerializeField] private Transform targetWeaponAttack;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private List<AudioClip> clips;

    private AudioSource audioSource;
    private float springStrength = 1700f;
    private Rigidbody springRb;
    private SpringJoint springJoint;

    private float secondsAfterLastHit = 0;
    private static float SECONDS_TO_RESTART = 4f;

    private Weapon currentWeapon = null;
    
    public bool IsAttacking { get; private set; }

    public bool HasWeapon
    {
        get
        {
            return currentWeapon != null;
        }
    }

    void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
        springRb = target.GetComponent<Rigidbody>();
        IsAttacking = false;
    }

    void Start()
    {
        springJoint = hand.gameObject.AddComponent<SpringJoint>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = Vector3.zero;
        springJoint.spring = 0;

    }

    void Update()
    {
        if (secondsAfterLastHit < 0)
        {
            ResetSpring();
            return;
        }

        secondsAfterLastHit -= Time.deltaTime;
    }

    public void ResetSpring()
    {
        springJoint.connectedBody = null;
        springJoint.spring = 0;
    }
    
    public void SetWeapon(Weapon weapon = null)
    {
        currentWeapon?.Drop();
        currentWeapon = null;
        if (weapon == null) return;
        
        currentWeapon = weapon;
        currentWeapon.Grab(weaponHolder);
    }
    
    public void Attack()
    {
        if (IsAttacking) return;

        Vector3 idlePositon = HasWeapon ? targetWeaponIdle.localPosition : targetIdle.localPosition;
        Vector3 attackPosition = HasWeapon ? targetWeaponAttack.localPosition : targetAttack.localPosition;
     
        int randomClip = Random.Range(0, clips.Count - 1);
        audioSource.PlayOneShot(clips[randomClip]);
        StartCoroutine(AttackCoroutine(idlePositon, attackPosition));
    }
    
    private IEnumerator AttackCoroutine(Vector3 idlePosition, Vector3 attackPosition)
    {
        IsAttacking = true;
        secondsAfterLastHit = SECONDS_TO_RESTART;
        if (springJoint.connectedBody == null)
        {
            springJoint.connectedBody = springRb;
            springJoint.spring = springStrength;
        }
        
        Vector3 initPosition = idlePosition;
        target.localPosition = attackPosition;
        yield return new WaitForSeconds(0.3f);
        target.localPosition = initPosition;
        yield return new WaitForSeconds(0.1f);
        IsAttacking = false;
    }
    
}
