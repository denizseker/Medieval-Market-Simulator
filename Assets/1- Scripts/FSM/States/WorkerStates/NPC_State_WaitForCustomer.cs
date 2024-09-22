using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_WaitForCustomer : NPCState
{
    private NPC_Worker worker;

    public NPC_State_WaitForCustomer(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
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
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        var firstSlot = worker.customerQue.queSlotList[0];
        if (!firstSlot._isSlotEmpty && firstSlot.npc.StateMachine.CurrentNPCState == firstSlot.npc.WaitForWorkerState)
        {
            if (firstSlot.npc != worker.previousCustomer)
            {
                worker.currentCustomer = firstSlot.npc;
                npcStateMachine.ChangeState(npc.HandleCustomerState);
            }
        }
    }
}
