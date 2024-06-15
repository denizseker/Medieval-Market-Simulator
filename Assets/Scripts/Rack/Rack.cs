using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Rack : MonoBehaviour , IInteractable , IPickable
{
    public List<Slot> slotList = new List<Slot>();
    public bool isRackFull;

    public abstract void PlaceItemToRack(Transform _player);
   
    public void PickUp(Transform _handPos)
    {
        
    }
    public Slot GetSlot()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].GetComponent<Slot>()._isEmpty)
            {
                return slotList[i];
            }
        }
        Debug.Log("Rack is full");
        isRackFull = true;
        return null;
    }
    public void Interact(Transform _player)
    {
        PlayerController _playerController = _player.GetComponent<PlayerController>();

        //player holding something
        if (_playerController._itemInHand != null)
        {
            PlaceItemToRack(_player);
        }
    }
}
