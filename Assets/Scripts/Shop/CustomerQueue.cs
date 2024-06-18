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
    public void RemoveCustomerFromQue(NPC_Customer _npc)
    {
        for (int i = 0; i < queSlotList.Count; i++)
        {
            if (!queSlotList[i]._isSlotEmpty)
            {
                if(queSlotList[i].npc == _npc)
                {
                    queSlotList[i]._isSlotEmpty = true;
                    queSlotList[i].npc = null;
                    ReOrderQue(i);
                }
            }
        }
    }


    public void ReOrderQue(int _fromThisIndex)
    {
        for (int i = _fromThisIndex; i < queSlotList.Count; i++)
        {
            if (i + 1 < queSlotList.Count)
            {
                queSlotList[i]._isSlotEmpty = queSlotList[i + 1]._isSlotEmpty;
                queSlotList[i].npc = queSlotList[i + 1].npc;
            }

        }
    }

}
