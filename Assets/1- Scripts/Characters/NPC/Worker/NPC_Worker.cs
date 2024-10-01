using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC_Worker : NPC , IInteractable
{
    [HideInInspector] public CustomerQueue customerQue;
    [HideInInspector] public NPC_Customer currentCustomer;
    [HideInInspector] public NPC_Customer previousCustomer;

    private void Start()
    {
        // Initialize the state machine with the WaitForCustomerState
        StateMachine.Initialize(WaitForCustomerState);
        // Get the customer queue from the target shop
        targetShop = GetComponentInParent<Shop>();
        customerQue = targetShop.GetComponentInChildren<CustomerQueue>();
        // Initialize current and previous customers to null
        currentCustomer = null;
        previousCustomer = null;
        int qualityLevel = QualitySettings.GetQualityLevel();
        string qualityName = QualitySettings.names[qualityLevel];

        Debug.Log("Current Quality Level: " + qualityName);

    }

    public void PlaceItemToStall()
    {
        // Create a new sequence for the item placement animation
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOLookAt(targetShop.stallSlotPos.position, 0.5f, AxisConstraint.Y))
        .OnComplete(() =>
        {
            // Drop the item and move it to the stall slot position
            
            itemInHand.transform.parent = null;
            itemInHand.transform.DOMove(targetShop.stallSlotPos.position, 0.5f)
            .OnComplete(() =>
            {
                // Update the stall slot to indicate it is no longer empty
                targetShop.stallSlotPos.GetComponent<Slot_Stall>()._item = itemInHand;
                targetShop.stallSlotPos.GetComponent<Slot_Stall>()._isEmpty = false;
                itemInHand = null;
            });
        });
    }

    private void Update()
    {
        // Update the current state of the state machine
        StateMachine.CurrentNPCState.FrameUpdate();

        // Check if the agent has reached its destination and update animations accordingly
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
        throw new System.NotImplementedException();
    }
}
