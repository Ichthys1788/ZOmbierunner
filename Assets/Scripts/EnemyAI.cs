using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent myNavMeshAgent;
    [SerializeField] Transform player;
    [SerializeField] float Radius = 5;
    Vector3 firstpos;
    float DistancetoTarget = Mathf.Infinity;
    bool provoked = false;
    
    // Start is called before the first frame update
    void Start()
    {
      myNavMeshAgent = GetComponent<NavMeshAgent>();
      firstpos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        DistancetoTarget = Vector3.Distance(player.position, transform.position);
        Debug.Log(DistancetoTarget);
        if(DistancetoTarget <= Radius)
        {
            provoked = true;
        }
        else
        {
            provoked= false;
        }

        if(provoked)
        {
            Engage();
        }
        else if(provoked == false)
        {

            Revert();
        }
    }

    void Engage()
    {
        if (DistancetoTarget >= myNavMeshAgent.stoppingDistance)
        {
            Debug.Log("SDFSFDSF1");

            ChaseTarget();
        }
        if (DistancetoTarget <= myNavMeshAgent.stoppingDistance)
        {
            Debug.Log("SDFSFDSF2");
            AttackTarget();
        }
    }

    void ChaseTarget()
        {
            myNavMeshAgent.SetDestination(player.position);

        }
    void AttackTarget()
        {
            Debug.Log("sdfssdf");
        }

    void Revert()
        { 
            myNavMeshAgent.SetDestination(firstpos);
            Debug.Log("SDFSFDSF");
        }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
