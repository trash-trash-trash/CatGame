using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class NPC_kitten_script : MonoBehaviour
{
	
	CharacterController2D controller;
    
	public Animator animator;
	
	Rigidbody2D rb;
	
	float pushdistance;
	
	float updistance;
	
	string myName="Kitten";
	
	int maxHP=3;
	
	int HP;
	
	int BoopTracker=0;
	
	int decision = 0;
	
	int decision2 = 0;
	
	bool isAlive=true;
	
	bool moving=false;
	
	//movespeed
	float speed=6;
	
	// Start is called before the first frame update
    void Awake()
    {
			
		this.GetComponent<NPC_stats>().SetupStats(myName, maxHP, BoopTracker, speed);
		
		controller  = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
			
        rb = GetComponent<Rigidbody2D>();
		HP = maxHP;
		
		StartCoroutine(Decision());
		
		//kitten evolves into cats? become controllable
		//StartCoroutine(EvolveDecision());
		
		Physics2D.IgnoreLayerCollision(10,11);
		
    }

	private void Update()
	{
		
		
				
	}
	
	
			
	IEnumerator Decision()
	{
		
		

		
		
			
		Debug.Log ("Hmm...");
		
		decision = Random.Range(0,6);
	
			switch (decision)
			{
				case int decision when (decision == 1):
				Debug.Log(decision+": Walking!");
				animator.SetFloat("Speed", 1);
				moving=true;
				break;
				
				case int decision when (decision == 2 ):
				
				Debug.Log(decision+": Standing!");
				animator.SetFloat("Speed", 0);
				animator.SetFloat("IdleFloat", 0);
				moving=false;
				break;
				
				case int decision when (decision == 3 ):
				
				Debug.Log(decision+": Standing idle 01!");
				animator.SetFloat("Speed", 0);
				animator.SetFloat("IdleFloat", 1.2f);
				moving=false;
				break;
				
				case int decision when (decision == 4 ):
				
				Debug.Log(decision+": Standing idle 02!");
				animator.SetFloat("Speed", 0);
				animator.SetFloat("IdleFloat", 2.2f);
				moving=false;
				break;
				
				case int decision when (decision == 5 ):
				
				Debug.Log(decision+": Standing idle 03!");
				animator.SetFloat("Speed", 0);
				animator.SetFloat("IdleFloat", 3.2f);
				moving=false;
				break;
				
			
			
		
			}

		
		yield return new WaitForSecondsRealtime(3f);
		
		animator.SetFloat("Speed", 0);
		animator.SetFloat("IdleFloat", 0);
		decision=0;
		
		
		//restarts decision coroutine
		StartCoroutine(Decision());
		
	} 
	
		
	
	
	
	

}
