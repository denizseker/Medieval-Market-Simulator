using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_State_PickUp : NPCState
{

    NPC_Worker worker;

    public NPC_State_PickUp(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        worker = npc.GetComponent<NPC_Worker>();

        Item _item = worker.targetShop.ReturnItemPickUpPos(worker.currentCustomer.wantToBuy).GetComponent<Slot>()._item;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(worker.transform.DOLookAt(worker.targetShop.ReturnItemPickUpPos(worker.currentCustomer.wantToBuy).position, 0.5f, AxisConstraint.Y))
        .OnComplete(() =>
        {
            worker.PickItem();
            _item.PickUp(worker.handPos);
            worker.itemInHand = _item;
            mySequence.Kill();
            npcStateMachine.ChangeState(npc.ReturnFromRackWithItemState);

        });
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
}
