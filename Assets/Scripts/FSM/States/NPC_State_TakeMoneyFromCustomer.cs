using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_State_TakeMoneyFromCustomer : NPCState
{
    NPC_Worker worker;
    public NPC_State_TakeMoneyFromCustomer(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);

    }

    public override void EnterState()
    {
        worker = npc.GetComponent<NPC_Worker>();
        
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private void TookGoldAnimDone()
    {
        
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (worker.currentCustomer.StateMachine.CurrentNPCState == worker.currentCustomer.PayForItemState && worker.currentCustomer.PayForItemState.isAnimDone)
        {
            npc.animator.Play("PickUpWithoutCarry");
            worker.currentCustomer.GetComponentInChildren<CoinBag>().Interact(null);
            npcStateMachine.ChangeState(npc.WaitForCustomerState);
        }
    }


}
