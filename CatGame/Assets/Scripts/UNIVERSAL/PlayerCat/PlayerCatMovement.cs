using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerCatMovement : MonoBehaviour
{
	//bool that tracks if the game is paused or not. Player cannot make most inputs while paused
	bool paused;
	public Image PawsedImage;


	public CharacterController2D controller;
	
	public Animator animator;
	
	float runSpeed = 80f;
	
	float horizontalMove = 0f;
	
	Rigidbody2D rb;
	
	PlayerCatStats catStats;
	
	//
	//defines where attacks come from
	public Transform attackPoint;
	
	//
	//attack range (shorter for Booping)
	private float attackRange;
	
	//
	//attack power (booping has 0 attack power)
	[SerializeField] private int attackPower;
	
	private bool canAttack;
	
	//
	//declares enemy layer
	public LayerMask enemyLayers;
	
	//
	//declares item layer
	public LayerMask itemLayers;
	
	//
	//are the cat's claws out or not?
	//clawsOut determines attack power and climbing ability
	bool clawsOut=false;
	
	//
	//is jumping
	//interacts with 2DController
	public bool jump=false;
	
	//
	//separate jump bool for animation
	public bool jumping=false;
	
	//
	//list containing special moves
	//which special move is selected determines available actions
	//int[] specialMoves = new int[] {1, 2};
	
	
	//"better jump"
	//
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
	
	
	//
	//is character charging jump?
	bool charging = false;				//is charging jump? note the notes on the right side here
	
	bool canMove = true;
	
	//
	//determines jump charge time
	//time spent holding down + jump charges jump power, which gets added to jumpforce. increasing jump height
	float jumpCharge=1f;
		
	//
	//is crouching
	bool crouch = false;
	
	//
	//is character attacking?
	//combo attacks how?
	bool attacking = false;
	
	//
	//is character in the booping motion?
	bool booping = false;
	
	//
	//is character expelling waste?
	public bool expellingwaste;

	public float poopee;
	float maxPoopee=100f;
	
	public Transform poo1small;
	public Transform poo2medium;
	public Transform poo3max;
	
	//transform for instantiating poosprites
	public Transform poopoint;

	private enum SpecialMoves
	{
		Nothing,
		Meow,
		Defecate,
		Bite,
	}

	string stringEquippedMove;

	public string SpecialMovesString()
    {
		if (clawsOut)
        {
			switch ((int)equippedSpecial)
            {
				case 0:
					stringEquippedMove = "Nothing";
					break;
				case 1:
					stringEquippedMove = "MeowClawsOut";
					break;
				case 2:
					stringEquippedMove = "PoopeeClawsOut";
					break;
				case 3:
					stringEquippedMove = "BiteClawsOut";
					break;

			}
        }
		else if (!clawsOut)
        {
			switch ((int)equippedSpecial)
			{
				case 0:
					stringEquippedMove = "Nothing";
					break;
				case 1:
					stringEquippedMove = "MeowClawsIn";
					break;
				case 2:
					stringEquippedMove = "PoopeeClawsIn";
					break;
				case 3:
					stringEquippedMove = "BiteClawsIn";
					break;

			}
		}
		return stringEquippedMove;
    }

	private int intSpecialMoves = System.Enum.GetValues(typeof(SpecialMoves)).Length;
	SpecialMoves equippedSpecial;


	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		
		catStats = this.GetComponent<PlayerCatStats>();

		PawsedImage.enabled = false;


		expellingwaste =false;
		canAttack=true;
		//poopee = catStats.GetPoopee();
		equippedSpecial = SpecialMoves.Nothing;
	}

    
    void Update()
    {

		//add velocity while falling
		if (rb.velocity.y < 0)
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
		else if (rb.velocity.y > 0 && !Input.GetButtonDown("1"))
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}


		

		//Debug.Log(catStats.GetPoopee());
		
		if(canMove)
		{
			
		Physics2D.IgnoreLayerCollision(9,12);
		
		Physics2D.IgnoreLayerCollision(9,11);	
		
		Physics2D.IgnoreLayerCollision(9,10);
			
			
		//
		//move speed multiplied by runSpeed
		//GetAxisRaw is specific (and feels better imo)
		//
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		
		//tells animator what horizontalMove is
		//animator is set to play walk / run / crouch depending on speed
		//Mathf.Abs is "absolute" and sets negative speeds to positive (when walking left)
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		
		//
		//can only un/seathe while grounded
		if ((!jumping) && (!crouch) && (Input.GetButtonDown("1")))
		{
			StartCoroutine(Sheathe());
		}

		//cycle through equipped Special Move
		//
		//add later: hold to equip first?
		if (Input.GetButtonDown("2"))
		{
			ChangeSpecialMove();
		}

		//
		//if grounded and poopee button
		//switch to different excrements based on poopee stat
		if (((!jumping) && (!attacking)) && (Input.GetButtonDown("4")))
		{
			SpecialMove();
		}



			

		//
		//what happens while cat is actively peeing
		if (expellingwaste)
		{
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			ChangePoopee(-15f*Time.deltaTime);
			animator.SetBool("IsExpelling", true);
			animator.SetBool("IsPeeing",true);
			Debug.Log ("PSSSSS");
			
			if(poopee<5)
			{
				expellingwaste=false;
				Debug.Log ("Done!");
				canMove=true;
				animator.SetBool("IsPeeing", false);
				animator.SetBool("IsExpelling", false);
				rb.constraints = RigidbodyConstraints2D.None;	
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			}
			
		}
			
		//
		//during normal play cat steadily gains poopee
/* 		if(!expellingwaste)
		{
			//catStats.ChangePoopee(10f * Time.deltaTime);
		} */
		
		if (Input.GetButtonDown("Up"))
		{
			LookUp();
		}
		
		if (Input.GetButtonDown("Attack") && (canAttack))
			{
				if (clawsOut)
				{
					StartCoroutine(Attack());
					Debug.Log ("Yah!");
				}
				else if (!clawsOut)
				{
					StartCoroutine(Boop());
					Debug.Log ("Boop!");
				}
			}
			
		
		
		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
			animator.SetBool("IsCrouching", true);
			Debug.Log ("Crouching!");
						
			if ((crouch) && (Input.GetButtonDown("Attack")))
			{
				if (clawsOut){
				StartCoroutine(CrouchAttack());
				}
				else{
				StartCoroutine(CrouchBoop());
				}
			}
			
			
			if (Input.GetButtonDown("Jump"))
			{
				animator.SetBool("IsCharging", true);
				ChargeJump();
					
					if (Input.GetButtonUp("Jump"))
					{
						Jump();
					}
			}
				
			else if (Input.GetButtonUp("Crouch"))
			{
				charging = false;
				crouch = false;
				animator.SetBool("IsCrouching", false);
				controller.Move(1f, false, false);
			}	
			
		}
		
		if (Input.GetButtonUp("Crouch"))
		{
				crouch = false;
				animator.SetBool("IsCrouching", false);
		}	
		
			//initiates jump
			//
			if ((!jumping) && (Input.GetButtonUp("Jump")))
			{
				Jump();
			}
	
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}

 
 
    void FixedUpdate()
    {

		
		if(charging)
		{
			//increase JumpForce
			controller.ChangeJumpForce(1f * Time.deltaTime);
			print("Charging!");
		}
		
		//
		//
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
		
		
		
		
			
    }
	
	//looks up
	//
	void LookUp()
	{
		Debug.Log ("Cat looks up!");
		//moves camera up
	}
	
	//void LookDown()
	//{
	//	
	//}
	
	//flips the boolean for claws in / claws out
	//
	//investigate time stop, want to make this a somewhat epic event
	//
	IEnumerator Sheathe()
	{
		canMove = false;
		clawsOut=!clawsOut;
			
		if (clawsOut)
		{
			//freezes all XYZ constraints 
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			
			
			//Time.timeScale = 0f;
			//look into Time.timeScale!!
			
			print("Claws out!");
			animator.SetBool("ClawsOut", true);	
			animator.SetBool("Withdrawing", true);
				
			yield return new WaitForSecondsRealtime(1f);
			
			catStats.FlipClaws();
			
			yield return new WaitForSecondsRealtime(1.0f);
			
			//unfreezes
			//re-freezes Z rotation
			rb.constraints = RigidbodyConstraints2D.None;	
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			print("Free!");
			//Time.timeScale = 1.0f;
			animator.SetBool("Withdrawing", false);
			canMove = true;
		}
		
		else
		{
			canMove = false;
			animator.SetBool("ClawsOut", false);
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			
			yield return new WaitForSecondsRealtime(0.4f);	
			
			catStats.FlipClaws();
			rb.constraints = RigidbodyConstraints2D.None;	
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			print("Claws in!");

			canMove = true;
		}
		catStats.EquipSpecial();
		
	}
	
	
	//
	//attack
	//how to do combos?
	//
	IEnumerator Attack()
	{
		attackPower=1;
		//need nested ifs for crouching/jumping variants
		Debug.Log ("YAH!");
		
		canMove=false;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		animator.SetBool("IsAttacking", true);
		
		//
		//creates a circle hitbox based on attack range. detects enemies within the circle
		//change hitbox later. rectangle hitbox is OverLapAreaAll and needs two transform points
		attackRange=4;
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);	
		Collider2D[] hitItems = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, itemLayers);			
		
		//
		//for each enemy hit added to the list and does dmg
		foreach(Collider2D enemy in hitEnemies)
		{
				enemy.GetComponent<NPC_stats>().TakeDamage(1);
		}
		
		foreach(Collider2D item in hitItems)
		{
				item.GetComponent<OBJECT_stats>().TakeDamage(1);
		}
		
		yield return new WaitForSeconds(0.4f);
		
		
			
		animator.SetBool("IsAttacking", false);
		rb.constraints = RigidbodyConstraints2D.None;	
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		canMove=true;
	}
	
	IEnumerator CrouchAttack()
	{
		attackPower=1;
	//	attacking = true;
	//	print("Yah!!!");
	//	attacking = false;
			canMove=false;
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			animator.SetBool("IsAttacking", true);
						
			yield return new WaitForSeconds(0.7f);
			
			animator.SetBool("IsAttacking", false);
			rb.constraints = RigidbodyConstraints2D.None;	
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			canMove=true;
	}
	
	//
	//fires off boop animation by setting attacking to true
	//waits 0.4 seconds
	//sets booping to false
	//
	IEnumerator Boop()
	{
		attackPower=0;
		canMove=false;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		animator.SetBool("IsBooping", true);
			
		//
		//creates a circle hitbox based on attack range. detects enemies within the circle
		attackRange=2;
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
		Collider2D[] hitItems = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, itemLayers);
		
		//
		//for each enemy hit added to the list and does dmg
		foreach(Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<NPC_stats>().TakeDamage(0);		
				Debug.Log ("Bopped NPC!");
			}
		
		foreach(Collider2D item in hitItems)
			{
				item.GetComponent<OBJECT_stats>().TakeDamage(0);		
				Debug.Log ("Bopped item!");
			}
						
		yield return new WaitForSeconds(0.7f);
		
		animator.SetBool("IsBooping", false);
		rb.constraints = RigidbodyConstraints2D.None;	
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		canMove=true;
	}
	
	IEnumerator CrouchBoop()
	{			


		Debug.Log ("Crouch boop!");
		canMove = false;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		animator.SetBool("IsBooping", true);
		animator.SetBool("IsCrouching", true);

		yield return new WaitForSeconds(2.4f);

		canMove = true;
		rb.constraints = RigidbodyConstraints2D.None;	
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		animator.SetBool("IsBooping", false);
		animator.SetBool("IsCrouching", false);


	}
	
	//jumps
	//
	void Jump()
	{

		Debug.Log ("Boing!");
		

	}
	
	void ChargeJump()
	{
		charging = true;
		animator.SetBool("IsCharging", true);
		Debug.Log ("Charging!");
		
		controller.ChangeJumpForce(0.5f * Time.deltaTime);
		runSpeed=0f;
				
		animator.SetBool("IsCharging", false);
		charging = false;
		runSpeed=40f;
	}
	
	//
	//function for pee/pooing
	/* void ExpelWaste()
	{
		
		switch (poopee)
		{
			case float poopee when (poopee <= 65 && poopee >= 25):
			Debug.Log ("Psss!!! The cat peed!");
			expellingwaste=true;
			animator.SetBool("IsPeeing", true);
			Pee();
			break;
			
			case float poopee when (poopee <= 75 && poopee >= 65):
			Debug.Log ("Little shit!");
			catStats.ChangePoopee(-30);
			break;
			
			case float poopee when (poopee <= 85 && poopee >= 75):
			Debug.Log ("Medium shit!");
			catStats.ChangePoopee(-50);
			break;
			
			case float poopee when (poopee >= 95):
			Debug.Log ("Maximum shit!");
			catStats.ChangePoopee(-100);
			break;		

		}
	} */
	

	
	//function for changing poopee
	public void ChangePoopee (float amount)
	{
		Debug.Log ("Poopee changed by "+amount);
		poopee += amount;
		//PoopeeMeter.value=poopee;
		if(poopee>maxPoopee)
		{
			poopee = maxPoopee;
		}
		
		/* //poopee meter is invisible until 25 poopee, also when you can begin to pee
		if(poopee >= 25)
		{
			PoopeeGroup.alpha=1;
		}
		else
		{
			PoopeeGroup.alpha=0;
		} */
		
		//if poopee is less or equal to zero set to zero
		if (poopee<=0)
		{
			poopee=0;
			
		}
		
		
	}
	
	//
	//draws the attack  hitbox
	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
		return;
		
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}

	/* 	void ChangeRunSpeed(float amount)
		{
			runSpeed += amount;
		} */
	
	//Function pauses or unpauses the game
	//if game is paused, Adds an overlay and stops time and Player input
	//else reverses


	//changes the currently equipped Special Move
	//first adds +1 to the currently equipped special move enum. if it reaches the max amount, it cycles back to the first slot, 0
	//
	//add charge a la Dark Souls 3 
	private void ChangeSpecialMove()
    {
		equippedSpecial += 1;
		if ((int)equippedSpecial == intSpecialMoves)
		{
			equippedSpecial = 0;
		}
		catStats.EquipSpecial();

		Debug.Log(equippedSpecial);
	}

	//performs a different special depending on what is currently equipped
	private void SpecialMove()
    {
		int intEquippedSpecial = (int)equippedSpecial;
		switch (intEquippedSpecial)
        {
			case 0:
				Nothing();
				break;

			case 1:
				Meow();
			break;
				
			case 2:
				Defecate();
				break;

			case 3:
				Bite();
				break;
        }
    }

	//Nothing
	//
	//Splash! Maybe trigger idle animations from here!
	private void Nothing()
    {
		Debug.Log("But nothing happened!");
    }

	//Meow
	//
	//Audio communication with the rest of the world
	//
	//if clawsOut, will be aggressive, hiss, yowl, etc
	//
	//if !clawsOut, will purr, be inquisitive, soft, etc
	private void Meow()
    {
		if (clawsOut)
		{
			CatGameSFXScript.PlaySound("Hiss01");
			Debug.Log("MEOW!!!");
		}
		else
		{
			CatGameSFXScript.PlaySound("Meow01");
			Debug.Log("Purr...");
		}
    }

	//Defecate
	//
	//Cat pees or poos depending on poopee level
	private void Defecate()
    {
		switch (poopee)
		{
			case float poopee when (poopee < 15):
				Debug.Log("But nothing happened!");
				break;

			case float poopee when (poopee <= 65 && poopee >= 25):
				Debug.Log("Psss!!! The cat peed!");
				expellingwaste = true;
				animator.SetBool("IsPeeing", true);
				break;

			case float poopee when (poopee <= 75 && poopee >= 65):
				Debug.Log("Little shit!");
				ChangePoopee(-30);
				Instantiate(poo1small, poopoint.position, poopoint.rotation);
				break;

			case float poopee when (poopee <= 85 && poopee >= 75):
				Debug.Log("Medium shit!");
				ChangePoopee(-50);
				Instantiate(poo2medium, poopoint.position, poopoint.rotation);
				break;

			case float poopee when (poopee >= 95):
				Debug.Log("Maximum shit!");
				ChangePoopee(-100);
				Instantiate(poo3max, poopoint.position, poopoint.rotation);
				break;
		}
	}

	//Bite
	//
	//if clawsOut, Bite is an aggressive attack. 
	//Cat can use Bite to eat smaller animals and items. be careful! this doesn't always work
	//
	//if !clawsOut, Bite can pick up small animals and items
	//
	private void Bite()
    {
		if (clawsOut) Debug.Log("Ferocious bite!");
		else Debug.Log("Nom nom!");
    }

	//pauses game
	private void Pause()
	{
		paused = !paused;

		if (paused)
		{
			Time.timeScale = 0;
			PawsedImage.enabled = true;
			canMove = false;
		}

		else if (!paused)
		{
			Time.timeScale = 1;
			PawsedImage.enabled = false;
			canMove = true;
		}
	}

}
