using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_DeQueFromShop : NPCState
{
    public NPC_State_DeQueFromShop(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        npc.targetShop.GetComponentInChildren<CustomerQueue>().RemoveCustomerFromQue(npc.GetComponent<NPC_Customer>());
        npcStateMachine.ChangeState(npc.GoToDespawnState);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
