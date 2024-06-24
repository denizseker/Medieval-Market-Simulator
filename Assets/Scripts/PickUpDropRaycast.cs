using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDropRaycast : MonoBehaviour
{

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }




    // Update is called once per frame
    void Update()
    {
        //if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit, interactDistance, interactableLayer))
        //{
        //    Transform _object = raycastHit.transform;

        //    _object.GetComponent<Outline>().ShowOutline();
        //}
    }
}
