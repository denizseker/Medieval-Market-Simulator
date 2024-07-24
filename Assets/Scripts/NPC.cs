using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;

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
        WaitingForQue,
        PlayingAnim,
    }

    public NPCStateMachine StateMachine;
    public NPC_State_Idle IdleState;
    public NPC_State_InQue InQueState;
    public NPC_State_PickUp PickUpState;
    public NPC_State_WaitForCustomer WaitForCustomerState;
    public NPC_State_WaitForWorker WaitForWorkerState;
    public NPC_State_HandleCustomer HandleCustomerState;
    public NPC_State_ReturnFromRackWithItem ReturnFromRackWithItemState;
    public NPC_State_GiveItemToCustomer GiveItemToCustomerState;
    public NPC_State_GoToDespawn GoToDespawnState;
    private void AnimationTriggerEvent(AnimationTriggerType _triggerType) 
    {
        //fill
    }
    public enum AnimationTriggerType
    {
        PickUp,
        Drop,
    }
    public TMP_Text stateText;
    [HideInInspector] public NPCState state;
    [HideInInspector] public float range = 50.0f;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;
    public Shop targetShop;
    public Transform handPos;
    [HideInInspector] public Item itemInHand;
    //Pick or drop animation boolean. Animator behaviour controlling this boolean if its playing or not.
    [HideInInspector] public bool isPickDropAnimPlaying;

    [HideInInspector] public bool _pickedSomething;
    private void Awake()
    {
        StateMachine = new NPCStateMachine();
        IdleState = new NPC_State_Idle(this, StateMachine);
        InQueState = new NPC_State_InQue(this, StateMachine);
        PickUpState = new NPC_State_PickUp(this, StateMachine);
        WaitForCustomerState = new NPC_State_WaitForCustomer(this, StateMachine);
        WaitForWorkerState = new NPC_State_WaitForWorker(this, StateMachine);
        HandleCustomerState = new NPC_State_HandleCustomer(this, StateMachine);
        ReturnFromRackWithItemState = new NPC_State_ReturnFromRackWithItem(this, StateMachine);
        GiveItemToCustomerState = new NPC_State_GiveItemToCustomer(this, StateMachine);
        GoToDespawnState = new NPC_State_GoToDespawn(this, StateMachine);

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
    public void PickItem()
    {
        isPickDropAnimPlaying = true;
        animator.Play("PickUp");
        _pickedSomething = true;
        agent.speed = 0.9f;
    }
    //Calling when drop anim start
    public void DropItem()
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

}
