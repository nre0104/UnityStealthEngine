using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//https://docs.unity3d.com/Manual/nav-AgentPatrol.html

public class EnemyController : MonoBehaviour
{
    public float LookRadius = 6f;
    NavMeshAgent agent;
    Transform target;
    private Vector3 startingPosition;
    public Transform[] points;
    private int destPoint = 0;
    private State state;

    private enum State
    {
        Patrole,
        Chase,
    }

    void Start()
    {
        startingPosition = transform.position;;
        target = GameManager.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
    }

    void Update()
    {

        switch (state)
        {
            default:
            case State.Patrole:
                PatroleMap();
                break;
            case State.Chase:
                ChasePlayer();
                break;
        }
    }

    void PatroleMap()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (!agent.pathPending && agent.remainingDistance < 2f)
        {
            GotoNextPoint();
        }
        if(distance <= LookRadius)
        {
            state = State.Chase;
        }
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= LookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
        if(distance >= LookRadius)
        {
            state = State.Patrole;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
}
