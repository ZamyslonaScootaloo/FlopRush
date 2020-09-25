using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
	[Header("Health")]
    public float health; //życie przeciwnika
    public float maxHealth; //ile przeciwnik moze miec max hp ( można edytować w unity )

	public float dodajzycie; //ile przeciwnik traci czy dostaje zycie. jeżeli chcemy aby tracił ustawiamy wartość na minus ( - )


	public void Start()
	{
		health = maxHealth; // Ustawiamy zycie na samym początku na maxa
	}
	public void Update()
	{
		#region Health
		if (health >= maxHealth) // jeżeli zycie jest wieksze to ustaw na początkową wartośc
		{
			health = maxHealth;
		} else if(health <= 0) // jeżeli życie jest mniejsze to ustaw na 0
		{
			health = 0;
			Destroy(this.gameObject);
		}
		#endregion 
		if (Input.GetKeyDown(KeyCode.H)) // Sprawdzamy czy przeciwnik traci albo dostaje zycie
		{
			HealthModify(dodajzycie);
		}
	}


	public void HealthModify(float value) // Funkcja sprawdzająca czy przeciwnik traci albo dostaje zycie
	{
		health += value;
	}
}
