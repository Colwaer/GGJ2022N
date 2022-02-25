using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Key : MonoBehaviour
{
	public bool isObtained = false;

	private LayerMask cat;
	// Start is called before the first frame update
	void Start()
	{
		cat = LayerMask.GetMask("Cat");
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		int mask = 1 << collision.gameObject.layer;
		if ((mask & cat.value) != 0)
		{
			isObtained = true;
			Debug.Log(collision.gameObject);
			transform.DOMove(collision.gameObject.transform.position, 0.6f).SetEase(Ease.OutQuad);
			transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.OutQuad);
		}
	}
}
