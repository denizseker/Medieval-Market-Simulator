using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public enum ShopType
    {
        WeaponShop,
        ArmorShop,
        ButcherShop,
        FishShop,
        BazaarShop,
        JeweleryShop
    }

    protected ShopType shopType;
    public List<Item> itemList = new List<Item>();
    [SerializeField] protected List<Rack> rackList = new List<Rack>();
    public CustomerQueue customerQue;
    public Transform workerSalePos;
    public Transform stallSlotPos;

    public bool IsShopHaveItem(SO_Item _SOItem)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i]._SOItem == _SOItem)
            {
                return true;
            }
        }
        return false;
    }


    public Transform ReturnItemPickUpPos(SO_Item _SOItem)
    {
        //Checking every rack in shop
        for (int i = 0; i < rackList.Count; i++)
        {
            //Checking each rack's slots
            for (int j = 0; j < rackList[i].slotList.Count; j++)
            {
                //if slot have any item
                if(!rackList[i].slotList[j]._isEmpty)
                {
                    //if slot have that spesific item
                    if (rackList[i].slotList[j]._item._SOItem == _SOItem)
                    {
                        //returning slot transform so worker can go there and pick up that item.
                        return rackList[i].slotList[j].transform;
                    }
                }
                
            }
        }
        //cant find that spesific item in any rack at this shop.
        return null;
    }
    public void RemoveItemFromList(Item _item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i] == _item)
            {
                itemList.Remove(_item);
                return;
            }
        }
    }

    public CustomerQueueSlot ReturnQueSlot(NPC _npc)
    {
        for (int i = 0; i < customerQue.queSlotList.Count; i++)
        {
            if(customerQue.queSlotList[i].npc == _npc)
            {
                return customerQue.queSlotList[i];
            }
        }
        Debug.Log("null");
        return null;
    }
    public int WhichPlaceAtQue(NPC _npc)
    {
        for (int i = 0; i < customerQue.queSlotList.Count; i++)
        {
            if (customerQue.queSlotList[i].npc == _npc)
            {
                return i;
            }
        }
        return 0;
    }
    public CustomerQueueSlot ReturnPreviousQueSlot(NPC _npc)
    {
        for (int i = 0; i < customerQue.queSlotList.Count; i++)
        {
            if (customerQue.queSlotList[i].npc == _npc)
            {
                if (i == 0) return customerQue.queSlotList[0];
                return customerQue.queSlotList[i-1];
            }
        }
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.shopList.Add(this);
    }

}
