
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform cameraTransform;

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
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit, 6f, interactableLayer))
        {
            highlight = raycastHit.transform;
            if (highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                    highlight.gameObject.GetComponent<Outline>().ShowOutline();

                }
            }
            else
            {
                highlight = null;
            }
        }

        // Selection
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (highlight)
        //    {
        //        if (selection != null)
        //        {
        //            selection.gameObject.GetComponent<Outline>().enabled = false;
        //        }
        //        selection = raycastHit.transform;
        //        selection.gameObject.GetComponent<Outline>().enabled = true;
        //        highlight = null;
        //    }
        //    else
        //    {
        //        if (selection)
        //        {
        //            selection.gameObject.GetComponent<Outline>().enabled = false;
        //            selection = null;
        //        }
        //    }
        //}
    }

}

