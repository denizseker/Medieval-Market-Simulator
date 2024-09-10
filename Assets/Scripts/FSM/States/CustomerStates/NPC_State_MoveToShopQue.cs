using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_MoveToShopQue : NPCState
{
    Transform target;

    public NPC_State_MoveToShopQue(NPC _npc, NPCStateMachine _npcStateMachine, Transform _target) : base(_npc, _npcStateMachine)
    {
        target = _target;
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        npc.MoveTo(target);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(npc.agent.remainingDistance < 0.1f && !npc.agent.pathPending)
        {
            npcStateMachine.ChangeState(npc.InQueState);
        }
    }
}
