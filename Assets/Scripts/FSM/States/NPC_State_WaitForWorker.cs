using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_WaitForWorker : NPCState
{
    NPC_Customer customer;
    private bool isAnimDone = false;
    private bool isBubleDone = false;


    public NPC_State_WaitForWorker(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);
        isAnimDone = true;
    }

    public override void EnterState()
    {
        base.EnterState();
        customer = npc.GetComponent<NPC_Customer>();
    }

    public override void ExitState()
    {
        base.ExitState();
        isAnimDone = false;
        isBubleDone = false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (!npc.targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty)
        {
            npc.PickItem();
            Item _item = npc.targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item;
            _item.PickUp(npc.handPos);
            npc.targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty = true;
            npc.targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item = null;
            npc.itemInHand = _item;
            ChatBubble.Create(npc.transform, "Exactly what I wanted.",1, () => isBubleDone = true);
        }
        //Waiting till character anim and bubble anim finishes
        if(isAnimDone && isBubleDone)
        {
            npc.targetShop.GetComponentInChildren<CustomerQueue>().RemoveCustomerFromQue(npc.GetComponent<NPC_Customer>());
            npcStateMachine.ChangeState(npc.DeQueFromShop);
        }
    }  

}
