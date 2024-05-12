using System.Collections;
using System.Collections.Generic;
using Scriptableobject;
using UnityEngine;

namespace TankScripts
{
    public class TankMovwithBtn : MonoBehaviour
    {
        [SerializeField] private TankSO _tankSo;
        
        private bool leftclickControl;  //Sol Butona basılmasını kontrol etme
        private bool rightclickControl; //Sağ Butona basılmasını kontrol etme
        
        private float currentMoveSpeed;
        
        private IEnumerator accelerateCoroutine;
        private IEnumerator decelerateCoroutine;

        private bool upBtn, downBtn,upBtnDecelerate;

        void Start()
        {
            upBtn = false;
            upBtnDecelerate = true;
            downBtn = false;
            currentMoveSpeed = _tankSo.TankFirstSpeed;
        }
        void Update()
        {
            MoveTank(); // ileri geri tank hareketi
            LeftRotate(); //sola döndürme
            RightRotate(); //sağa döndürme
        
        }

        public void UpButton()
        {
            currentMoveSpeed = _tankSo.TankFirstSpeed;
            upBtn = !upBtn;
            downBtn = false;
            if (upBtn && accelerateCoroutine == null)
            {
                
                accelerateCoroutine = Accelerate();
                StartCoroutine(accelerateCoroutine);
            }
        }

        public void DownButton()
        {
            currentMoveSpeed = _tankSo.TankFirstSpeed;
            downBtn = !downBtn;
            upBtn = false;
            if (downBtn && accelerateCoroutine == null)
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
            if(upBtn && !downBtn || downBtn && !upBtn){
                while (currentMoveSpeed < _tankSo.TankMaxSpeed)
                {
                    currentMoveSpeed += _tankSo.TankAccelerationRate;
                    yield return new WaitForSeconds(0.2f);
                }
                accelerateCoroutine = null;
            
            }
                
                
        }
        IEnumerator Decelerate()
        {
            while (currentMoveSpeed > _tankSo.TankMinSpeed)
            {
                currentMoveSpeed -= (_tankSo.TankAccelerationRate);
                yield return new WaitForSeconds(0.2f);
            }
            decelerateCoroutine = null;
        }
        void LeftRotate()
        {
            if (leftclickControl==true && (upBtn==true || downBtn==true) )
            {
                transform.Rotate(new Vector3(0f, -_tankSo.RotateSpeed*Time.deltaTime, 0f));

            }
        }
        void RightRotate()
        {
            if(rightclickControl==true && (upBtn==true || downBtn==true))
                transform.Rotate(new Vector3(0f, _tankSo.RotateSpeed*Time.deltaTime, 0f));
        }
    }
}
