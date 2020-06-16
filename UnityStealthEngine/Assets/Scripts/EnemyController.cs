﻿using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Vision;

// https://docs.unity3d.com/Manual/nav-AgentPatrol.html
public class EnemyController : MonoBehaviour
{
    public float LookRadius = 6f;
    [Range(0, 360)]
    public float fieldOfViewAngle = 110f;
    private float distractionTime = 10f;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    public LayerMask targetLayer;
    public LayerMask obstacleLayer;

    NavMeshAgent agent;
    Transform target;
    public Transform[] points;

    private int destination = 0;
    private State state;
    private Vector3 roamPosition;
    private Vector3 stonePosition;

    public UnityEvent OnHearedEvent;
    public UnityEvent OnHearedLostEvent;
    public UnityEvent OnViewEvent;
    public UnityEvent OnViewLostEvent;

    private enum State
    {
        Patrol,
        Chase,
        Search,
        Distracted
    }

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

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
            case State.Patrol:
                PatrolMap();
                break;
            case State.Chase:
                ChasePlayer();
                break;
            case State.Search:
                roamPosition = GetRoamingPosition();
                SearchForPlayer();
                break;
            case State.Distracted:
                GetDistracted();
                break;
        }
    }

    void LateUpdate()
    {
        ViewVisualizer viewVisualizer = new ViewVisualizer(transform, LookRadius, fieldOfViewAngle, targetLayer, obstacleLayer, 6f, 6, 0.5f, viewMeshFilter);
        viewVisualizer.DrawFieldOfView();
    }

    void PatrolMap()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (!agent.pathPending && agent.remainingDistance < 2f)
        {
            GotoNextPoint();
        }
        if (distance <= LookRadius && target.GetComponent<PlayerController>().isHidden == false)
        {
            state = State.Chase;
        }
    }

    void SearchForPlayer()
    {
        float reachedPositionDistance = 2f;
        agent.SetDestination(roamPosition);

        if (Vector3.Distance(transform.position, roamPosition) > reachedPositionDistance)
        {
            state = State.Patrol;
        }

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= LookRadius && target.GetComponent<PlayerController>().isHidden == false)
        {
            state = State.Chase;
        }
    }

    Vector3 GetRoamingPosition()
    {
        return transform.position + GetRandomDir() * Random.Range(10f, 15f);
    }

    private Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Vector3 direction = target.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle <= fieldOfViewAngle * 0.5f && target.GetComponent<PlayerController>().isHidden == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, LookRadius))
            {
                agent.SetDestination(target.position);
                OnViewEvent.Invoke();
                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }
        }

        if (CalculatePathLength(target.position) <= LookRadius && target.GetComponent<PlayerController>().isSprinting == true)
        {
            OnHearedEvent.Invoke();
            agent.SetDestination(target.position);
        }

        if (distance >= LookRadius || CalculatePathLength(target.position) >= LookRadius || target.GetComponent<PlayerController>().isHidden == true)
        {
            OnHearedLostEvent.Invoke();
            OnViewLostEvent.Invoke();
            state = State.Search;
        }
    }

    float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled)
        {
            agent.CalculatePath(targetPosition, path);
        }
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }
        return pathLength;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Stone" && state != State.Chase)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.isStopped = false;
            state = State.Distracted;
            Debug.Log("Stone Collided");
            stonePosition = collider.gameObject.transform.position;
            Invoke("ReturnToPatrolling", distractionTime);
        }
    }

    void GetDistracted()
    {
        agent.SetDestination(stonePosition);
    }

    void ReturnToPatrolling()
    {
        state = State.Patrol;
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
        agent.destination = points[destination].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destination = (destination + 1) % points.Length;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
}