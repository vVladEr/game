using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game;

public class Inventory : MonoBehaviour
{
    public BasicWeapon EquipedWeapon;
    public List<GameObject> Weapons;
    public bool TakeWeaponOnThisTurn = false;
    private Player player;


    private void Start()
    {
        EquipedWeapon = gameObject.AddComponent<Dagger>();
        EquipedWeapon.InisialisePlayer();
        player = gameObject.GetComponent<Player>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!player.isMoving && !TakeWeaponOnThisTurn && collision.gameObject.tag == "Weapon") 
        {
            Instantiate(GetWeaponToDrop(), transform.position, transform.rotation);
            CollectWeapon(collision);
            Destroy(collision.gameObject);
            TakeWeaponOnThisTurn = true;
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
        Debug.Log(EquipedWeapon);
        EquipedWeapon.InisialisePlayer();   
    }

    private GameObject GetWeaponToDrop()
    {
        foreach (var weapon in Weapons) 
        {
            Debug.Log(weapon.GetComponent<BasicWeapon>().Name);
            Debug.Log(EquipedWeapon.Name);
            if (weapon.GetComponent<BasicWeapon>().Name == EquipedWeapon.Name) 
            {
                Debug.Log(weapon.GetComponent<BasicWeapon>().Name);
                return weapon;
            }
        }
        return null;
    }
}
