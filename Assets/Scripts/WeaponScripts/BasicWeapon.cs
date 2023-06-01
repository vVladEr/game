using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicWeapon : MonoBehaviour
{
    public LayerMask Enemy;
    public LayerMask Barriers;
    public string Name;
    public int Damage;
    public bool AttackSucc = false;
    public int AdditionalDamage = 0;
    public Collider2D coll { get; set;}
    public float stepLength { get; set;}
    public bool shouldMoveAfterHit = false;

    private void Start()
    {
        InisialiseBaseInfo();
    }
    public abstract void InisialiseBaseInfo();
    public abstract void InisialisePlayer();

    public void Attack(Vector3 attackDirection)
    {
        AttackSucc = false;
        var enemies = GetEnemiesInDirection(attackDirection.normalized);
        foreach (var enemy in enemies)
        {
            Hit(enemy);
            AttackSucc = true;
        }
        if (AttackSucc)
        {
            AdditionalDamage = Math.Min(Damage, AdditionalDamage + 1);
        }

    }


    public abstract List<Collider2D> GetEnemiesInDirection(Vector3 dir);

    public abstract bool IsEnemyInDirection(Vector3 dir);

    public void Hit(Collider2D hit)
    {
        if (hit.GetComponent<EnemyHp>())
        {
            hit.GetComponent<EnemyHp>().TakeHit(Damage + AdditionalDamage);
        }
        if (hit.GetComponent<PlayerHp>())
            hit.GetComponent<PlayerHp>().TakeHit(Damage);
    }

    public void DropAdditinalDamage() 
    {
        AdditionalDamage = 0;
    }
        
}
