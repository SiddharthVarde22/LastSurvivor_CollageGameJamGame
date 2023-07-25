using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


enum ZombieStates
{
    Idle,
    Follow,
    Attack,
    Deth
}
public class Zombie : MonoBehaviour
{
    [SerializeField]
    ZombieStates state = ZombieStates.Idle;

    [SerializeField]
    int maxHelth = 100;
    [SerializeField]
    int damageToPlayer = 10;

    int currentHelth;
    GameObject player;
    Animator zombieAnimator;
    NavMeshAgent zombieAgent;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        zombieAnimator = GetComponent<Animator>();
        zombieAgent = GetComponent<NavMeshAgent>();
        zombieAgent.speed = Random.Range(5,8);
        currentHelth = maxHelth;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case ZombieStates.Idle:
                Idle();
                break;
            case ZombieStates.Follow:
                Follow();
                break;
            case ZombieStates.Attack:
                Attack();
                break;
            case ZombieStates.Deth:
                Deth();
                break;
        }
    }

    void Idle()
    {
        if(Vector3.Distance(transform.position,player.transform.position) < 60)
        {
            state = ZombieStates.Follow;
        }
    }

    void Follow()
    {
        zombieAnimator.SetTrigger("IsAleart");
        zombieAgent.SetDestination(player.transform.position);
        if(Vector3.Distance(transform.position,player.transform.position) < zombieAgent.stoppingDistance)
        {
            state = ZombieStates.Attack;
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > zombieAgent.stoppingDistance)
        {
            zombieAnimator.SetBool("Attaking", false);
            state = ZombieStates.Follow;
        }
        else
        {
            zombieAnimator.SetBool("Attaking", true);
        }
    }

    void Deth()
    {
        zombieAnimator.applyRootMotion = true;
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<NavMeshAgent>());
        zombieAnimator.SetTrigger("IsDead");
        Destroy(gameObject, 5);
    }

    public void GetDamage(int ammount)
    {
        currentHelth -= ammount;
        if (currentHelth <= 0)
        {
            state = ZombieStates.Deth;
        }
    }

    public void DamagePlayer()
    {
        player.GetComponent<PlayerMovement>().GetDamage(damageToPlayer);
    }
}
