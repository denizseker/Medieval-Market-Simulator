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
    protected CustomerQueue customerQue;
    public Transform workerSalePos;

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
        for (int i = 0; i < rackList.Count; i++)
        {
            for (int j = 0; j < rackList[i].slotList.Count; j++)
            {
                Debug.Log("1"+ rackList[i]);
                Debug.Log("2" + rackList[i].slotList[j]);
                Debug.Log("3" + rackList[i].slotList[j]._item._SOItem);
                if (rackList[i].slotList[j]._item._SOItem == _SOItem)
                {
                    return rackList[i].slotList[j].transform;
                }
            }
        }
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
        return null;
    }

    public CustomerQueueSlot ReturnPreviousQueSlot(NPC _npc)
    {
        for (int i = 0; i < customerQue.queSlotList.Count; i++)
        {
            if (customerQue.queSlotList[i].npc == _npc)
            {
                if (i == 0) return null;
                return customerQue.queSlotList[i-1];
            }
        }
        return null;
    }


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.shopList.Add(this);
        customerQue = GetComponentInChildren<CustomerQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
