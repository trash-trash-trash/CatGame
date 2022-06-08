using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJECT_stats: MonoBehaviour
{
	
	string myName;
	
	int maxHP;
	
	int HP;
	
	//how much you can fling an object around by booping or attacking is determined by weight	
	float myWeight;
	
	float torque = 5f;
	
	int BoopTracker;
	
	bool isAlive;	
	
	
	//explosion sprite prefab spawned on death at this object's transform
	Transform explosionPoint;
	public GameObject explosionPrefab;
	
	CharacterController2D controller;
    
	public Animator animator;
	
	Rigidbody2D rb;
	
	float pushdistance;
	
	public string GetMyName()
	{
		return myName;
	}
    
	public int GetMaxHealth()
	{
		return maxHP;
	}
	
	public float GetMyWeight()
	{
		return myWeight;
	}
	
	public int GetMyHealth()
	{
		return HP;
	}
	
	public int GetMyBoop()
	{
		return BoopTracker;
	}
	
	
	public void SetupStats(string newMyName, int newMaxHP, float newMyWeight, int newBoopTracker)
	{
		myName = newMyName;
		maxHP = newMaxHP;
		HP = maxHP;
		myWeight = newMyWeight;
		
		BoopTracker = newBoopTracker;
		
		
		isAlive = true;
		
	}
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		
		controller  = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
		
		Physics2D.IgnoreLayerCollision(11,12);
	}
	
	//
	//testing 
	void Update()
		{
			if(Input.GetKeyDown("7"))
			{
				TakeDamage(0);
			}
			if(Input.GetKeyDown("8"))
			{
				TakeDamage(1);
			}
			
		}

	
	//
	//changes health when taking dmg from swipe
	public void TakeDamage(int damage)
	{
		
		
		if(damage <= 0)
		{
			BoopTracker ++;
			
			switch (name)
			{
				case string name when (myName == "Gamecontroller01"):
				this.GetComponent<OBJ_gamecontroller01>().BoopChange();
				Debug.Log (myName+" was booped!");
				break;
				
				case string name when (myName == "CRTV"):
				this.GetComponent<OBJ_CRTV>().Booped();
				break;
				
				case string name when (myName == "Chicken Egg"):
				this.GetComponent<OBJ_chickenEgg>().Booped(40f);
				Debug.Log (myName+" was booped!");
				break;
			}
			
			if(controller.m_FacingRight)
			{
				pushdistance=2.2f*myWeight;
				//
				//pushes chicken away
				rb.AddForce(new Vector2(pushdistance, 3.2f), ForceMode2D.Impulse);
				animator.SetTrigger("Booped");
			}
			else
			{
				pushdistance=-2f*myWeight;
				//pushes chicken away
				rb.AddForce(new Vector2(pushdistance, 3.2f), ForceMode2D.Impulse);
				animator.SetTrigger("Booped");
			}
			
		}
		
		else if (controller.m_FacingRight)
			{		
				HP -= damage;
				animator.SetTrigger("Damaged");
				pushdistance=1f*myWeight;
				rb.AddForce(new Vector2(pushdistance, 4f), ForceMode2D.Impulse);
			}
			else
			{
				HP -= damage;
				animator.SetTrigger("Damaged");
				pushdistance=-1f*myWeight;
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
		animator.SetTrigger("Destroyed");
		
		

		
		//spawns explosion sprite
		explosionPoint = this.gameObject.transform;
		Instantiate(explosionPrefab, explosionPoint.position, Quaternion.identity);
		
		switch (name)
			{
				case string name when (myName == "Trashcan"):
				this.GetComponent<OBJ_trashcan>().Die();
				Debug.Log (myName+" was destroyed!");
				Instantiate(explosionPrefab, explosionPoint.position, Quaternion.identity);
				Instantiate(explosionPrefab, explosionPoint.position, Quaternion.identity);
				break;
			}

		//randomises death timer amongst destroyed items
		//gives variety when killing groups
		//float deathTime = Random.Range(.3f,1.3f);
		
		yield return new WaitForSecondsRealtime(0.5f);
	
		Destroy(this.gameObject);
	}

	
	
}
