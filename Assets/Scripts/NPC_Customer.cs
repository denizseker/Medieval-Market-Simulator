using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_Customer : NPC
{

    public SO_Item wantedSOItem;

    private void Start()
    {
        GameManager.Instance.npcCustomerList.Add(this);
    }

    public void SetCustomer(SO_Item _SOItem)
    {
        wantedSOItem = _SOItem;
        targetShop = GameManager.Instance.GetShopForCustomer(this);
        MoveTo(targetShop.ReturnQueSlot(this).transform);
        state = NPCState.GoingForQue;
    }

    private void NPCAI_Customer()
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
                if (agent.remainingDistance < 0.1f)
                {
                    state = NPCState.Queued;
                }
                break;
            case NPCState.Queued:
                if (targetShop.GetComponentInParent<Shop>().ReturnPreviousQueSlot(this) == null)
                {
                    transform.DOLookAt(targetShop.GetComponentInParent<Shop>().transform.position, 0.5f, AxisConstraint.Y, Vector3.up)
                    .OnComplete(() =>
                    {
                        state = NPCState.WaitingForWorker;
                    });
                }
                else
                {
                    transform.DOLookAt(targetShop.GetComponentInParent<Shop>().ReturnPreviousQueSlot(this).transform.position, 0.5f, AxisConstraint.Y, Vector3.up)
                    .OnComplete(() => 
                    {
                        state = NPCState.WaitingForWorker;
                    });
                }
                
                break;
            case NPCState.WaitingForWorker:
                if (!targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty)
                {
                    PickItem();
                    Item _item = targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item;
                    _item.PickUp(handPos);
                    itemInHand = _item;
                    state = NPCState.Idle;
                }
                break;
            default:
                break;
        }
    }


    private void Update()
    {
        NPCAI_Customer();

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

    }
}
