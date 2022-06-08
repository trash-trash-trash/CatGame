using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJ_loserhouse_explosion : MonoBehaviour
{
	
	private Animator animator;

    void Awake()
    {
		animator = GetComponent<Animator>();
		animator.SetTrigger("Destroyed");
		StartCoroutine(Die());
    }
	
	private IEnumerator Die()
	{
		yield return new WaitForSecondsRealtime(0.65f);
		Destroy(this.gameObject);
	}

   
}
