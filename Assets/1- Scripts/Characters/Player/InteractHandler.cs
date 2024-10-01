
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InteractHandler : MonoBehaviour
{
    private PlayerController playerController;
    private Transform highlight;
    private Transform selection;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float interactDistance;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }


    public void DropItem()
    {
        if (playerController._itemInHand.isAnimCompleted)
        {
            //deattach from parent
            playerController._itemInHand.transform.parent = null;
            //adding rigidbody back and setting its values
            Rigidbody _rb = playerController._itemInHand.gameObject.AddComponent<Rigidbody>();
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _rb.useGravity = true;
            _rb.excludeLayers = LayerMask.GetMask("Player");

            //Vector3 deneme = Vector3.ClampMagnitude(_rbPlayer.velocity, _speed * 1.4f);
            _rb.AddForce(playerController._rbPlayer.velocity, ForceMode.VelocityChange);
            _rb.AddForce(((cameraTransform.transform.forward + cameraTransform.transform.up) * 2), ForceMode.VelocityChange);

            //setting trigger false so it can interact with world
            playerController._itemInHand.GetComponentInChildren<Collider>().isTrigger = false;
            //hand is empty now
            playerController._itemInHand = null;
        }
    }

    public void DropItemKeyPressed(InputAction.CallbackContext context)
    {
        if (context.started) //KEY PRESSED
        {
            if (playerController._itemInHand != null)
            {
                DropItem();
            }
        }
            

    }

    public void InteractKeyPressed(InputAction.CallbackContext context)
    {
        if (context.started) //KEY PRESSED
        {
            if (highlight != null) //if something outlined/interactable already
            {
                //target is interactable
                if (highlight.TryGetComponent(typeof(IInteractable), out Component component))
                {
                    //hand is empty, interact with object.
                    if (playerController._itemInHand == null)
                    {
                        highlight.GetComponent<IInteractable>().Interact(transform);
                    }
                    //hand is not empty
                    else
                    {
                        //hand not empty and interacted with rack
                        if (highlight.GetComponent<Rack>() != null)
                        {
                            highlight.GetComponent<IInteractable>().Interact(transform);
                        }
                        //hand not empty and interacted with another item
                        if (highlight.GetComponent<Item>() != null)
                        {
                            if (playerController._itemInHand.isAnimCompleted)
                            {
                                DropItem();
                                highlight.GetComponent<IInteractable>().Interact(transform);
                            }
                        }
                        //hand not empty interacted with label
                        if (highlight.GetComponent<Label>() != null)
                        {
                            highlight.GetComponent<IInteractable>().Interact(transform);
                        }
                        //hand not empty interacted with NPC
                        if (highlight.GetComponent<NPC>() != null)
                        {
                            highlight.GetComponent<IInteractable>().Interact(transform);
                        }
                    }
                }
            }
        }
    }

    //public void InteractKeyPressed(InputAction.CallbackContext context)
    //{
    //    if (context.started && highlight != null && highlight.TryGetComponent<IInteractable>(out var interactable))
    //    {
    //        bool isHandEmpty = playerController._itemInHand == null;

    //        if (interactable != null)
    //        {
    //            interactable.Interact(transform);
    //        }
    //        else if (highlight.GetComponent<Item>() != null && playerController._itemInHand.isAnimCompleted)
    //        {
    //            DropItem();
    //            interactable.Interact(transform);
    //        }
    //    }
    //}


    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            if(highlight.gameObject.GetComponent<Outline>() != null)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = false;
                highlight.gameObject.GetComponent<Outline>().HideOutline();
                highlight = null;
            }
            
        }
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * interactDistance, Color.red);
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit, interactDistance, interactableLayer))
        {
            highlight = raycastHit.transform;
            if (highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                    highlight.gameObject.GetComponent<Outline>().ShowOutline();
                }
                if(highlight.gameObject.GetComponent<Item>() != null)
                {
                    TooltipScreenSpaceUI.ShowTooltip_Static(highlight.gameObject.GetComponent<Item>().itemName, highlight.gameObject.GetComponent<Item>().itemDesc, highlight.gameObject.GetComponent<Item>().itemType, highlight.gameObject.GetComponent<Item>().itemPrice);
                }
            }
            else
            {
                highlight = null;
            }
        }
    }

}

