
using Scriptableobject;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Combat
{
	public class Damagable : MonoBehaviour
	{
	    public static Damagable instance { get; private set; }

			[SerializeField] private TankSO _coreData;
			
			[field: SerializeField] public float currentHealth;

	/*		[Header("Slider")]
			[SerializeField] private FloatingSliderBar _healSliderBar;
			[SerializeField] private FloatingSliderBar _ShildSliderBar;
	*/
			public float Health
			{
				get { return currentHealth; }
				set
				{
					currentHealth = value;
					
				}
			}

			public UnityEvent OnDead;
			public UnityEvent OnHit, OnHeal;

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
			
			private void Start()
			{
				Health = _coreData.MaxHealth;

			}

			private void Update()
			{
				if (Input.GetKeyDown(KeyCode.P))
				{
					takeDamage(50);
				}
				if (Input.GetKeyDown(KeyCode.O))
				{
					takeHeal(10);
				}
			}

			internal void takeDamage(float damage)
			{
				
				Health -= damage;
				//_healSliderBar.UpdateSliderBar(Health, _coreData.MaxHealth);
				if (Health <= 0)
				{ 
					OnDead?.Invoke();

				}
				else
				{
					OnHit?.Invoke();
				}
				
			}
			
			public void takeHeal(int healthBoost)
			{
				Health += healthBoost;
				Health = Mathf.Clamp(Health, 0, _coreData.MaxHealth);
				//_healSliderBar.UpdateSliderBar(Health, _coreData.MaxHealth);
				OnHeal?.Invoke();
			}

			public void TankDeath()
			{
				Destroy(gameObject);
			}
	}
}
