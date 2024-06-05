using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceAI : MonoBehaviour
{
    public enum State
    {
        PreparationPhase,
        AssaultPhase
    }

    public State currentState = State.PreparationPhase;
    public Transform target;
    public float attackRadius;

    private List<Transform> waypoints = new List<Transform>();
    private NavMeshAgent agent;

    [SerializeField] private Hp hp;
    [SerializeField] private AnimationEnemy enemyAnim;

    [SerializeField] private AudioClip[] _moveLines;
    [SerializeField] private AudioClip[] _backLines;

    public Action<AudioClip[]> isOrdered;

    private bool isOrdering = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        hp = agent.GetComponent<Hp>();
        enemyAnim = GetComponent<AnimationEnemy>();

        agent.speed = 3f;

        attackRadius = 5f;

        GameObject[] waypointParents = GameObject.FindGameObjectsWithTag("WaypointParent");
        foreach (GameObject parent in waypointParents)
        {
            foreach (Transform child in parent.transform)
            {
                waypoints.Add(child);
            }
        }

        if (waypoints.Count > 0)
        {
            MoveToNextWaypoint();
        }

        FindAndSetRandomGangTarget();
    }

    private void Update()
    {
        if (!hp.isDead)
        {
            switch (currentState)
            {
                case State.PreparationPhase:
                    agent.stoppingDistance = 1f;
                    if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
                    {
                        currentState = State.PreparationPhase;
                        agent.isStopped = true;
                    }
                    else
                    {
                        enemyAnim.Walking(true);
                        agent.isStopped = false;
                    }
                    if (isOrdering)
                    {
                        isOrdered?.Invoke(_backLines);
                        isOrdering = true;
                    }
                    break;
                case State.AssaultPhase:
                    if (target != null)
                    {
                        agent.SetDestination(target.position);
                        if (Vector3.Distance(transform.position, target.position) < attackRadius)
                        {
                            agent.isStopped = true;
                            enemyAnim.Walking(false);
                        }
                        else
                        {
                            enemyAnim.Walking(true);
                            agent.isStopped = false;
                            agent.SetDestination(target.position);
                        }
                        if (isOrdering)
                        {
                            isOrdered?.Invoke(_moveLines);
                            isOrdering = true;
                        }
                    }
                    break;
            }

            if (agent.isStopped)
            {
                enemyAnim.Walking(false);
            }
        }
        else
        {
            enemyAnim.Walking(false);
            agent.enabled = false;
            this.enabled = false;
        }
    }

        private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Gang"))
            {
                target = collider.transform;
                break;
            }
        }
    }

    public void MoveToNextWaypoint()
    {
        if (waypoints.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, waypoints.Count);
            Transform randomWaypoint = waypoints[randomIndex];
            agent.SetDestination(randomWaypoint.position);
            currentState = State.PreparationPhase;
        }
    }

    private void FindAndSetRandomGangTarget()
    {
        GameObject[] gangObjects = GameObject.FindGameObjectsWithTag("Gang");
        if (gangObjects.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, gangObjects.Length);
            target = gangObjects[randomIndex].transform;
        }
    }

    public void SetAttackTarget(Transform newTarget)
    {
        target = newTarget;
        currentState = State.AssaultPhase;
    }

    public void SetPreparationPhase()
    {
        currentState = State.PreparationPhase;
        MoveToNextWaypoint();
    }
    public void SetAssaultPhase()
    {
        currentState = State.AssaultPhase;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
