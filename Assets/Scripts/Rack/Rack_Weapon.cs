using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rack_Weapon : Rack
{
    public override void PlaceItemToRack(Transform _player)
    {
        Item _item = _player.GetComponent<PlayerController>()._itemInHand;
        if (_item.isAnimCompleted)
        {
            //type comparision
            if (_item.itemType == Item.ItemType.Weapon)
            {
                //trying to get emtpy slot
                _item.transform.parent = GetSlot().transform;
                //if there is empty slot
                if (_item.transform.parent != null)
                {
                    _item.GetComponent<Collider>().isTrigger = true;
                    _item.isAnimCompleted = false;
                    //Creating move and rotate sequence for pickup anim
                    Sequence mySequence = DOTween.Sequence();
                    mySequence.Append(_item.transform.DOLocalMove(new Vector3(0, 0, 0), 0.4f))
                        .Join(_item.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f))
                        .Insert(0.3f, _item.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.OutBack))
                        .OnComplete(() => {

                        //setting boolean when anim completed
                        _item.isItemInRack = true;
                            _item.isAnimCompleted = true;
                            _item.transform.parent.GetComponent<Slot>()._item = _item;
                            _item.transform.parent.GetComponent<Slot>()._isEmpty = false;
                            GetComponentInParent<Shop>().itemList.Add(_item);
                            _player.GetComponent<PlayerController>()._itemInHand = null;
                            mySequence.Kill();

                        });
                }
            }
        }
    }
}
