using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

public class CivilAI : MonoBehaviour
{
    [SerializeField] private Transform escapePoint;

    [SerializeField] private NavMeshAgent agent;

    CivilAnimations civilAnim;

    CivilIntoRobber player;

    private void Start()
    {
        escapePoint = GameObject.FindGameObjectWithTag("Escape").transform;

        agent = GetComponent<NavMeshAgent>();

        civilAnim = GetComponent<CivilAnimations>();

        player = FindObjectOfType<CivilIntoRobber>();

        agent.SetDestination(escapePoint.position);
        agent.isStopped = true;
    }

    private void Update()
    {
        if (player.enabled == false)
        {
            if (civilAnim.isHostage == false)
            {
                civilAnim.RunAnimation(true);
                agent.isStopped = false;
                agent.speed = 10f;
            }
            else
            {
                agent.speed = 0;
                agent.isStopped = true;
            }
        }
        else
        {
            civilAnim.RunAnimation(false);
            agent.isStopped = true;
        }
    }

    public void DestroyYourself()
    {
        Destroy(this.gameObject);
    }
}
