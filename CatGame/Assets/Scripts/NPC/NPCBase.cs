using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the script for the NPC Scriptable Object
//NPCBase holds a NPC's Stats, such as its Name, Health Points, Attack Power, etc

//create NPC from Inspector by right clicking in a project asset folder
//all SerializedFields are variables that can be set in the Inspector for variable Character creation
[CreateAssetMenu(fileName = "Cat Game NPC", menuName = "Cat Game NPC/Create new NPC")]

public class NPCBase : ScriptableObject
{
    //NPC Name. Not currently used, but fun
    [SerializeField] string myName;

    //bool determines if an NPC can be Carried in the mouth of the Player
    //smaller animals like kittens and mice can be Carried, larger animals like dogs and humans cannot
    [SerializeField] bool canCarry;

    //to be utilised properly in the future. Declares the sprite of the Character's projectile
    [SerializeField] Sprite projectileSprite;

    //determines Character's maximum Health Points (HP) value
    //the more HP an NPC has the harder it is to kill
    [SerializeField] int maxHP;

    //determines Character's movement speed
    [SerializeField] float moveSpeed;

    //determines how much damage a Character does per hit
    [SerializeField] float attackPower;

    //determines how fast projectiles are fired
    [SerializeField] float fireRate;

    //determines how fast projectiles travel through the air once fired
    [SerializeField] float projectileSpeed;

    //determines the NPC's chance to drop items upon death
    [SerializeField] float chanceToDrop;

    //gets private stats and declares them publicly upon request from scripts
    public string Name
    {
        get { return myName; }
    }

    public Sprite ProjectileSprite
    {
        get { return projectileSprite; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    public float AttackPower
    {
        get { return attackPower; }
    }

    public float FireRate
    {
        get { return fireRate; }
    }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }
    public float ChanceToDrop
    {
        get { return chanceToDrop; }
    }


}
