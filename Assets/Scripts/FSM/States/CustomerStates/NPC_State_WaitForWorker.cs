using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_WaitForWorker : NPCState
{
    NPC_Customer customer;


    public NPC_State_WaitForWorker(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
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
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (!npc.targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty)
        {
            npcStateMachine.ChangeState(npc.PayForItemState);
        }

    }  

}
