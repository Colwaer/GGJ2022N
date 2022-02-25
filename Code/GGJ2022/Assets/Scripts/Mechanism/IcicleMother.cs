using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleMother : MonoBehaviour
{
	public float interval;
	public float initialDelay;
	public Icicle icicle;

	private WaitForSeconds waitSec;
	// Start is called before the first frame update
	void Start()
	{
		waitSec = new WaitForSeconds(interval);
		StartCoroutine(MainCoroutine());
	}

	IEnumerator MainCoroutine()
	{
		yield return new WaitForSeconds(initialDelay);
		while (true)
		{
			yield return waitSec;
			icicle.gameObject.SetActive(true);
			icicle.transform.position = this.transform.position;
			icicle.Drop();
		}
	}
}
