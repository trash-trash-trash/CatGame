	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	//[RequireComponent(typeof(BoxCollider2D))]

	public class OBJ_CRTV: MonoBehaviour
	{
		
		//trashcan that may hold other items
		
		CharacterController2D controller;
		
		public Animator animator;
		
		Rigidbody2D rb;
		
		float pushdistance;
		
		float updistance;
		
		string myName="CRTV";
		
		int maxHP=1;
		
		int HP;
		
		float myWeight=10f;
		
		int BoopTracker=0;
				
		bool isAlive=true;
		
		bool moving=false;
		
		private void Reset()
		{
				Init();
		}
		
		void Init()
		{
			//make box collider trigger
			GetComponent<BoxCollider2D>().isTrigger = true;
			
		}
		

			
		// Start is called before the first frame update
		void Awake()
		{
				
			this.GetComponent<OBJECT_stats>().SetupStats(myName, maxHP, myWeight, BoopTracker);
			
			controller  = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
				
			rb = GetComponent<Rigidbody2D>();
			HP = maxHP;
			
			Debug.Log ("My name is "+myName);
			
		}
		
		public void Booped()
		{
			BoopTracker ++;
			Debug.Log ("TV booped! Changing channel!");
			
			//
			//picks a float at random to send to the animator to play a random idle animation
			float idleFloat = (Random.Range(0.1f, 5.9f));
			
			animator.SetFloat("IdleFloat", idleFloat);
			
			Debug.Log (idleFloat);
			
			
		}


			

			
			
	}
		
