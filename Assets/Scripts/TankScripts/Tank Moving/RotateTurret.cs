using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public TankScripts.Inputs inputs; // Inputs sınıfına erişmek için referans
    // Start is called before the first frame update

    void Update()
    {
        RotatationTurret();
    }

    void RotatationTurret()
    {
        Vector2 inputDirection = inputs.joystickVec; // Joystick giriş değerini al

        // Joystick vektörünü dünya koordinatlarına göre dönüştür
        Vector3 moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); // Hedef rotasyonu belirle
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Dönme işlemini uygula
        }
    }
}
