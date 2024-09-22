using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(CapsuleCollider),typeof(Animator))]
public class NPC_Customer : NPC , IInteractable
{

    public CoinBag coinBag;

    public SO_Item wantToBuy;



    public NPC_Customer()
    {
    }

    private void Start()
    {
        StateMachine.Initialize(FreeRoamState);
        GameManager.Instance.npcCustomerList.Add(this);
    }

    public void SetCustomer(SO_Item _wantToBuy)
    {
        wantToBuy = _wantToBuy;
        targetShop = GameManager.Instance.GetShopForCustomer(this);
        //NPC_State_MoveToShopQue moveToShopQue = new NPC_State_MoveToShopQue(this, StateMachine, targetShop.ReturnQueSlot(this).transform);
        //StateMachine.ChangeState(moveToShopQue);
    }
 
    private void Update()
    {
        StateMachine.CurrentNPCState.FrameUpdate();

        if (agent.enabled && !agent.hasPath && !agent.pathPending && agent.remainingDistance == 0 && (animator.GetBool("Walking") || animator.GetBool("CarryWalk")))
        {
            if (_pickedSomething)
            {

                animator.SetBool("CarryWalk", false);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
    }

    //Press E on NPC
    public void Interact(Transform _playerTransform)
    {
        //Giving item to NPC if it waiting for it
        if(StateMachine.CurrentNPCState == WaitForWorkerState)
        {
            if(_playerTransform.GetComponent<PlayerController>()._itemInHand?._SOItem == wantToBuy)
            {

                if (targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty)
                {
                    _playerTransform.GetComponent<PlayerController>()._itemInHand.transform.parent = null;

                    //TODO: Move item to stallslot , remove from player hand, set itemInHand to null, set stallslot to not empty , set npc state to dequefromshop
                    _playerTransform.GetComponent<PlayerController>()._itemInHand.transform.DOMove(targetShop.stallSlotPos.transform.position, 0.5f)
                    .OnComplete(() =>
                        {
                            targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item = _playerTransform.GetComponent<PlayerController>()._itemInHand;
                            
                            _playerTransform.GetComponent<PlayerController>()._itemInHand = null;
                            targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty = false;   
                        });
                }


            }
            else { Debug.Log("Not Same"); }
        }
    }
}
