using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerQueue : MonoBehaviour
{
    public List<CustomerQueueSlot> queSlotList = new List<CustomerQueueSlot>();
    public bool _isQueueFull;


    public UnityEvent onQueChange;


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
                    onQueChange.Invoke();
                    break;
                }
            }
        }
    }

    public void ReOrderQue(int _fromThisIndex)
    {
        for (int i = _fromThisIndex; i < queSlotList.Count - 1; i++)
        {
            if (queSlotList[i + 1]._isSlotEmpty)
            {
                break;
            }
            queSlotList[i].npc = queSlotList[i + 1].npc;
            queSlotList[i]._isSlotEmpty = false;
            queSlotList[i + 1].npc = null;
            queSlotList[i + 1]._isSlotEmpty = true;
        }
    }

}
