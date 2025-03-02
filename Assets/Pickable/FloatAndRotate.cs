using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAndRotate : MonoBehaviour
{
    public float floatSpeed = 1.0f; // Kecepatan melayang
    public float floatHeight = 0.5f; // Ketinggian melayang
    public float rotationSpeed = 100.0f; // Kecepatan rotasi
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Gerakan melayang atas-bawah menggunakan sin wave
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Rotasi seperti koin (pada sumbu Z agar tampak berputar)
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}