using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_ShopInterest : NPCState
{
    public NPC_State_ShopInterest(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        npc.transform.DOLookAt(npc.targetShop.stallSlotPos.transform.position, 0.5f, AxisConstraint.Y, Vector3.up);
        npc.animator.SetBool("InterestIdle", true);
        SO_Item tempWantToBuy = GameManager.Instance.GetRandomItem();

        DOVirtual.DelayedCall(Random.Range(1.5f, 5f), () =>
        {
            if (npc.targetShop.IsShopHaveItem(tempWantToBuy))
            {
                ChatBubble.Create(npc.gameObject.transform, tempWantToBuy, "I found what I want.", 1);
                npc.GetComponent<NPC_Customer>().wantToBuy = tempWantToBuy;
                npc.StateMachine.ChangeState(npc.MoveToShopQueueState);
            }
            else
            {
                ChatBubble.Create(npc.gameObject.transform, tempWantToBuy, "I can't find what I want.", 1);
                npc.StateMachine.ChangeState(npc.FreeRoamState);
            }
            
        });
    }

    public override void ExitState()
    {
        base.ExitState();
        npc.animator.SetBool("InterestIdle", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
}
