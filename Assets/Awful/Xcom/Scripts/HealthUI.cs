using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	private Xcom.Health ownHealth;
	private Slider healthBar;
	private Image[] yeh;

	private void Awake()
	{
		Debug.Log("Yeh");
		ownHealth = gameObject.GetComponentInParent<Xcom.Health>();
		healthBar = gameObject.GetComponentInChildren<Slider>();
		yeh = GetComponentsInChildren<Image>();
		healthBar.value = 1f;
	}

	private void OnEnable()
	{
		ownHealth.UpdateUI += UpdateHealth;
		ownHealth.Die += TurnRed;
		
	}

	private void OnDisable()
	{
		ownHealth.UpdateUI -= UpdateHealth;
		ownHealth.Die -= TurnRed;
	}
	//These are the things that update the ugly healthbar when you take damage,
	//works as intended and made it easier in the beginning of the project since
	//I didn't need to add specific references to the UI script, so I could playtest
	//without having to set up every single gameobject with the right scripts and children.
	private void UpdateHealth()
	{
		healthBar.value = ownHealth.HitPoints / 10f;
	}

	private void TurnRed()
	{
		foreach (var image in yeh)
		{
			image.color = Color.red;
		}
	}
}
