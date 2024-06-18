using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_WaitForCustomer : NPCState
{
    NPC_Worker worker;

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

        if (!worker.customerQue.queSlotList[0]._isSlotEmpty)
        {
            if (worker.customerQue.queSlotList[0].npc.StateMachine.CurrentNPCState == worker.customerQue.queSlotList[0].npc.WaitForWorkerState)
            {
                worker.currentCustomer = worker.targetShop.customerQue.queSlotList[0].npc;
                npcStateMachine.ChangeState(npc.HandleCustomerState);
            }
        }
    }
}
