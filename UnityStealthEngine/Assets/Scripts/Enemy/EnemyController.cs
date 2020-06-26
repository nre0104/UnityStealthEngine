using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Assets.Scripts.Enemy
{
    /**
     * Controls the different states the walking enemy can use.
     * Also provides the implementations for the states
     */
    public class EnemyController : MonoBehaviour
    {
        public float LookRadius = 6f;
        [Range(0, 360)]
        public float fieldOfViewAngle = 110f;
        private float distractionTime = 10f;
        public float enemyStunningTime;

        NavMeshAgent agent;
        Transform target;
        public Transform[] patrolingPoints;

        private int destination = 0;
        public State state;
        private Vector3 roamPosition;
        private GameObject Stone;
        public bool isStuned;
        private Material oldMaterial;

        public UnityEvent OnHearedEvent;
        public UnityEvent OnHearedLostEvent;

        /**
         *   Patrol: Moves between transforms
         *   Chase:  Chases target
         *   Search: Searches - Go to a random position
         *   Distracted: Focus an Distraction-Object which hits the collider
         *   Stunned: Is deactivated for X seconds
        */
        public enum State
        {
            Patrol,
            Chase,
            Search,
            Distracted,
            Stunned
        }

        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Body")
                {
                    oldMaterial = transform.GetChild(1).GetComponent<Renderer>().material;
                }
            }
            target = GameManager.player.transform;
            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;
            GotoNextPoint();
        }

        // State Machine in switch case
        void LateUpdate()
        {
            switch (state)
            {
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
                case State.Stunned:
                    Stunned();
                    break;
                default:
                    break;
            }
        }

        /**
         * Patrolling the Map between Transforms and switches into Chasing State in the event of seeing or hearing the Enemy
         */
        void PatrolMap()
        {
            float distance = Vector3.Distance(target.position, transform.position);
            Vector3 direction = target.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (!agent.pathPending && agent.remainingDistance < 2f)
            {
                GotoNextPoint();
            }

            // Enemy sees the Target in the patrolling State and changes into the chasing state
            if ((distance <= LookRadius && angle <= fieldOfViewAngle * 0.5f) && target.GetComponent<PlayerController>().isHidden == false)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, LookRadius))
                {
                    if (hit.transform.gameObject == target.transform.gameObject)
                    {
                        state = State.Chase;
                    }
                }
            }

            // Enemy hears Target end switches in the Chasing State 
            if ((CalculatePathLength(target.transform.position) <= LookRadius && target.GetComponent<PlayerController>().isSprinting) && target.GetComponent<PlayerController>().isHidden == false)
            {
                OnHearedEvent.Invoke();
                state = State.Chase;
            }
        }

        /**
         * Moves to a Random Position inside the Map if losing the Target 
         */
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

        /**
         * Moves to the Targets position and Faces the target in case of being to close to him to actually move
         */
        void ChasePlayer()
        {
            float distance = Vector3.Distance(target.position, transform.position);
            Vector3 direction = target.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            agent.SetDestination(target.position);
            if (angle <= fieldOfViewAngle * 0.5f && target.GetComponent<PlayerController>().isHidden == false)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, LookRadius))
                {
                    if (hit.transform.gameObject == target.transform.gameObject)
                    {
                        if (distance <= agent.stoppingDistance)
                        {
                            FaceTarget(target.position);
                        }
                    }
                }
            }

            if (distance >= LookRadius || CalculatePathLength(target.transform.position) >= LookRadius || target.GetComponent<PlayerController>().isHidden == true)
            {
                OnHearedLostEvent.Invoke();
                state = State.Search;
            }
        }

        /**
         * Calculates the Path to the Target by creating a chain of linked Vectors
         * Is used to hear the Enemy Moving in a certain distance behind Walls
         * <para>Vector3 targetPosition</para>
         */
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

        /**
         * Uses The OnTriggerEnter Method to get the the distraction Object with Tag Stone
         */
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Stone" && state != State.Chase)
            {
                Stone = other.transform.gameObject;
                agent.ResetPath();
                state = State.Distracted;
                Invoke("ReturnToPatrolling", distractionTime);
            }
        }

        void ReturnToPatrolling()
        {
            state = State.Patrol;
        }

        /**
         * Binds the target to the Stone. If the Enemy sees the Target it starts chasing him
         */
        void GetDistracted()
        {
            if (Stone != null && !isStuned)
            {
                agent.SetDestination(Stone.transform.position);
                FaceTarget(Stone.transform.position);
                Vector3 direction = target.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle <= fieldOfViewAngle * 0.5f && target.GetComponent<PlayerController>().isHidden == false)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, LookRadius))
                    {
                        if (hit.transform.gameObject == target.transform.gameObject)
                        {
                            state = State.Chase;
                        }
                    }
                }
            }
        }

        void FaceTarget(Vector3 position)
        {
            Vector3 direction = (position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }

        void GotoNextPoint()
        {
            // Returns if no patrolingPoints have been set up
            if (patrolingPoints.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = patrolingPoints[destination].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destination = (destination + 1) % patrolingPoints.Length;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, LookRadius);
        }

        /**
         * Makes the Enemy inoparable for 10 seconds, disables the viewVisualizer and stops all Coroutines
         */
        public void Stunned()
        {
            if (isStuned == false)
            {
                agent.isStopped = true;
                agent.ResetPath();
                Invoke("Destunned", enemyStunningTime);
                isStuned = true;

                gameObject.GetComponent<ViewVisualizer>().StopAllCoroutines();
                gameObject.GetComponent<ViewVisualizer>().enabled = false;
            }
        }

        /**
         * Sets the Enemy into Patrol state and actives the ViewVisualizer and its Coroutine.
         * Also sets the Material back to Standard
         */
        void Destunned()
        {
            isStuned = false;
            agent.isStopped = false;
            state = State.Patrol;
            gameObject.GetComponent<ViewVisualizer>().enabled = true;
            gameObject.GetComponent<ViewVisualizer>().StartCoroutine("FindTargetsWithDelay", .2f);

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).tag == "Body")
                {
                    transform.GetChild(i).GetComponent<Renderer>().material = oldMaterial;
                }
            }
        }
    }
}
