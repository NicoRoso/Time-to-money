using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class CivilAI : MonoBehaviour
{
    [SerializeField] private Transform escapePoint;

    [SerializeField] private NavMeshAgent agent;

    CivilAnimations civilAnim;

    CivilIntoRobber player;

    Hp hp;

    private void Start()
    {
        escapePoint = GameObject.FindGameObjectWithTag("Escape").transform;

        agent = GetComponent<NavMeshAgent>();

        civilAnim = GetComponent<CivilAnimations>();

        player = FindObjectOfType<CivilIntoRobber>();

        hp = GetComponent<Hp>();

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

        if (hp.currentHp <=0)
        {
            agent.speed = 0;
            agent.isStopped = true;
        }
    }

    public void DestroyYourself()
    {
        Destroy(this.gameObject);
    }
}
