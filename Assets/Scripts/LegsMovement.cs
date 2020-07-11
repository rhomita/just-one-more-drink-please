using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegsMovement : MonoBehaviour
{

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private List<Leg> legs;

    private Animator animator;

    private float raycastOffset;
    private float stepDistance;
    private float secondsToMove;
    private float hintOffset;
    
    public bool IsMoving { get; set; }
    public bool IsRunning { get; private set; }

    public List<Leg> Legs
    {
        get
        {
            return legs;
        }
    }

    void Awake()
    {
        animator = transform.GetComponent<Animator>();
    }
    
    void Start()
    {
        SetRunning(false);
        IsMoving = false;
    }

    public void SetRunning(bool isRunning)
    {
        IsRunning = isRunning;
        raycastOffset = isRunning ? 0.1f : 0.07f;
        stepDistance = isRunning ? 0.55f : 0.2f;
        secondsToMove = isRunning ? 0.1f : 0.1f;
        hintOffset = isRunning ? 0.4f : 0.3f;
    }

    void Update()
    {
        animator.SetBool("isRunning", IsMoving && IsRunning);
        animator.SetBool("isWalking", IsMoving && !IsRunning);
        
        float _raycastOffset = IsMoving ? raycastOffset * 2 : raycastOffset;
        
        foreach (Leg leg in legs)
        {
            RaycastHit hit;
            Vector3 raycastPosition = leg.transform.position + (transform.forward * _raycastOffset);
            if (Physics.Raycast(raycastPosition, Vector3.down, out hit, 10f, groundMask))
            {
                float distance = (leg.Target - hit.point).sqrMagnitude;
                if (distance < stepDistance * stepDistance) continue;
                if (legs.Any(_leg => _leg.IsMoving)) return;

                Vector3 _hintOffset = transform.forward * hintOffset;
                leg.ChangeTargetPosition(hit.point, _hintOffset, secondsToMove);
            }
        }
    }
    
}
