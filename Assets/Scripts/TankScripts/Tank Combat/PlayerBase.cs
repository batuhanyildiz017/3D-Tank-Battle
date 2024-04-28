using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

	public static PlayerBase instance { get; private set; }

	//[SerializeField] private TMP_Text deathCountText;
	//[SerializeField] private TMP_Text LevelCountText;

	public int deathCount = 0;
	public int playerLevel = 0;
	public int playerBudget = 0;

	public bool PlayerLive = true;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
	}

	private void Update()
	{
	//	deathCountText.text = "" + deathCount;
	//	LevelCountText.text = "" + playerLevel;

	}
	
	//LoadData() 
	//SaveData()
	
	//PlayerDeath 
	
	//Diğer tank fonksiyonları ney ise bunlar eklenebilir 
	
}
