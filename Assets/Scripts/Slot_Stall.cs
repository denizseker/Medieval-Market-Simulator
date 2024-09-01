using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Stall : MonoBehaviour
{
    public Item _item;
    public bool _isEmpty;
    public Material transparentMaterial; // Transparan materyal referansý

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = transparentMaterial;
    }
}
