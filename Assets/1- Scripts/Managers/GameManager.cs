using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform chatBubble;
    public Transform deSpawnTransform;


    public List<Shop> shopList = new List<Shop>();
    public List<SO_Item> SOItemList = new List<SO_Item>();
    public List<NPC_Customer> npcCustomerList = new List<NPC_Customer>();

    


    //events
    public UnityEvent onGoldChange;
    public UnityEvent onReputationChange;
    //player stats
    public int PlayerGold { get; private set; } = 500;
    public int PlayerReputation { get; private set; } = 0;

    public void Deneme(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
        }
        
    }

    public Shop GetShopForCustomer(NPC_Customer npc)
    {
        foreach (var shop in shopList)
        {
            var customerQueue = shop.GetComponentInChildren<CustomerQueue>();
            if (!customerQueue._isQueueFull)
            {
                customerQueue.AddCustomerToQueue(npc);
                return shop;
            }
        }
        return null;
    }

    public SO_Item GetRandomItem()
    {
        return SOItemList[Random.Range(0, SOItemList.Count)];
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


    public void ChangeGold(int _gold)
    {
        PlayerGold += _gold;
        onGoldChange.Invoke();
    }
    public void ChangeReputation(int _reputation)
    {
        PlayerReputation += _reputation;
        onReputationChange.Invoke();
    }

}
