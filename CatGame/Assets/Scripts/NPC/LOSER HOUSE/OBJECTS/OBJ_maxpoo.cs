	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	//[RequireComponent(typeof(BoxCollider2D))]

	public class OBJ_maxpoo: MonoBehaviour
	{
		
		
		CharacterController2D controller;
		
		public Animator animator;
				
		Rigidbody2D rb;
		
		float pushdistance;
		
		float updistance;
		
		string myName="Maximum Poo";
		
		int maxHP=1;
		
		int HP;
		
		float myWeight=1f;
		
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


		
	}
