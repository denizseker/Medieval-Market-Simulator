using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_ReturnFromRackWithItem : NPCState
{
    NPC_Worker worker;
    public NPC_State_ReturnFromRackWithItem(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
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

        worker.MoveTo(worker.targetShop.workerSalePos);

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (npc.agent.remainingDistance < 0.1f && !npc.agent.pathPending)
        {
            npcStateMachine.ChangeState(npc.GiveItemToCustomerState);
        }
    }
}
