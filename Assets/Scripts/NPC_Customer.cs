using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_Customer : NPC , IInteractable
{

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

    private void NPCAI_Customer()
    {
        switch (state)
        {
            case NPCState.Idle:
                break;
            case NPCState.Walking:
                if (!isPickDropAnimPlaying)
                {
                    Destroy(this.gameObject);
                }
                break;
            case NPCState.PickedUpWalking:
                break;
            case NPCState.GoingForQue:
                if (agent.remainingDistance < 0.1f && !agent.pathPending)
                {
                    state = NPCState.Queued;
                    Debug.Log("Que again");
                }
                break;
            case NPCState.Queued:
                //if npc is on 1st place in que
                if (targetShop.WhichPlaceAtQue(this) == 0)
                {
                    transform.DOLookAt(targetShop.transform.position, 1f, AxisConstraint.Y, Vector3.up)
                    .OnComplete(() =>
                    {
                        Debug.Log("1. sýra" + this);
                        state = NPCState.WaitingForWorker;
                    });
                    state = NPCState.PlayingAnim;
                }
                //if npc is not on 1st place in que
                else
                {
                    transform.DOLookAt(targetShop.ReturnPreviousQueSlot(this).transform.position, 0.5f, AxisConstraint.Y, Vector3.up)
                    .OnComplete(() => 
                    {
                        Debug.Log("2. sýra" + this);
                        state = NPCState.WaitingForQue;
                    });
                    state = NPCState.PlayingAnim;
                }
                break;
            case NPCState.WaitingForQue:
                MoveTo(targetShop.ReturnQueSlot(this).transform);
                if (targetShop.WhichPlaceAtQue(this) == 0)
                {

                    state = NPCState.Queued;
                }

                
                break;
            case NPCState.WaitingForWorker:
                if (!targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty)
                {
                    PickItem();
                    Item _item = targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item;
                    _item.PickUp(handPos);
                    targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty = true;
                    targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item = null;
                    itemInHand = _item;
                    targetShop.GetComponentInChildren<CustomerQueue>().RemoveCustomerFromQue(this);
                    state = NPCState.Walking;
                }
                break;
            default:
                break;
        }
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            PickItem();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            DropItem();
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            ChatBubble.Create(transform,"I want to buy this one and some more word for check. Bla bla bla bla bla bla.");
        }

    }

    public void Interact(Transform _playerTransform)
    {
        Debug.Log("Selam");
    }
}
