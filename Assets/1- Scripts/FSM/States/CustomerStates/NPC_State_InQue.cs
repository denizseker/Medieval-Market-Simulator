using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_State_InQue : NPCState
{
    NPC_Customer customer;

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
        npc.targetShop.customerQue.deneme.AddListener(CheckQue);
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

    public override void ExitState()
    {
        base.ExitState();
        npc.targetShop.customerQue.deneme.RemoveListener(CheckQue);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    private void CheckQue()
    {
        npc.StateMachine.ChangeState(npc.MoveToShopQueueState);
        //npc.MoveTo(npc.targetShop.ReturnQueSlot(npc).transform);
    }

    private void AskForItem()

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
            ChatBubble.Create(npc.gameObject.transform, customer.wantToBuy, "I’d like this one. It suits me well.",55);
            npc.transform.DOLookAt(npc.targetShop.stallSlotPos.transform.position, 1f, AxisConstraint.Y, Vector3.up);
            npcStateMachine.ChangeState(npc.WaitForWorkerState);
        }
        
    }

    private void WaitForQue()
    {
        npc.transform.DOLookAt(npc.targetShop.ReturnPreviousQueSlot(npc).transform.position, 0.5f, AxisConstraint.Y, Vector3.up);
    }
}
