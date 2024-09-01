using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_TakeItemFromStall : NPCState
{
    public NPC_State_TakeItemFromStall(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);
        
    }

    public override void EnterState()
    {
        base.EnterState();
        Item _item = npc.targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item;

        npc.transform.DOLookAt(npc.targetShop.stallSlotPos.transform.position, 0.5f, AxisConstraint.Y, Vector3.up)
            .OnComplete(() =>
            {
                npc.PlayPickAnim();
                _item.PickUp(npc.handPos,
            () =>
                {
                    npc.targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item = null;
                    npc.itemInHand = _item;
                    npc.StateMachine.ChangeState(npc.DeQueFromShopState);

                });
                npc.targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty = true;
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
