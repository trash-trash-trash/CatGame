	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	//[RequireComponent(typeof(BoxCollider2D))]

	public class OBJ_trashcan: MonoBehaviour
	{
		
		//trashcan that may hold other items
		
		CharacterController2D controller;
		
		public Animator animator;
		
		public GameObject OBJ_ciderjugPrefab;
		
		
		public GameObject OBJ_beerbottlePrefab;
		public GameObject OBJ_beerbottle01Prefab;
		
		public GameObject OBJ_chickenEggPrefab;
		
		public GameObject OBJ_gamecontroller01Prefab;
	
		public GameObject OBJ_beersteinPrefab;
		public GameObject OBJ_brokenmicrowavePrefab;
		public GameObject Poo3MaxPrefab;	
		public GameObject NPC_kittengreyPrefab;
		public GameObject NPC_kittenorangePrefab;
		public GameObject NPC_kittenblackPrefab;
		public GameObject NPC_kittenwhitePrefab;
		public GameObject NPC_kittentortyPrefab;
		

				
		Rigidbody2D rb;
		
		Transform spawnPoint;
		
		float pushdistance;
		
		float updistance;
		
		string myName="Trashcan";
		
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
			
			StartCoroutine(Idle());
			
		}
		
		IEnumerator Idle()
		{
			float waitTime = (Random.Range(0.1f, 5.5f));
			
			yield return new WaitForSecondsRealtime (waitTime);
			
			animator.SetFloat("IdleFloat", 1f);
			
			
			yield return new WaitForSecondsRealtime (.3f);
			
			animator.SetFloat("IdleFloat",0f);
			
			StartCoroutine(Idle());
			
		}
		
		public void Die()
		{
			spawnPoint = this.gameObject.transform;
			
			int trashSpawn = Random.Range(0,15);
			
			switch (trashSpawn)
			{
				case 0:
				Debug.Log("But nothing happened!");
				break;
				
				case 1:
				Debug.Log("Spawned a ciderjug!");
				Instantiate(OBJ_ciderjugPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 2:
				Debug.Log("Spawned a beer bottle!");
				Instantiate(OBJ_beerbottlePrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 3:
				Debug.Log("Spawned a game controller 01!");
				Instantiate(OBJ_gamecontroller01Prefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 4:
				Debug.Log("Spawned a grey kitten!");
				Instantiate(NPC_kittengreyPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 5:
				Debug.Log("Spawned a beerstein!");
				Instantiate(OBJ_beersteinPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 6:
				Debug.Log("Spawned a broken microwave!");
				Instantiate(OBJ_brokenmicrowavePrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				/* case 7:
				Debug.Log("Spawned a flower 01!");
				Instantiate(OBJ_flower01Prefab, spawnPoint.position, Quaternion.identity);
				break; */
				
				case 7:
				Debug.Log("Spawned a chicken egg!");
				Instantiate(OBJ_chickenEggPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 8:
				Debug.Log("Spawned a beer bottle 01!");
				Instantiate(Poo3MaxPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 9:
				Debug.Log("Spawned a max poo!");
				Instantiate(Poo3MaxPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 10:
				Debug.Log("Spawned a white kitten!");
				Instantiate(Poo3MaxPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 11:
				Debug.Log("Spawned a black kitten!");
				Instantiate(NPC_kittenblackPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 12:
				Debug.Log("Spawned an orange kitten!");
				Instantiate(NPC_kittenorangePrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 13:
				Debug.Log("Spawned a tortoise-shell kitten!");
				Instantiate(NPC_kittentortyPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				case 14:
				Debug.Log("Spawned a grey kitten!");
				Instantiate(NPC_kittengreyPrefab, spawnPoint.position, Quaternion.identity);
				break;
				
				
				
			}
			

			
			
		}
		



		
	}
