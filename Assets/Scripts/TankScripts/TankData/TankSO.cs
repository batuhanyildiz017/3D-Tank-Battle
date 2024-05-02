using UnityEngine;

namespace Scriptableobject
{
	[CreateAssetMenu(fileName = "NewTankData", menuName = "Data/TankData")]
	public class TankSO : ScriptableObject
	{
	
		[Header("Movemnt")]
		public float TankFirstSpeed = 10f; // Tankın başlangıç hızı
		public float TankMaxSpeed = 10f;  // Tankın en fazla gidebileceği hız
		public float TankMinSpeed = 0f;  // Tankın min gidebileceği hız
		public float TankAccelerationRate = 0.1f; // tankın zamanla hızlanma hızı 
		public float HeadRotateSpeed = 10f; // Tankın üst gövdesinin dönme hızı 

		[Header("Combat")]
		public float Force = 10f; // merminin atış hızı 

	}

}