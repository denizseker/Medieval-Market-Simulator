using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_State_InQue : NPCState
{
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

        if (npc.targetShop.WhichPlaceAtQue(npc) == 0)
        {
            npc.transform.DOLookAt(npc.targetShop.transform.position, 1f, AxisConstraint.Y, Vector3.up)
            .OnComplete(() =>
            {
                npcStateMachine.ChangeState(npc.WaitForWorkerState);
            });
        }
        //if npc is not on 1st place in que
        else
        {
            npc.transform.DOLookAt(npc.targetShop.ReturnPreviousQueSlot(npc).transform.position, 0.5f, AxisConstraint.Y, Vector3.up);
        }

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (npc.targetShop.WhichPlaceAtQue(npc) == 0)
        {
            //npcStateMachine.ChangeState(new NPC_State_WaitForWorker(npc,npcStateMachine));
        }
        else
        {
            npc.MoveTo(npc.targetShop.ReturnQueSlot(npc).transform);
        }
    }
}
