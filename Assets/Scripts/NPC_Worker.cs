using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_Worker : NPC
{
    [HideInInspector] public CustomerQueue customerQue;
    [HideInInspector] public NPC_Customer currentCustomer;
    private void Start()
    {
        StateMachine.Initialize(WaitForCustomerState);
        state = NPCState.WaitingForCustomer;
        customerQue = targetShop.GetComponentInChildren<CustomerQueue>();
        currentCustomer = null;
    }
    private void NPCAI_Worker()
    {
        switch (state)
        {
            case NPCState.Idle:
                break;
            case NPCState.Walking:
                break;
            case NPCState.PickedUpWalking:
                break;
            case NPCState.GoingForQue:
                break;
            case NPCState.Queued:
                break;
            case NPCState.WaitingForWorker:
                break;
            case NPCState.WaitingForCustomer:
                Debug.Log("Biri var mý kontrol");
                //If there is customer and its ready to buy (animations over and customer in 1st place on que)
                if (!customerQue.queSlotList[0]._isSlotEmpty && customerQue.queSlotList[0].npc.state == NPCState.WaitingForWorker)
                {
                    Debug.Log("Müþteri bulundu");
                    if (currentCustomer != customerQue.queSlotList[0].npc)
                    {
                        //Debug.Log(customerQue.queSlotList[0].npc);
                        Debug.Log("Ayný deðil iþe baþla");
                        currentCustomer = customerQue.queSlotList[0].npc;
                        state = NPCState.HandlingCustomer;
                    }
                }
                break;
            case NPCState.HandlingCustomer:
                //Checking if shop have that item on sale.
                if (targetShop.IsShopHaveItem(currentCustomer.wantToBuy))
                {
                    //Get the item from rack and give to customer.
                    MoveTo(targetShop.ReturnItemPickUpPos(currentCustomer.wantToBuy));
                    state = NPCState.TakingItemFromRack;
                }
                else
                {
                    //Shop dont have that item.
                }
                break;
            case NPCState.TakingItemFromRack:
                if (agent.remainingDistance < 0.1f && !agent.pathPending)
                {
                    Item _item = targetShop.ReturnItemPickUpPos(currentCustomer.wantToBuy).GetComponent<Slot>()._item;
                    Sequence mySequence = DOTween.Sequence();
                    mySequence.Append(transform.DOLookAt(targetShop.ReturnItemPickUpPos(currentCustomer.wantToBuy).position, 0.5f,AxisConstraint.Y))
                    .OnComplete(() =>
                    {
                        PickItem();
                        _item.PickUp(handPos);
                        itemInHand = _item;
                        state = NPCState.ReturningFromRackWithItem;
                        mySequence.Kill();
                    });
                    state = NPCState.PlayingAnim;
                }
                break;
            case NPCState.ReturningFromRackWithItem:
                //Dont move till pick or drop animation finish
                if (!isPickDropAnimPlaying)
                {
                    MoveTo(targetShop.workerSalePos);
                    state = NPCState.GiveItemToCustomer;
                }
                break;
            case NPCState.GiveItemToCustomer:
                
                if (agent.remainingDistance < 0.1f && !agent.pathPending)
                {
                    if (!isPickDropAnimPlaying)
                    {
                        PlaceItemToStall();
                    }
                }
                break;
            default:
                break;
        }
    }
    public void PlaceItemToStall()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOLookAt(targetShop.stallSlotPos.position, 0.5f, AxisConstraint.Y))
        .OnComplete(() =>
        {
            DropItem();
            itemInHand.transform.parent = null;
            itemInHand.transform.DOMove(targetShop.stallSlotPos.position, 0.5f)
            .OnComplete(() => 
            {
                targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item = itemInHand;
                targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty = false;
                itemInHand = null;
            });
            state = NPCState.WaitingForCustomer;
            mySequence.Kill();
        });
    }
    private void Update()
    {
        StateMachine.CurrentNPCState.FrameUpdate();
        //NPCAI_Worker();

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

}
