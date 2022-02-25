using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Patrol : MonoBehaviour
{
	public Transform endPoint1, endPoint2;
	public float walkTime, waitTime;


	private Animator AM;
	private SpriteRenderer SR;
	private LayerMask cat;
	private WaitForSeconds waitIdle, waitWalk;
	// Start is called before the first frame update
	void Start()
	{
		AM = GetComponent<Animator>();
		SR = GetComponent<SpriteRenderer>();
		cat = LayerMask.GetMask("Cat");
		waitIdle = new WaitForSeconds(waitTime);
		waitWalk = new WaitForSeconds(walkTime);
		StartCoroutine(MainCoroutine());
	}

	private Tween nowTween;
	IEnumerator MainCoroutine()
	{
		while(true)
		{
			
			AM.Play("Idle");
			yield return waitIdle;
			transform.localScale = new Vector3(-1, 1, 1);
			nowTween = transform.DOMove(endPoint2.position, walkTime).SetEase(Ease.Linear);
			AM.Play("Walk");
			yield return waitWalk;

			
			AM.Play("Idle");
			yield return waitIdle;
			transform.localScale = new Vector3(1, 1, 1);
			nowTween = transform.DOMove(endPoint1.position, walkTime).SetEase(Ease.Linear);
			AM.Play("Walk");
			yield return waitWalk;
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		int mask = 1 << collision.gameObject.layer;
		if ((mask & cat.value) != 0)
		{
			BlackCat cat = collision.gameObject.GetComponent<BlackCat>();
			if(cat.haveWeapon)
            {
				cat.BeginAttack(this.transform);
				StopAllCoroutines();
				nowTween?.Kill();
				AM.Play("Die");
				GetComponent<Collider2D>().enabled = false;
				StartCoroutine(DieCotoutine(cat.transform.position.x));
            }
			else
            {
				CatController.instance.Remake();
            }
		}
	}

	IEnumerator DieCotoutine(float catX)
    {
		int dir = catX - transform.position.x > 0 ? -1 : 1;
		yield return new WaitForSeconds(0.3f);
		transform.DOJump(transform.position - dir * new Vector3(-1, 0, 0), 0.5f, 1, 0.6f);
		yield return new WaitForSeconds(1.3f);
		float a = 1;
		while(a > 0)
        {
			a -= Time.deltaTime * 3;
			Color col = SR.color;
			col.a = a;
			SR.color = col;
			yield return null;
        }
		Destroy(gameObject);
	}
}
