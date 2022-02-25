using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{

	private LayerMask cat, ground;
	private Rigidbody2D RB;
	// Start is called before the first frame update
	void Start()
	{
		RB = GetComponent<Rigidbody2D>();
		cat = LayerMask.GetMask("Cat");
		ground = LayerMask.GetMask("Ground");
		gameObject.SetActive(false);
	}

	public void Drop()
	{
		RB.gravityScale = 1.5f;
		RB.velocity = new Vector2(0, -5f);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		int mask = 1 << collision.gameObject.layer;
		if((mask & cat.value) != 0)
		{
            CatController.instance.Remake();
			Hit();
		}
		else if((mask & ground.value) != 0)
		{
			Hit();
		}
	}

	private void Hit()
	{
		RB.gravityScale = 0;
		gameObject.SetActive(false);
	}
}
