using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_stats: MonoBehaviour
{
	
	string myName;
	
	int maxHP;
	
	int HP;
	
	int BoopTracker;
	
	float speed;
	
	bool isAlive;	
	
	CharacterController2D controller;
    
	private Animator animator;
	
	private Rigidbody2D rb;
	
	float pushdistance;
	
	bool moving;
	
	public string GetName()
	{
		return myName;
	}
    
	public int GetMaxHealth()
	{
		return maxHP;
	}
	
	public int GetHealth()
	{
		return HP;
	}
	
	public int GetBoop()
	{
		return BoopTracker;
	}
	
	public float GetSpeed()
	{
		return speed;
	}
	
	
	public void SetupStats(string newMyName, int newMaxHP, int newBoopTracker, float newSpeed)
	{
		myName = newMyName;
		maxHP = newMaxHP;
		HP = maxHP;
		speed = newSpeed;
		
		BoopTracker = newBoopTracker;
		
		
		
	}
	
	void Start()
	{
		isAlive = true;
		
		rb = GetComponent<Rigidbody2D>();
		
		animator = GetComponent<Animator>();
		animator.SetBool("IsAlive", true);

		
		controller  = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
		
		Physics2D.IgnoreLayerCollision(10,10);
		Physics2D.IgnoreLayerCollision(10,11);
		Physics2D.IgnoreLayerCollision(10,12);
		
		Debug.Log ("Hello! My name is "+myName);
	}
	
	//
	//changes health when taking dmg from swipe
	public void TakeDamage(int damage)
	{
		moving=false;
		if(damage <= 0)
		{
			BoopTracker ++;
			
			if(controller.m_FacingRight)
			{
				pushdistance=2.2f;
				//
				//pushes chicken away
				rb.AddForce(new Vector2(pushdistance, 2.2f), ForceMode2D.Impulse);
				animator.SetTrigger("Booped");
			}
			else
			{
				pushdistance=-2f;
				//pushes chicken away
				rb.AddForce(new Vector2(pushdistance, 2.2f), ForceMode2D.Impulse);
				animator.SetTrigger("Booped");
			}
			
		}
		
		else if (controller.m_FacingRight)
			{		
				HP -= damage;
				animator.SetTrigger("Damaged");
				pushdistance=1f;
				rb.AddForce(new Vector2(pushdistance, 4f), ForceMode2D.Impulse);
			}
			else
			{
				HP -= damage;
				animator.SetTrigger("Damaged");
				pushdistance=-1f;
				rb.AddForce(new Vector2(pushdistance, 4f), ForceMode2D.Impulse);
			}
				
				if(HP <= 0)
				{
					StartCoroutine(Die());
				}
			
	}
	
	
	IEnumerator Die()
	{
		
		Debug.Log (myName+" died!");
		animator.SetBool("IsAlive", false);
		isAlive=false;
		
		yield return new WaitForSeconds(0.7f);
		
		
		
		Destroy(this.gameObject);
	}

	
	
}
