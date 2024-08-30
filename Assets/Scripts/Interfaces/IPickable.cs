using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    public void PickUp(Transform _handPos);
    public void DropToGround();
    public void DropToPoint(Transform _point);
}
