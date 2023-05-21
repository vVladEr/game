using Game;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomMonster : BasicMonster
{
    public override void InitialiseMonster()
    {
        turnsTimer = TurnsDelay;
        Weapon = GetComponent<Dagger>();
        Weapon.InisialisePlayer();
    }

    public override void MonsterUpdate()
    {
        if (turnsTimer == 0)
        {
            turnsTimer = TurnsDelay;
            var directionVector = GetRandomDirection();
            if (directionVector.x == stepLength)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (directionVector.x == -stepLength)
                GetComponent<SpriteRenderer>().flipX = true;
            Weapon.Attack(directionVector.normalized);
            if (!Weapon.AttackSucc)
            {
                newPosition = directionVector + (Vector2)transform.position;
                isMoving = true;
            }
        }
        else
            turnsTimer--;

    }

    public override bool CanUpdate()
    {
        return IsAllowedToMoveByTick();
    }

}
