using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    public List<Shop> shopList = new List<Shop>();
    public List<SO_Item> SOItemList = new List<SO_Item>();
    public List<NPC_Customer> npcCustomerList = new List<NPC_Customer>();

    public Transform chatBubble;
    public Transform deSpawnTransform;


    public void Deneme(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SetCustomerToShop();
        }
        
    }


    public Shop GetShopForCustomer(NPC_Customer _npc)
    {
        for (int i = 0; i < shopList.Count; i++)
        {
            if (!shopList[i].GetComponentInChildren<CustomerQueue>()._isQueueFull)
            {
                shopList[i].GetComponentInChildren<CustomerQueue>().AddCustomerToQueue(_npc);
                return shopList[i];
            }
        }
        return null;
    }

    private void SetCustomerToShop()
    {
        for (int i = 0; i < npcCustomerList.Count; i++)
        {
            npcCustomerList[i].GetComponent<NPC_Customer>().SetCustomer(SOItemList[0]);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    private void Update()
    {
        
    }
}
