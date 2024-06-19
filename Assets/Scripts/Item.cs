using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour , IPickable , IInteractable
{
    public enum ItemType 
    {
        Weapon,
        Armor,
        Shield
    }

    [HideInInspector] public ItemType itemType;
    [HideInInspector] public bool isAnimCompleted;
    [HideInInspector] public string itemName;
    [HideInInspector] public int itemPrice;
    [HideInInspector] public string itemDesc;
    [HideInInspector] public int itemMaxStackCount;
    [HideInInspector] public bool isItemStackable;
    [HideInInspector] public bool isItemInRack;
    public SO_Item _SOItem;
    private void Awake()
    {
        itemType = _SOItem._itemType;
        itemPrice = _SOItem._itemPrice;
        itemName = _SOItem._itemName;
        itemDesc = _SOItem._itemDesc;
        itemMaxStackCount = _SOItem._itemMaxStackCount;
        isItemStackable = _SOItem._isItemStackable;
    }


    private void Start()
    {
        isAnimCompleted = true;
    }
    //Playercontroller calling this function when pressed E
    public void Interact(Transform _player)
    {
        if (isAnimCompleted)
        {
            if(_player.GetComponent<PlayerController>()._itemInHand == null)
            {
                _player.GetComponent<PlayerController>()._itemInHand = this;
                PickUp(_player.GetComponent<PlayerController>()._handPos);
            }
            else
            {
                Debug.Log("El dolu");
            }
            
            
        }

    }

    public void PickUp(Transform _handPos)
    {
        //trigger false for unwanted collisions
        GetComponent<Collider>().isTrigger = true;
        //if item already in rack
        if (isItemInRack)
        {
            //Setting slot to empty
            transform.parent.GetComponent<Slot>()._isEmpty = true;
            transform.parent.GetComponent<Slot>()._item = null;
            //Removing item from shop item list
            transform.GetComponentInParent<Shop>().RemoveItemFromList(this);
            isItemInRack = false;
        }
        //Item's parent is hand pos
        transform.parent = _handPos.transform;
        //Animation gonna start so setting the bool
        isAnimCompleted = false;
        //Creating move and rotate sequence for pickup anim
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOLocalMove(new Vector3(0, 0, 0), 0.6f))
            .Join(transform.DOLocalRotate(new Vector3(-35, 0, 90),0.2f))
            .Insert(0.3f,transform.DOLocalRotate(new Vector3(-35, 0, 90), 0.3f,RotateMode.FastBeyond360).SetEase(Ease.OutBack))
            .OnComplete(() => {

                //setting boolean when anim completed
                isAnimCompleted = true;
                mySequence.Kill();
            });
        //Destroying rigidbody for unwanted movements
        Destroy(GetComponent<Rigidbody>());
    }
}
