using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState
{
    protected NPC npc;
    protected NPCStateMachine npcStateMachine; 

    public NPCState(NPC _npc,NPCStateMachine _npcStateMachine)
    {
        this.npc = _npc;
        this.npcStateMachine = _npcStateMachine;
    }

    public virtual void EnterState() 
    {

        npc.stateText.text = npc.StateMachine.CurrentNPCState.ToString();
    
    }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType) { }

}
