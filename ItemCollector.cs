using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
	private int  fruits = 0;

	[SerializeField] private Text fruitsText;
	[SerializeField] private ManaBar manaBar;
	[SerializeField] private float maxMana = 100f;



	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Collectable Item"))
		{
			Destroy(collision.gameObject);
			fruits++;
			manaBar.AddMana(maxMana * .2f);
		}
    
	}
  
}

