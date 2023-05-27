using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public BasicWeapon EquipedWeapon;
    [SerializeField] private List<GameObject> Weapons;
    public bool TakeItemOnThisTurn = false;
    private Player player;
    private AudioSource collectWeaponAudio;
    public Dictionary<string, bool> Keys = new();
    public int CoinsCounter = 0;
    [SerializeField] private Text coinsAmountText;

    private void Start()
    {
        EquipedWeapon = gameObject.AddComponent<Dagger>();
        EquipedWeapon.InisialisePlayer();
        player = gameObject.GetComponent<Player>();
        collectWeaponAudio = GameObject.Find("WeaponChange").GetComponent<AudioSource>();
        Keys["blue"] = false;
        Keys["red"] = false;
        Keys["yellow"] = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!player.isMoving && !TakeItemOnThisTurn) 
        {
            switch (collision.gameObject.tag) 
            {
                case "Weapon":
                    Instantiate(GetWeaponToDrop(), transform.position, transform.rotation);
                    CollectWeapon(collision);
                    Destroy(collision.gameObject);
                    TakeItemOnThisTurn = true;
                    break;

                case "SmallHealthPotion":
                    gameObject.GetComponent<PlayerHp>().GainHealth(2);
                    Destroy(collision.gameObject);
                    TakeItemOnThisTurn = true;
                    break;

                case "Heart":
                    gameObject.GetComponent<PlayerHp>().AddHeart();
                    Destroy(collision.gameObject);
                    TakeItemOnThisTurn = true;
                    break;

                case "Key":
                    Keys[collision.gameObject.GetComponent<Key>().Color] = true;
                    collectWeaponAudio.Play();
                    Destroy(collision.gameObject);
                    TakeItemOnThisTurn = true;
                    break;

                case "Coin":
                    CoinsCounter++;
                    Destroy(collision.gameObject);
                    break;
            }
        }
    }

    private void CollectWeapon(Collider2D coll) 
    {
        var temp = coll.GetComponent<BasicWeapon>();
        switch (temp) 
        {
            case Spear:
                if (!gameObject.GetComponent<Spear>())
                    EquipedWeapon = gameObject.AddComponent<Spear>();
                else
                    EquipedWeapon = gameObject.GetComponent<Spear>();
                break;

            case Axe:
                if (!gameObject.GetComponent<Axe>())
                    EquipedWeapon = gameObject.AddComponent<Axe>();
                else
                    EquipedWeapon = gameObject.GetComponent<Axe>();
                break;

            case Bow:
                if (!gameObject.GetComponent<Bow>())
                    EquipedWeapon = gameObject.AddComponent<Bow>();
                else
                    EquipedWeapon = gameObject.GetComponent<Bow>();
                break;
            case Dagger:
                if (!gameObject.GetComponent<Dagger>())
                    EquipedWeapon = gameObject.AddComponent<Dagger>();
                else
                    EquipedWeapon = gameObject.GetComponent<Dagger>();
                break;
        }
        collectWeaponAudio.Play();

        EquipedWeapon.InisialisePlayer();   
    }

    private GameObject GetWeaponToDrop()
    {
        foreach (var weapon in Weapons) 
        {
            if (weapon.GetComponent<BasicWeapon>().Name == EquipedWeapon.Name) 
            {
                weapon.GetComponent<BasicWeapon>().DropAdditinalDamage();
                return weapon;
            }
        }
        return null;
    }

    private void Update()
    {
        coinsAmountText.text = $"x{CoinsCounter}";
    }
}
