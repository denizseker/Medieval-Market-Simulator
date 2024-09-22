using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName ="ScriptableObject/Item")]
public class SO_Item : ScriptableObject
{
    public Item.ItemType _itemType;
    public int _itemPrice;
    public string _itemName;
    public string _itemDesc;
    public int _itemMaxStackCount;
    public bool _isItemStackable;
    public Sprite _itemSprite;

    public GameObject _itemPrefab;
}
