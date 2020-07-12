using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private EnemyCombat combat;
    private NavMeshAgent agent;
    private Transform player;
    private Guy playerGuy;

    void Awake()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        combat = transform.GetComponent<EnemyCombat>();
        enemy = transform.GetComponent<Enemy>();
    }
    
    void Start()
    {
        player = GameManager.instance.Player;
        playerGuy = GameManager.instance.PlayerGuy;

        enemy.Guy.onKill += () =>
        {
            agent.isStopped = true;
        };
    }

    void Update()
    {
        if (playerGuy.IsDead) return;

        agent.SetDestination(player.position);

        if ((player.position - transform.position).magnitude < agent.stoppingDistance)
        {
            FaceTarget();
            combat.Attack();
        }
    }

    
    
    private void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }
}
