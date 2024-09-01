using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_HandleCustomer : NPCState
{
    NPC_Worker worker;

    public NPC_State_HandleCustomer(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);
        Debug.Log("???");
    }

    public override void EnterState()
    {
        base.EnterState();
        worker = npc.GetComponent<NPC_Worker>();

        if (worker.targetShop.IsShopHaveItem(worker.currentCustomer.wantToBuy))
        {
            //Get the item from rack and give to customer.
            worker.MoveTo(worker.targetShop.ReturnItemPickUpPos(worker.currentCustomer.wantToBuy));
            ChatBubble.Create(npc.transform, "This one’s a good pick. I’ll get it ready for you.",1);
        }
        else
        {
            //TODO DONE: Set worker to wait for another customer and set current customer to leave que
            ChatBubble.Create(npc.transform, "Sorry sir. I don't have that one at the moment.", 1,
                () => 
                {
                    worker.currentCustomer.StateMachine.ChangeState(worker.currentCustomer.DeQueFromShopState);
                    npcStateMachine.ChangeState(npc.WaitForCustomerState);

                });
            
            
        }

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (npc.agent.remainingDistance < 0.1f && !npc.agent.pathPending && npc.agent.hasPath)
        {
            npcStateMachine.ChangeState(npc.TakeItemFromRack);
        }
    }
}
