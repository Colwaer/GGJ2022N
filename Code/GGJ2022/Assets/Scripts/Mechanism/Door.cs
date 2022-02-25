using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public Key[] keyList;
	public Collider2D doorRange;

	private AudioSource audioSource;
	private LayerMask cat;
	private bool couldBeOpened = false;
	private bool playerInRange = false;
	private bool haveAnyKey = false;
	// Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = false;
		cat = LayerMask.GetMask("Cat");
	}

	private void Update()
	{
		if(playerInRange)
		{
			if (couldBeOpened)
			{
				TipMessage.instance.SetWord("按E键开门喵！");
				if (Input.GetKeyDown(KeyCode.E))
				{
					audioSource.Play();
					Destroy(gameObject, 0.33f);
				}
			}
		}
	}

	private void FixedUpdate()
	{
		if(doorRange.IsTouchingLayers(cat))
		{
			if(!playerInRange)//上一帧玩家不在范围内，说明玩家是刚进来的
			{
				if(!couldBeOpened)
				{
					couldBeOpened = true;
					foreach (Key key in keyList)
					{
						couldBeOpened &= key.isObtained;
						haveAnyKey |= key.isObtained;
					}
				}
			}
			playerInRange = true;
		}
		else
		{
			playerInRange = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		int mask = 1 << collision.gameObject.layer;
		if ((mask & cat.value) != 0)
        {
			if (!couldBeOpened)
			{
				if(haveAnyKey)
				{
					TipMessage.instance.SetWord("钥匙怎么还不够喵...");
				}
				else
				{
					TipMessage.instance.SetWord("开门居然需要钥匙喵...");
				}
			}
        }
	}
}
