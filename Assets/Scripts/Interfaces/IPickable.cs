using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    public void PickUp(Transform _handPos,System.Action onAnimCompleted);
    public void DropToGround();
    public void DropToPoint(Transform _point);
}
