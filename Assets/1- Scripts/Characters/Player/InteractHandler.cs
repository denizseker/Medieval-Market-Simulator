
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InteractHandler : MonoBehaviour
{
    private PlayerController playerController;
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
            //Rigidbody _rb = playerController._itemInHand.gameObject.AddComponent<Rigidbody>();
            Rigidbody _rb = playerController._itemInHand.GetComponent<Rigidbody>();
            _rb.isKinematic = false;
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
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
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit, interactDistance, interactableLayer))
            {
                Transform target = raycastHit.transform;
                //target is interactable

                if (target.TryGetComponent(typeof(IInteractable), out Component component))
                {
                    //hand is empty, interact with object.
                    if (playerController._itemInHand == null)
                    {
                        target.GetComponent<IInteractable>().Interact(transform);
                    }
                    //hand is not empty
                    else
                    {
                        //hand not empty and interacted with rack
                        if (target.GetComponent<Rack>() != null)
                        {
                            target.GetComponent<IInteractable>().Interact(transform);
                        }
                        //hand not empty and interacted with another item
                        if (target.GetComponent<Item>() != null)
                        {
                            if (playerController._itemInHand.isAnimCompleted)
                            {
                                DropItem();
                                target.GetComponent<IInteractable>().Interact(transform);
                            }
                        }
                        //hand not empty interacted with label
                        if (target.GetComponent<Label>() != null)
                        {
                            target.GetComponent<IInteractable>().Interact(transform);
                        }
                        //hand not empty interacted with NPC
                        if (target.GetComponent<NPC>() != null)
                        {
                            target.GetComponent<IInteractable>().Interact(transform);
                        }
                        if (target.GetComponent<Door>() != null)
                        {
                            target.GetComponent<IInteractable>().Interact(transform);
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
        //if(selection != null)
        //{
        //    Outline outline = selection.gameObject.GetComponent<Outline>();
        //    outline.ShowOutline();
        //}

        //// Raycast iþlemi
        //Debug.DrawRay(cameraTransform.position, cameraTransform.forward * interactDistance, Color.red);
        //if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit, interactDistance, interactableLayer))
        //{
        //    // Yeni hedefe bakýyoruz
        //    target = raycastHit.transform;

        //    if (target != selection)  // Ayný hedef deðilse
        //    {
        //        Outline outline = target.gameObject.GetComponent<Outline>();
        //        Debug.Log(outline);
        //        if (outline != null)
        //        {
        //            selection = target;
        //        }

        //        // Tooltip gösterme iþlemi
        //        Item item = target.gameObject.GetComponent<Item>();
        //        if (item != null)
        //        {
        //            TooltipScreenSpaceUI.ShowTooltip_Static(item.itemName, item.itemDesc, item.itemType, item.itemPrice);
        //        }
        //    }
        //    else
        //    {
        //        // Eðer highlight selection ile aynýysa sýfýrla
        //        target = null;
        //    }
        //}
        //else
        //{
        //    if(selection != null) // Eðer selection null deðilse (yani bir þey seçiliyse)
        //    {
        //        Outline outline = selection.gameObject.GetComponent<Outline>();
        //        outline.HideOutline();
        //        selection = null;
        //    }

        //}

        


    }

}

