using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_State_InQue : NPCState
{
    NPC_Customer customer;
    private bool isMovingToNext = false;

    public NPC_State_InQue(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        customer = npc.GetComponent<NPC_Customer>();
        npc.targetShop.customerQue.onQueChange.AddListener(MoveNext);
        CheckQueSituation();

    }

    public override void ExitState()
    {
        base.ExitState();
        npc.targetShop.customerQue.onQueChange.RemoveListener(MoveNext);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        //Check if npc is moving to next slot in que
        if (isMovingToNext)
        {
            if (npc.agent.remainingDistance < 0.1f && !npc.agent.pathPending)
            {
                isMovingToNext = false;
                CheckQueSituation();
            }
        }
    }

    //Move to next slot in que triggered by onQueChange event from CustomerQue
    private void MoveNext()
    {
        npc.MoveTo(npc.targetShop.ReturnQueSlot(npc).transform);
        isMovingToNext = true;

    }

    //Check if npc is first in que and ask for item
    private void AskForItem()
    {
        Debug.Log("Asking for item");
        if (npc.targetShop.IsShopHaveItem(npc.GetComponent<NPC_Customer>().wantToBuy))
        {
            //There is worker NPC in shop.
            if (npc.targetShop.worker != null)
            {

                ChatBubble.Create(npc.gameObject.transform, customer.wantToBuy, "I’d like this one. It suits me well.", 1);
                npc.transform.DOLookAt(npc.targetShop.stallSlotPos.transform.position, 1f, AxisConstraint.Y, Vector3.up)
                .OnComplete(() =>
                {
                    npcStateMachine.ChangeState(npc.WaitForWorkerState);
                });
            }
            //Player should handle customers
            else
            {
                ChatBubble.Create(npc.gameObject.transform, customer.wantToBuy, "I’d like this one. It suits me well.", 2);
                npc.transform.DOLookAt(npc.targetShop.stallSlotPos.transform.position, 1f, AxisConstraint.Y, Vector3.up);
                npcStateMachine.ChangeState(npc.WaitForWorkerState);
            }
        }
        //If shop doesn't have the item
        else
        {
            ChatBubble.Create(npc.gameObject.transform, npc.GetComponent<NPC_Customer>().wantToBuy, "I can't find what I want.", 1, () => 
            {
                npc.StateMachine.ChangeState(npc.DeQueFromShopState);
            });
            npc.transform.DOLookAt(npc.targetShop.stallSlotPos.transform.position, 1f, AxisConstraint.Y, Vector3.up);

        }
    }

    //Check npc's situation in que
    private void CheckQueSituation()
    {
        if (npc.targetShop.WhichPlaceAtQue(npc) == 0)
        {

            AskForItem();
        }
        //if npc is not on 1st place in que
        else
        {
            WaitForQue();
        }
    }
    //Wait for npc's turn in que
    private void WaitForQue()
    {
        npc.transform.DOLookAt(npc.targetShop.ReturnPreviousQueSlot(npc).transform.position, 0.5f, AxisConstraint.Y, Vector3.up);
    }
}
