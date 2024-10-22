using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCart : MonoBehaviour
{

    public WheelCollider[] wheelColliders;  // Wheel Colliders (ön ve arka tekerlekler için)
    public Transform[] wheelMeshes;         // Görsel tekerlek modelleri (Meshler)

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(new Vector3(0, 0, 10));

        for (int i = 0; i < wheelColliders.Length; i++)
        {
            //UpdateWheelPosition(wheelColliders[i], wheelMeshes[i]);
        }
    }

    private void UpdateWheelPosition(WheelCollider collider, Transform mesh)
    {
        // Tekerlek Collider'ýn pozisyonunu ve dönüþünü alýn
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        // Tekerleðin görsel pozisyonunu ve dönüþünü ayarlayýn
        mesh.position = position;
        mesh.rotation = rotation;
    }

}
