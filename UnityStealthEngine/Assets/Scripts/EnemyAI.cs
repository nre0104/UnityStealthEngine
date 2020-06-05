using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

// From Unity Doc https://docs.unity3d.com/Manual/nav-AgentPatrol.html
// Enemy via state machine https://www.youtube.com/watch?v=dYi-i83sq5g

// TODO: Via State machine and movement
public class EnemyAI : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Transform playerPosition;

    void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition(startingPosition);

        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    public void Chase()
    {
        playerPosition = transform.GetComponent<ViewVisualizer>().visibleTargets[0].transform;
    }

    public void Search()
    {
        float reachedRoamPositionDistance = 1f;

        if (Vector3.Distance(transform.position, roamPosition) < reachedRoamPositionDistance)
        {
            //Reached Roam Position
            roamPosition = GetRoamingPosition(transform.position);
        }
    }

    Vector3 GetRoamingPosition(Vector3 startingPosition)
    {
        return startingPosition + GetRandomDirection() * Random.Range(10f, 70f);
    }

    Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void Patrol()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
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
}
