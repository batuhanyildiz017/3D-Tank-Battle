using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankScripts
{
    public class TankMovwithBtn : MonoBehaviour
    {
        [SerializeField]public float moveSpeed = 5f; // Tankın hareket hızı
        public float rotationSpeed = 350f; // Tankın rotasyon hızı
        private bool leftclickControl;  //Sol Butona basılmasını kontrol etme
        private bool rightclickControl; //Sağ Butona basılmasını kontrol etme
        
        [SerializeField] private float initialMoveSpeed = 1f; // Tankın başlangıç hızı
        [SerializeField] private float maxMoveSpeed = 5f; // Tankın maksimum hızı
        [SerializeField] private float accelerationRate = 0.1f; // Hızlanma oranı
        private float currentMoveSpeed;
        
        private IEnumerator accelerateCoroutine;

        private bool upBtn, downBtn;
        // Start is called before the first frame update
        void Start()
        {
            upBtn = false;
            downBtn = false;
            currentMoveSpeed = initialMoveSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            MoveTank(); // ileri geri tank hareketi
            LeftRotate(); //sola döndürme
            RightRotate(); //sağa döndürme
        
        }

        public void UpButton()
        {
            currentMoveSpeed = initialMoveSpeed;
            upBtn = !upBtn;
            downBtn = false;
            if (accelerateCoroutine == null)
            {
                accelerateCoroutine = Accelerate();
                StartCoroutine(accelerateCoroutine);
            }
        }

        public void DownButton()
        {
            currentMoveSpeed = initialMoveSpeed;
            downBtn = !downBtn;
            upBtn = false;
            if (accelerateCoroutine == null)
            {
                accelerateCoroutine = Accelerate();
                StartCoroutine(accelerateCoroutine);
            }
        }
        public void MakeTrueLeft()  //sol butona bas�ld���n� kontrol etme
        {
            leftclickControl = true;
        }
        public void MakeFalseLeft()  //sol butona bas�lmay� b�rakt���n� kontrol etme
        {
            leftclickControl=false;
        }
        public void MakeTrueRight() //sa� butona bas�ld���n� kontrol etme
        {
            rightclickControl = true;
        }
        public void MakeFalseRight() //sa� butona bas�lmay� b�rak�ld���n� kontrol etme
        {
            rightclickControl = false;
        }

        void MoveTank()
        {
            if (upBtn==true && downBtn==false)
            {
                transform.Translate(Vector3.forward * currentMoveSpeed * Time.deltaTime);
            }else if (downBtn == true && upBtn==false)
            {
                transform.Translate(Vector3.back * currentMoveSpeed * Time.deltaTime);
            }
        }
        IEnumerator Accelerate()
        {
            while (currentMoveSpeed < maxMoveSpeed)
            {
                currentMoveSpeed += accelerationRate;
                yield return null;
            }
            accelerateCoroutine = null;
        }
        void LeftRotate()
        {
            if (leftclickControl==true && (upBtn==true || downBtn==true) )
            {
                transform.Rotate(new Vector3(0f, -rotationSpeed*Time.deltaTime, 0f));

            }
        }
        void RightRotate()
        {
            if(rightclickControl==true && (upBtn==true || downBtn==true))
                transform.Rotate(new Vector3(0f, rotationSpeed*Time.deltaTime, 0f));
        }
        
    }
}
