using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_Customer : NPC , IInteractable
{

    [HideInInspector] public SO_Item wantToBuy;

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        GameManager.Instance.npcCustomerList.Add(this);
    }

    public void SetCustomer(SO_Item _wantToBuy)
    {
        wantToBuy = _wantToBuy;
        targetShop = GameManager.Instance.GetShopForCustomer(this);
        NPC_State_MoveToShopQue moveToShopQue = new NPC_State_MoveToShopQue(this, StateMachine, targetShop.ReturnQueSlot(this).transform);
        StateMachine.ChangeState(moveToShopQue);
        //MoveTo(targetShop.ReturnQueSlot(this).transform);
        //state = NPCState.GoingForQue;
    }
 
    private void Update()
    {
        StateMachine.CurrentNPCState.FrameUpdate();

        if (agent.enabled && !agent.hasPath && !agent.pathPending && agent.remainingDistance == 0 && (animator.GetBool("Walking") || animator.GetBool("CarryWalk")))
        {
            if (_pickedSomething)
            {

                animator.SetBool("CarryWalk", false);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
    }

    public void Interact(Transform _playerTransform)
    {
        Debug.Log("Selam");
    }
}
