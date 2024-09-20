using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInterestForShop : MonoBehaviour
{
    //Triggering when npc enters the interest area from shops
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InterestArea"))
        {
            //if the NPC is in FreeRoamState, change the state to ShopInterestState
            if (GetComponent<NPC>() is NPC npc && npc.StateMachine.CurrentNPCState == npc.FreeRoamState)
            {
                npc.targetShop = other.GetComponentInParent<Shop>();
                npc.StateMachine.ChangeState(npc.ShopInterestState);
            }
        }
    }
}
