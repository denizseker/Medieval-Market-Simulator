using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_Worker : NPC
{
    private Shop shop;
    private CustomerQueue customerQue;
    private NPC_Customer currentCustomer;
    private void Start()
    {
        state = NPCState.WaitingForCustomer;
        shop = GetComponentInParent<Shop>();
        customerQue = shop.GetComponentInChildren<CustomerQueue>();
    }

    private void TakeItemFromRack(SO_Item _SOItem)
    {

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
                //If there is customer and its ready to buy (animations over and customer in 1st place on que)
                if(!customerQue.queSlotList[0]._isSlotEmpty && customerQue.queSlotList[0].npc.state == NPCState.WaitingForWorker)
                {
                    Debug.Log("Customer came and waiting for item.");
                    currentCustomer = customerQue.queSlotList[0].npc;
                    state = NPCState.HandlingCustomer;
                }
                break;
            case NPCState.HandlingCustomer:
                //Checking if shop have that item on sale.
                if (shop.IsShopHaveItem(currentCustomer.wantedSOItem))
                {
                    //Get the item from rack and give to customer.
                    MoveTo(shop.ReturnItemPickUpPos(currentCustomer.wantedSOItem));
                    state = NPCState.TakingItemFromRack;
                }
                else
                {
                    //Shop dont have that item.
                }
                break;
            case NPCState.TakingItemFromRack:
                if (agent.remainingDistance < 0.1f)
                {
                    Item _item = shop.ReturnItemPickUpPos(currentCustomer.wantedSOItem).GetComponent<Slot>()._item;
                    Sequence mySequence = DOTween.Sequence();
                    mySequence.Append(transform.DOLookAt(shop.ReturnItemPickUpPos(currentCustomer.wantedSOItem).position, 0.5f,AxisConstraint.Y))
                    //.Append(_item.transform.DOMove(handPos.transform.position,0.3f))
                    .OnComplete(() =>
                    {
                        PickItem();
                        _item.PickUp(handPos);
                        Debug.Log("Took item");
                        state = NPCState.ReturningFromRackWithItem;
                    });
                    state = NPCState.Idle;
                    
                }
                break;
            case NPCState.ReturningFromRackWithItem:
                //Dont move till pick or drop animation finish
                if (!isPickDropAnimPlaying)
                {
                    MoveTo(shop.workerSalePos);
                }
                
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        NPCAI_Worker();

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
