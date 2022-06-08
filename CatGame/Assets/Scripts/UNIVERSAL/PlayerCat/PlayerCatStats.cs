using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCatStats : MonoBehaviour
{

	//this script contains the player Cat's stats (health, poopee, etc)
	//as well as corresponding UI elements and code

	PlayerCatMovement catMovement;
	
	//
	
	//public Canvas LifeCanvas;	
	public CanvasGroup LifeGroup;
	int HP;
	int MaxHP;
	public GameObject[] hearts;
	
	int Lives;
	int MaxLives=9;
	public GameObject lives;
	public Text LifeText;
	
	bool isAlive;
	
	bool clawsOut;
	public Image ClawsIn;
	public Image ClawsOut;
	
	public float poopee;
	float maxPoopee;
	public Slider PoopeeMeter;

	public Image SpecialMove;
	[SerializeField] Sprite Nothing;
	[SerializeField] Sprite MeowClawsIn;
	[SerializeField] Sprite MeowClawsOut;
	[SerializeField] Sprite PoopeeClawsIn;
	[SerializeField] Sprite PoopeeClawsOut;
	[SerializeField] Sprite BiteClawsIn;
	[SerializeField] Sprite BiteClawsOut;

	public float GetPoopee()
	{
		return poopee;	
	}
	
	public void SetPoopee(float newPoopee)
	{
		poopee = newPoopee;
	}
	
	//Start
    void Start()
    {
		catMovement = this.GetComponent<PlayerCatMovement>();

		Lives = 9;
		MaxHP=3;
		isAlive=true;
		HP=MaxHP;
		clawsOut=false;
		poopee=25;
		maxPoopee=100;
		
		ClawsOut.enabled = false;
		ClawsIn.enabled = true;
		
		//fades out Life UI elements
		lives.SetActive(true);
		
		
    }

    // Update is called once per frame
    void Update()
    {





     
		
		
        
		//tells LifeText how many lives you have left
		LifeText.text = ("x "+Lives);
		
    }
	
	/* void OnEnable()
	{
		CatEventManager.PlayerDamagedEvent += TakeDamage;
	} */
	
/* 	void OnDisable()
	{
		CatEventManager.PlayerDamagedEvent -= TakeDamage;
	} */
	
	
	void FixedUpdate()
	{
		//poopee goes up over time
		//ChangePoopee(1f*Time.deltaTime);
	}
	
	//function for taking damage
	public void TakeDamage(int amount)
	{
		HP += amount;
		
		//function only triggers with HP left to lose
		if(HP >= 1)
		{
			HP -= amount;
			Destroy(hearts[HP].gameObject);
			
			if(HP < 1)
			{
				LoseLife(-1);
			}
		}
	}
	
	//function for losing life
	public void LoseLife(int amount)
	{
		Lives -= amount;
		
		if(Lives < 1)
		{
			//Game Over event goes here
			print("Game Over!");
		}
	}
	
	//function for flipping ClawsOut
	public void FlipClaws()
	{
		clawsOut=!clawsOut;
		
		if(clawsOut)
		{
			ClawsOut.enabled = true;
			ClawsIn.enabled = false;
		}
		else
		{
			ClawsOut.enabled = false;
			ClawsIn.enabled = true;
		}
		
	}

	public void EquipSpecial()
    {


		string equippedSpecial = catMovement.SpecialMovesString();

		switch (equippedSpecial)
		{
			case ("Nothing"):
				SpecialMove.sprite = Nothing;
				break;
			case ("MeowClawsOut"):
				SpecialMove.sprite = MeowClawsOut;
				break;
			case ("MeowClawsIn"):
				SpecialMove.sprite = MeowClawsIn;
				break;
			case ("PoopeeClawsOut"):
				SpecialMove.sprite = PoopeeClawsOut;
				break;
			case ("PoopeeClawsIn"):
				SpecialMove.sprite = PoopeeClawsIn;
				break;
			case ("BiteClawsOut"):
				SpecialMove.sprite = BiteClawsOut;
				break;
			case ("BiteClawsIn"):
				SpecialMove.sprite = BiteClawsIn;
				break;

		}

	}

	private IEnumerator FadeUI()
    {
		yield return new WaitForSecondsRealtime(3f);

		


    }
	
	/* //function for changing poopee
	public void ChangePoopee (float amount)
	{
		Debug.Log ("Poopee changed by "+amount);
		poopee += amount;
		PoopeeMeter.value=poopee;
		if(poopee>maxPoopee)
		{
			poopee = maxPoopee;
		}
		
		//poopee meter is invisible until 25 poopee, also when you can begin to pee
		if(poopee >= 25)
		{
			PoopeeGroup.alpha=1;
		}
		else
		{
			PoopeeGroup.alpha=0;
		}
		
		//if poopee is less or equal to zero set to zero
		if (poopee<=0)
		{
			poopee=0;
			
		}
		

		Debug.Log ("NEWPOOPEE ="+poopee);
		
	} */
	
			

}



