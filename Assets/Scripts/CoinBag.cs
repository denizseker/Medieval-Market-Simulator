using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBag : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Transform _player)
    {
        NPC npc = GetComponentInParent<NPC>();

        if(npc.StateMachine.CurrentNPCState == npc.PayForItemState)
        {
            npc.animator.SetBool("HandTheMoney", false);
            npc.PayForItemState.InteractedCharacterTookMoney();
        }
    }

}
