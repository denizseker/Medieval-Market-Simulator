using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_State_PayForItem : NPCState
{
    public bool isAnimDone = false;

    public NPC_State_PayForItem(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);

        if(_triggerType == NPC.AnimationTriggerType.AnimationEnded) isAnimDone = true;

    }

    public override void EnterState()
    {
        base.EnterState();
        ChatBubble.Create(npc.transform, "Exactly what I wanted. Here is your money.", 2);
        npc.GetComponent<NPC_Customer>().coinBag.gameObject.SetActive(true);
        npc.PlayHandTheMoneyAnim();
    }


    public void InteractedCharacterTookMoney()
    {
        npc.StateMachine.ChangeState(npc.TakeItemFromStallState);
    }

    public override void ExitState()
    {
        base.ExitState();
        npc.GetComponent<NPC_Customer>().coinBag.gameObject.SetActive(false);
        isAnimDone = false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

}
