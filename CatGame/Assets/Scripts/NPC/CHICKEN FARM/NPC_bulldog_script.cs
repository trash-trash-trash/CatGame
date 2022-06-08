using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_bulldog_script : MonoBehaviour
{
    
	string myName="Bulldog";
	
	//
	//defines collider that hurts cat
	public Collider2D hurtBox;
	
	//
	//defines collider that cat can jump off
	//how to declare without using public drag and drop?
	public Collider2D jumpingBox;
	
	int maxHP=10;
	
	int HP;
	
	int BoopTracker=0;
	
	float speed=7;
	
	CharacterController2D controller;
	
	PlayerCatStats catStats;
    
	public Animator animator;
	
	Rigidbody2D rb;
	
	bool moving=false;
	
	int decision;
	
	
	// Start is called before the first frame update
    void Start()
    {
        this.GetComponent<NPC_stats>().SetupStats(myName, maxHP, BoopTracker, speed);
		
		controller  = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
		catStats = GameObject.FindWithTag("Player").GetComponent<PlayerCatStats>();
		
			
        rb = GetComponent<Rigidbody2D>();
		HP = maxHP;
		
		
		Physics2D.IgnoreLayerCollision(10,11);
		
		StartCoroutine(Decision());
    }

    // Update is called once per frame
    void Update()
    {
		if(moving)
		{
			rb.velocity = new Vector2 (speed * -1, rb.velocity.y);
			transform.localScale = new Vector3(10, 10, 10);
		}
		else
		{
			rb.velocity = new Vector2 (speed, rb.velocity.y);
			transform.localScale = new Vector3(-10, 10, 10);
		}
    }
	
	
	IEnumerator Decision()
	{
		
		yield return new WaitForSeconds (5f);
		
		decision = Random.Range(0,10);
		
		switch (decision)
		{
			case int decision when (decision == 5):
			speed=0;
			Debug.Log ("Standing still!");
			break;
			
			case int decision when (decision > 5):
			moving=true;
			speed=7;
			Debug.Log ("Moving left!");
			
			break;
			
			case int decision when (decision < 5):
			moving=false;
			speed=7;
			Debug.Log ("Moving right!");
			break;
			
		}
		
		StartCoroutine(Decision());
		
	}
	

	
	
}
