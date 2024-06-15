using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    public List<CustomerQueueSlot> queSlotList = new List<CustomerQueueSlot>();
    public bool _isQueueFull;


    public CustomerQueueSlot AddCustomerToQueue(NPC_Customer _npc)
    {
        for (int i = 0; i < queSlotList.Count; i++)
        {
            if (queSlotList[i]._isSlotEmpty)
            {
                queSlotList[i]._isSlotEmpty = false;
                queSlotList[i].npc = _npc;
                return queSlotList[i];
            }
        }
        _isQueueFull = true;
        return null;
    }
}
