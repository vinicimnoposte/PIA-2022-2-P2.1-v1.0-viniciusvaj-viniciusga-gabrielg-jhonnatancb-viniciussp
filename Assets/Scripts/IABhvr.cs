using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABhvr : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    //public Animator anim;
    //public SkinnedMeshRenderer render;
    public Vector3 patrolposition;
    public float patrolDistance = 10;
    public float stoppedTime;
    public float distancetotrigger = 10;
    public float timetowait = 3;
    
    public enum States
    {
        pursuit,
        atacking,    
        patrol,     
    }

    public States state;

    // Start is called before the first frame update
    void Start()
    {
        patrolposition = new Vector3(transform.position.x + Random.Range(-patrolDistance, patrolDistance), transform.position.y, transform.position.z + Random.Range(-patrolDistance, patrolDistance));
    }
    private void Update()
    {
        StateMachine();
        //anim.SetFloat("Velocidade", agent.velocity.magnitude);
    }
    void StateMachine()
    {
        switch (state)
        {
            case States.pursuit:
                PursuitState();
                break;
            case States.atacking:
                AttackState();
                break;
            case States.patrol:
                PatrolState();
                break;
        }
    }
    void PursuitState()
    {
        agent.isStopped = false;
        agent.destination = target.transform.position;
        //anim.SetBool("Attack", false);
        //anim.SetBool("Damage", false);
        //if (Vector3.Distance(transform.position, target.transform.position) < 5)
        //{
        //    state = States.breath;
        //}
        if (Vector3.Distance(transform.position, target.transform.position) >= distancetotrigger)
        {
            state = States.patrol;
        }
            
    }
    void AttackState()
    {
        agent.isStopped = true;
        //anim.SetBool("Attack", true);
        //anim.SetBool("Damage", false);
        //if (Vector3.Distance(transform.position, target.transform.position) > 4)
        //{
        //    state = States.breath;
        //}
    }
    void PatrolState()
    {
        agent.isStopped = false;
        agent.SetDestination(patrolposition);
        //anim.SetBool("Attack", false);
        //tempo parado
        if (agent.velocity.magnitude < 0.1f)
        {
            stoppedTime += Time.deltaTime;

        }
        //se for mais q timetowait segundos
        if (stoppedTime > timetowait)
        {
            stoppedTime = 0;
            patrolposition = new Vector3(transform.position.x + Random.Range(-patrolDistance, patrolDistance), transform.position.y, transform.position.z + Random.Range(-patrolDistance, patrolDistance));
        }
        //ditancia do jogador for menor q distancetotrigger
        if (Vector3.Distance(transform.position, target.transform.position) < distancetotrigger)
        {
            state = States.pursuit;
        }
    }
}

    

