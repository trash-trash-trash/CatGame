using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]

public class NPC_mouse_script: MonoBehaviour
{
			
	CharacterController2D controller;
    
	public Animator animator;
	
	Rigidbody2D rb;
	
	float pushdistance;
	
	float updistance;
	
	string myName="Mouse";
	
	int maxHP=2;
	
	int HP;
	
	int BoopTracker=0;
	
	float speed = 5f;
	
	int decision = 0;
	
	bool isAlive=true;
	
	bool moving=false;
	
	public List<Transform> points;
	
	//
	//int value for next point index
	//0 is first point, etc
	public int nextID=0;
	
	//
	//changes values between points
	int idChangeValue=1;
	
	private void Reset()
	{
			Init();
	}
	
	void Init()
	{
		//make box collider trigger
		GetComponent<BoxCollider2D>().isTrigger = true;
		
		//create root object
		GameObject root = new GameObject(name + "_Root");
		
		//reset position of root to NPC object
		root.transform.position = transform.position;
		
		//set enemy object as child of root
		transform.SetParent(root.transform);
		
		//create waypoints object
		GameObject waypoints = new GameObject("Waypoints");
		
		//reset waypoints position to root
		//make waypoints object child of root
		waypoints.transform.SetParent(root.transform);
		waypoints.transform.position = root.transform.position;
		
		//create two (+) gameobject points and reset their position to waypoint objects
		//make the points children of waypoint objects
		GameObject p1 = new GameObject("Point0");p1.transform.SetParent(waypoints.transform);p1.transform.position = root.transform.position;
		GameObject p2 = new GameObject("Point1");p2.transform.SetParent(waypoints.transform);p2.transform.position = root.transform.position;
		
		//Init points list then add the points
		points = new List<Transform>();
		points.Add(p1.transform);
		points.Add(p2.transform);
		
	}
		
	// Start is called before the first frame update
    void Awake()
    {
			
		this.GetComponent<NPC_stats>().SetupStats(myName, maxHP, BoopTracker, speed);
		
		controller  = GameObject.FindWithTag("Player").GetComponent<CharacterController2D>();
			
        rb = GetComponent<Rigidbody2D>();
		HP = maxHP;
		
		StartCoroutine(Decision());
		
		Physics2D.IgnoreLayerCollision(10,11);
		
		
		
    }

	private void Update()
	{
		
		if(moving)
		{
			MoveToNextPoint();
		}	
		

				
	}
	
		
	IEnumerator Decision()
	{
		
		yield return new WaitForSecondsRealtime(3f);
			
		Debug.Log ("Hmm...");
		
		decision = Random.Range(1,10);
	
			switch (decision)
			{
				
				case int decision when (decision == 5):
				Debug.Log("Idle1!");
				animator.SetFloat("Speed", 0);
				animator.SetFloat("IdleFloat", 1.1f);
				moving=false;
				decision = 0;
				break;
				
				case int decision when (decision < 5):
				Debug.Log("Walking!");
				animator.SetFloat("Speed", 1);
				moving=true;
				decision = 0;
				break;
				
				case int decision when (decision > 5):
				Debug.Log("Standing normally!");
				animator.SetFloat("Speed", 0);
				animator.SetFloat("IdleFloat", 0.1f);
				moving=false;
				decision = 0;
				break;
				
			
		
			}
		
		//restarts decision coroutine
		StartCoroutine(Decision());
		
	} 
	
		
	void MoveToNextPoint()
	{
		//get the next point transform
		Transform goalPoint = points[nextID];
		
		//flip NPC to look at point's direction
		if(goalPoint.transform.position.x>transform.position.x)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		
		//move the enemy towards goal point
		//gets local position and goal position and walks towards it at walkspeed x time
		transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed*Time.deltaTime);
		
		//check distance between enemy and goal point to trigger next point
		if(Vector2.Distance(transform.position, goalPoint.position)<1f)
		{
			//check if we've reached goal point, make change -1
			if(nextID == points.Count - 1)
			{
				idChangeValue = -1;
			}
			
			//check if we've reached start point, make change +1
			if(nextID == 0)
			{
				idChangeValue =1;
			}
			
			//apply change on the nextID
			nextID += idChangeValue;
		}
		
	}
		
}
