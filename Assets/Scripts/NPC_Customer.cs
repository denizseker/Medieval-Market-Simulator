using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_Customer : NPC , IInteractable
{

    public CoinBag coinBag;

    [HideInInspector] public SO_Item wantToBuy;

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        GameManager.Instance.npcCustomerList.Add(this);
    }

    public void SetCustomer(SO_Item _wantToBuy)
    {
        wantToBuy = _wantToBuy;
        targetShop = GameManager.Instance.GetShopForCustomer(this);
        NPC_State_MoveToShopQue moveToShopQue = new NPC_State_MoveToShopQue(this, StateMachine, targetShop.ReturnQueSlot(this).transform);
        StateMachine.ChangeState(moveToShopQue);
        //MoveTo(targetShop.ReturnQueSlot(this).transform);
        //state = NPCState.GoingForQue;
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

    public void Interact(Transform _playerTransform)
    {
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
