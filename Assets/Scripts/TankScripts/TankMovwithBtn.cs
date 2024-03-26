using System.Collections.Generic;
using UnityEngine;

namespace TankScripts
{
    public class TankMovwithBtn : MonoBehaviour
    {
        [SerializeField]public float moveSpeed = 5f; // Tankın hareket hızı
        public float rotationSpeed = 350f;
        private bool leftclickControl;
        private bool rightclickControl;

        private bool upBtn, downBtn;
        // Start is called before the first frame update
        void Start()
        {
            upBtn = false;
            downBtn = false;

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
            upBtn = !upBtn;
            downBtn = false;
        }

        public void DownButton()
        {
            downBtn = !downBtn;
            upBtn = false;
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
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }else if (downBtn == true && upBtn==false)
            {
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            }
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
