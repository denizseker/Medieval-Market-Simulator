using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantedItemPreview : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Y ekseninde hareket hýzý
    public float moveRange = 1.0f; // Y ekseninde hareket aralýðý
    public float rotateSpeed = 30.0f; // Dönme hýzý

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Y ekseninde yukarý ve aþaðý hareket
        float newY = startPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Yavaþça dönme
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
