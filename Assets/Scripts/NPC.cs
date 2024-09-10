using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;

public class NPC : MonoBehaviour
{
    public NPCStateMachine StateMachine;
    public NPC_State_Idle IdleState;
    public NPC_State_InQue InQueState;
    public NPC_State_TakeItemFromRack TakeItemFromRack;
    public NPC_State_WaitForCustomer WaitForCustomerState;
    public NPC_State_WaitForWorker WaitForWorkerState;
    public NPC_State_HandleCustomer HandleCustomerState;
    public NPC_State_ReturnFromRackWithItem ReturnFromRackWithItemState;
    public NPC_State_GiveItemToCustomer GiveItemToCustomerState;
    public NPC_State_GoToDespawn GoToDespawnState;
    public NPC_State_DeQueFromShop DeQueFromShopState;
    public NPC_State_PayForItem PayForItemState;
    public NPC_State_TakeItemFromStall TakeItemFromStallState;
    public NPC_State_TakeMoneyFromCustomer TakeMoneyFromCustomerState;

    //Animation behaviour scripts calling this function via B_XXXX script. Every character handling its own situation.
    public void AnimationTriggerEvent(AnimationTriggerType _triggerType) 
    {
        StateMachine.CurrentNPCState.AnimationTriggerEvent(_triggerType);
    }

    //Animation behaviour scripts using it.
    public enum AnimationTriggerType
    {
        AnimationStarted,
        AnimationHalfwayDone,
        AnimationEnded,
    }
    public TMP_Text stateText;
    [HideInInspector] public float range = 50.0f;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;
    public Shop targetShop;
    public Transform handPos;
    [HideInInspector] public Item itemInHand;

    [HideInInspector] public bool _pickedSomething;
    private void Awake()
    {
        StateMachine = new NPCStateMachine();
        IdleState = new NPC_State_Idle(this, StateMachine);
        InQueState = new NPC_State_InQue(this, StateMachine);
        TakeItemFromRack = new NPC_State_TakeItemFromRack(this, StateMachine);
        WaitForCustomerState = new NPC_State_WaitForCustomer(this, StateMachine);
        WaitForWorkerState = new NPC_State_WaitForWorker(this, StateMachine);
        HandleCustomerState = new NPC_State_HandleCustomer(this, StateMachine);
        ReturnFromRackWithItemState = new NPC_State_ReturnFromRackWithItem(this, StateMachine);
        GiveItemToCustomerState = new NPC_State_GiveItemToCustomer(this, StateMachine);
        GoToDespawnState = new NPC_State_GoToDespawn(this, StateMachine);
        DeQueFromShopState = new NPC_State_DeQueFromShop(this, StateMachine);
        PayForItemState = new NPC_State_PayForItem(this, StateMachine);
        TakeItemFromStallState = new NPC_State_TakeItemFromStall(this, StateMachine);
        TakeMoneyFromCustomerState = new NPC_State_TakeMoneyFromCustomer(this, StateMachine);


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

    public void PlayHandTheMoneyAnim()
    {
        animator.SetBool("HandTheMoney", true);
        animator.Play("HandTheMoney");
    }
    //Calling when pickup anim start
    public void PlayPickAnim()
    {
        animator.Play("PickUp");
        _pickedSomething = true;
        agent.speed = 0.9f;
    }
    //Calling when drop anim start
    public void PlayDropAnim()
    {
        if (_pickedSomething)
        {
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
