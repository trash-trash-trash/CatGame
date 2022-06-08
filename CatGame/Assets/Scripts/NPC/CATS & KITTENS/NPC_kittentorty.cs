	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

public class NPC_kittentorty: MonoBehaviour
{
	//this is the universal NPC_Kitten script

	//declares Player's prescence
	CharacterController2D controller;
	Animator animator;
	Rigidbody2D rb;

	//ints that track how many times the Player has Booped the NPC
	//increasing this value may change the NPC's behaviour
	int BoopTracker = 0;

	//determines how far the NPC travels when the NPC gets Booped or Attack
	float pushdistance;
	float updistance;

	//declares NPCBase to grab the NPC's stats
	NPCBase _Base;
			
	//NPC's name
	string myName;
		
	//NPC's Maximum HP value and an int to track its current HP
	int maxHP;
	int currentHP;
		
	//
	float moveSpeed = 2f;
		
	int decision = 0;
		
	//NPC can only act if it is Alive
	bool isAlive=true;

	//for determining which way the NPC is currently facing
	private float movement;
	private bool facingRight; 
		
	//explosion sprite prefab spawned on death at this object's transform
	Transform explosionPoint;
	public GameObject explosionPrefab;
		
	void Awake()
	{

		myName = _Base.Name;

		maxHP = _Base.MaxHP;
		currentHP = maxHP;

		moveSpeed = _Base.MoveSpeed;

			
		controller  = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
				
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		//GetComponent<BoxCollider2D>().isTrigger = true;
			
		//starts the CoRoutine deciding what to do next 
		StartCoroutine(Decision());
									
	}
		
	private void Update()
	{
			
		if(!isAlive)
		Die();
			
        else if (movement > 0f)
        {
            rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
        }
        else if (movement < 0f) 
        {
            rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
		}
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
		
		//logic for flipping NPC Sprite depending on its movement
			if (movement > 0 && !facingRight)
				Flip();
			else if (movement < 0 && facingRight)
				Flip();						
			
		}
		
		private void Flip()
		{
			//flips the facingRight bool on/off
			facingRight = !facingRight;

			// Multiply the NPC's x local scale by -1.
			Vector3 localScale = transform.localScale;
			localScale.x *= -1;
			transform.localScale = localScale;
		}

				
		IEnumerator Decision()
		{
			
			float decisionFloat = (Random.Range(3f, 7f));

			const float waitTime = 3;
			float counter = 0f;

			Debug.Log("Hello Before Waiting");
			while (decisionFloat < waitTime)
			{
				Debug.Log("Current WaitTime: " + counter);
				counter += Time.deltaTime;
				yield return null; //Don't freeze Unity
			}
			Debug.Log("Hello After waiting for 3 seconds");

		movement = 0f;
		animator.SetFloat("Speed", 0);
		animator.SetFloat("Idle", 0f);
		animator.SetBool("IsBooping", false);
				
		Debug.Log ("Hmm...");
			
		decision = Random.Range(1,15);
		
		switch (decision)
		{
					
			case int decision when (decision < 5):
			Debug.Log("Walking!");
			animator.SetFloat("Speed", 2);
			movement = (Random.Range(-3f,3f));
			animator.SetFloat("Idle", 0f);
					
			break;
					
			case int decision when (decision == 6):
			Debug.Log("Running!!");
			animator.SetFloat("Speed", 3);
					animator.SetFloat("Idle", 0f);
					movement = (Random.Range(-3f,3f));				
					break;
					
					case int decision when (decision== 7):
					Debug.Log("Idle 01!");
					animator.SetFloat("Idle", 0.2f);
					movement = 0;
					break;
					
					case int decision when (decision == 8):
					Debug.Log("Idle 02!");
					animator.SetFloat("Speed", 0);
					animator.SetFloat("Idle", 1.2f);
					movement = 0;
					break;
					
					case int decision when (decision == 9):
					Debug.Log("Idle 03!");
					animator.SetFloat("Speed", 0);
					animator.SetFloat("Idle", 2.2f);
					movement = 0;
					break;
					
					case int decision when (decision == 10):
					Debug.Log("Idle 04!");
					animator.SetFloat("Speed", 0);
					animator.SetFloat("Idle", 3.2f);
					movement = 0;
					break;
					
					case int decision when (decision < 10):
					Debug.Log("Boops!");
					movement = 0;
					animator.SetBool("IsBooping", true);
					//StartCoroutine(Boop());
					break;
			
				}
			
							
			//restarts decision coroutine
			StartCoroutine(Decision());
			
		} 
		
	void Die()
		{
			Debug.Log (myName+" died!");
						
			//spawns explosion sprite
			explosionPoint = this.gameObject.transform;
			Instantiate(explosionPrefab, explosionPoint.position, Quaternion.identity);
		}
		
	}
