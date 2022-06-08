	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class NPC_kittenblack: MonoBehaviour
	{
		
		CharacterController2D controller;
		
		Animator animator;
				
		Rigidbody2D rb;
		
		float pushdistance;
		
		float updistance;
		
		string myName="Black Kitten";
		
		int maxHP=3;
		
		int HP;
		
		int BoopTracker=0;
		
		float speed = 2f;
		
		int decision = 0;
		
		bool isAlive=true;
				
		private float movement;
		
		private bool m_FacingRight;  // For determining which way the NPC is currently facing.
		
			
		//explosion sprite prefab spawned on death at this object's transform
		Transform explosionPoint;
		public GameObject explosionPrefab;
		
		void Awake()
		{
				
			this.GetComponent<NPC_stats>().SetupStats(myName, maxHP, BoopTracker, speed);
			
			controller  = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
				
			rb = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
			//GetComponent<BoxCollider2D>().isTrigger = true;
			
			StartCoroutine(Decision());
									
		}
		
		private void Update()
		{
			
			Debug.Log (movement);
			
			if(!isAlive)
			{
				Die();
			}
			
        if (movement > 0f)
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
        else if (movement < 0f) 
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
		}
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
		
		// If the input is moving the player right and the player is facing left...
			if (movement > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (movement < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
							
			
		}
		
		private void Flip()
		{
				
			// Switch the way the NPC is labelled as facing.
			m_FacingRight = !m_FacingRight;

			// Multiply the NPC's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

				
		IEnumerator Decision()
		{
			
			float decisionFloat = (Random.Range(3f, 7f));
			
			yield return new WaitForSecondsRealtime(decisionFloat);
			
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
