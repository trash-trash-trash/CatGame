using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJ_chickenEgg : MonoBehaviour
{	
	CharacterController2D controller;
	
	string myName="Chicken Egg";
	
	int maxHP=1;
	
	int HP;
	
	float myWeight=0.5f;
	
	int BoopTracker;
	
	public Animator animator;
	
	public GameObject chick;
	
	Transform SpawnPoint;
	
	Rigidbody2D rb;
	
	float pushdistance;
	
	float updistance;
	
	private int decision = 0;
	
	private bool isAlive=true;
	
	private void Awake()
	{
			rb = GetComponent<Rigidbody2D>();
			
			controller  = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
			
			isAlive=true;
			
			StartCoroutine(BirthDecision());
			
			this.GetComponent<OBJECT_stats>().SetupStats(myName, maxHP, myWeight, BoopTracker);
			
			
			SpawnPoint = this.gameObject.transform;
			
			Debug.Log ("My name is "+myName);
	
	}
	
	
	
	IEnumerator BirthDecision()
	{
		yield return new WaitForSecondsRealtime (.7f);
		
		if (isAlive)
		{
		
			decision += Random.Range(0,2);
			
			if(decision==1)
			{
				Birth();			
			}
				
			else
			{
				Debug.Log ("But nothing happened!");
			}
		}
		
		StartCoroutine (BirthDecision());
		
	}
	
	public void Booped(float angularChangeInDegrees)
	{
	        float impulse = (angularChangeInDegrees * Mathf.Deg2Rad) * rb.inertia;

			rb.AddTorque(impulse, ForceMode2D.Impulse);
	}
	
	private void Birth()
	{
		Debug.Log ("A baby chick was born!");
		Instantiate(chick, SpawnPoint.position, Quaternion.identity);
		StartCoroutine(Die());
	}

	IEnumerator Die()
	{	
	
		animator.SetTrigger("Destroyed");
				
		//egg has a random chance to spawn a chick 
		decision += Random.Range (0,2);
		
		if(decision==1)
		{
			Instantiate(chick, SpawnPoint.position, Quaternion.identity);
				
			Debug.Log ("A baby chick was born!");
		}
		
		isAlive = false;
		
		Debug.Log (myName+" broke!");
		
		yield return new WaitForSecondsRealtime(1.2f);
		
		Destroy(this.gameObject);
	}





}
