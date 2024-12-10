using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    private float openAngle = 0f; // Kapý açýldýðýnda 0 dereceye gelecek
    private float closeAngle = 90f; // Kapý kapandýðýnda 90 dereceye gelecek
    private float duration = 1f; // Animasyon süresi

    public void Interact(Transform _playerTransform)
    {
        if (isOpen)
        {
            RotateDoor(closeAngle);
        }
        else
        {
            RotateDoor(openAngle);
        }

        isOpen = !isOpen;
    }

    private void RotateDoor(float targetAngle)
    {
        transform.DORotate(new Vector3(0, targetAngle, 0), duration);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
