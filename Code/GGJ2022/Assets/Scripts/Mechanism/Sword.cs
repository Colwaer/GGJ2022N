using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sword : MonoBehaviour
{
	public Transform catBackPos;//在黑猫下加一子物体，位置在黑猫的身后
	public float floatingTime;

	private WaitForSeconds waitSec;
	private Vector3 topPoint, bottomPoint, posDiff;
	private Vector3 frontPoint;
	private LayerMask cat;
	private bool obtained = false;
	private bool attacking = false;
	// Start is called before the first frame update
	void Start()
	{
		cat = LayerMask.GetMask("Cat");

		topPoint = new Vector3(0, 0.6f, 0);
		bottomPoint = new Vector3(0, -0.2f, 0);
		frontPoint = new Vector3(1f, 0.3f, 0);
		posDiff = Vector3.zero;
		waitSec = new WaitForSeconds(floatingTime);
		StartCoroutine(MainCoroutine());
	}

	// Update is called once per frame
	void Update()
	{
		if (obtained)
		{
			if(!attacking)
            {
				transform.position = Vector3.Lerp(transform.position, catBackPos.position + posDiff, 5f * Time.deltaTime);
            }
			else
            {
				//transform.position = Vector3.Lerp(transform.position, catBackPos.position + frontPoint, 3f * Time.deltaTime);
			}
		}
	}

	IEnumerator MainCoroutine()
	{
		while (true)
		{
			DOTween.To(() => posDiff, x => posDiff = x, topPoint, floatingTime).SetEase(Ease.InOutSine);
			yield return waitSec;
			DOTween.To(() => posDiff, x => posDiff = x, bottomPoint, floatingTime).SetEase(Ease.InOutSine);
			yield return waitSec;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		int mask = 1 << collision.gameObject.layer;
		if ((mask & cat.value) != 0)
		{
			obtained = true;
			GetComponent<Collider2D>().enabled = false;
			collision.gameObject.GetComponent<BlackCat>().GiveWeapon(this);
			transform.DORotate(new Vector3(0, 0, -45f), 0.5f, RotateMode.LocalAxisAdd);
		}
	}

	public void Attack(Transform enemyTrans)
    {
		StartCoroutine(AttackCoroutine(enemyTrans));
	}

	IEnumerator AttackCoroutine(Transform enemyTrans)
    {
		yield return new WaitForSeconds(0.2f);
		attacking = true;
		if(catBackPos.lossyScale.x > 0)
        {
			transform.DORotate(new Vector3(0, 0, 2160f), 1.8f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
			transform.DOMoveX(catBackPos.position.x + frontPoint.x, 1.3f).SetEase(Ease.OutBack);
			transform.DOMoveY(catBackPos.position.y + frontPoint.y, 1.3f).SetEase(Ease.InSine);
			/*yield return new WaitForSeconds(1.65f);
			Vector3 dis = enemyTrans.position + new Vector3(0, 1, 0) - this.transform.position;
			transform.DOMove(this.transform.position + dis * 4f, 0.15f).SetEase(Ease.OutSine);*/
        }
		else
        {
			transform.DORotate(new Vector3(0, 0, -2160f), 1.8f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
			transform.DOMoveX(catBackPos.position.x - frontPoint.x, 1.3f).SetEase(Ease.OutBack);
			transform.DOMoveY(catBackPos.position.y + frontPoint.y, 1.3f).SetEase(Ease.InSine);
		}
		
		attacking = false;
	}
}
