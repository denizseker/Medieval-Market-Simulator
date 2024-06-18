using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_GiveItemToCustomer : NPCState
{
    NPC_Worker worker;
    public NPC_State_GiveItemToCustomer(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
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
        worker.PlaceItemToStall();

        ChatBubble.Create(npc.transform, "Evir çevir götüne sok");

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
