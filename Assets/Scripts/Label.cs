using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label : MonoBehaviour , IInteractable
{
    Item item;

    public void Interact(Transform _playerTransform)
    {
        UI_InputPanel.ShowInputPanel_Static(item);
    }

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponentInParent<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
