using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class NPC : MonoBehaviour
{
    public enum NPCState
    {
        Idle,
        Walking,
        PickedUpWalking,
        GoingForQue,
        Queued,
        WaitingForWorker,
        WaitingForCustomer,
        HandlingCustomer,
        TakingItemFromRack,
        ReturningFromRackWithItem,
        GiveItemToCustomer,
    }

    public NPCState state;
    public float range = 50.0f;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Shop targetShop;
    public Transform handPos;
    public Item itemInHand;
    //Pick or drop animation boolean. Animator behaviour controlling this boolean if its playing or not.
    [HideInInspector] public bool isPickDropAnimPlaying;

    protected bool _pickedSomething;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    //Calling when pickup anim start
    protected void PickItem()
    {
        isPickDropAnimPlaying = true;
        animator.Play("PickUp");
        _pickedSomething = true;
        agent.speed = 0.9f;
    }
    //Calling when drop anim start
    protected void DropItem()
    {
        if (_pickedSomething)
        {
            isPickDropAnimPlaying = true;
            animator.Play("Drop");
            _pickedSomething = false;
            agent.speed = 1.5f;
        }
    }
    public void MoveTo(Transform _target)
    {
        agent.SetDestination(_target.position);
        if (_pickedSomething)
        {
            animator.SetBool("CarryWalk", true);

        }
        else
        {
            animator.SetBool("Walking", true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
