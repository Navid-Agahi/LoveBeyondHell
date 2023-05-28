using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
	PlayerLife playerLife;

	private void Awake ()
	{
	playerLife= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			playerLife.UpdateCheckPoint(transform.position);
		}
	}


}



