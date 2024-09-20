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
        Debug.Log("ShopInterestState");
        npc.transform.DOLookAt(npc.targetShop.stallSlotPos.transform.position, 0.5f, AxisConstraint.Y, Vector3.up);
        npc.animator.SetBool("InterestIdle", true);
        DOVirtual.DelayedCall(2, () =>
        {
            npc.StateMachine.ChangeState( npc.MoveToShopQueueState);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
