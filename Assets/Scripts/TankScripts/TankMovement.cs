using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Tankın hareket hızı
    public float rotationSpeed = 100f;
    public TankScripts.Inputs inputs; // Inputs sınıfına erişmek için referans
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDirection = inputs.joystickVec; // Joystick giriş değerini al

        // Joystick vektörünü dünya koordinatlarına göre dönüştür
        Vector3 moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;

        // Hareketi uygula
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        // Tankın dönme işlemini gerçekleştir
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); // Hedef rotasyonu belirle
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Dönme işlemini uygula
        }
    }
    
}
