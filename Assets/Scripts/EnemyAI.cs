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
    [SerializeField] float SoundRadius = 20;
    [SerializeField] AudioClip ZombieScream;

    AudioSource audioSource;

    //Play the music
    bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool m_ToggleChange;


    Vector3 firstpos;
    float DistancetoTarget = Mathf.Infinity;
    bool provoked = false;
    // Start is called before the first frame update
    void Start()
    {
      myNavMeshAgent = GetComponent<NavMeshAgent>();
      firstpos = transform.position;
      audioSource = GetComponent<AudioSource>();
      m_Play = false;
      m_ToggleChange = true;
    }

    // Update is called once per frame
    void Update()
    {
        DistancetoTarget = Vector3.Distance(player.position, transform.position);
        if(DistancetoTarget <= Radius)
        {
            provoked = true;
        }

        if (DistancetoTarget <= SoundRadius)
        {
            Debug.Log("hess");
            SoundSystem();
        }
        if (provoked)
        {
            Engage();
        }
        else if(provoked == false)
        {

            Revert();
        }
    }

    public void SoundSystem()
    {
        Soundmechanism();
    }

    private void Soundmechanism()
    {
        m_Play = true;
        if (m_Play == true && m_ToggleChange == true)
        {
            m_ToggleChange = false;

            //Play the audio you attach to the AudioSource component
            audioSource.loop = true;
            audioSource.PlayOneShot(ZombieScream);
            //Ensure audio doesn’t play more than once
        }

        //Check if you just set the toggle to false
        if (m_Play == false && m_ToggleChange == true)
        {
            //Stop the audio
            audioSource.Stop();
            //Ensure audio doesn’t play more than once
            m_ToggleChange = false;
        }
    }

    void Engage()
    {
        if (DistancetoTarget >= myNavMeshAgent.stoppingDistance)
        {

            ChaseTarget();
        }
        if (DistancetoTarget <= myNavMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void ChaseTarget()
        {
            myNavMeshAgent.SetDestination(player.position);

        }
    void AttackTarget()
        {
        }

    void Revert()
        { 
            myNavMeshAgent.SetDestination(firstpos);
        }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SoundRadius);
    }
    

}
