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
            FlipSprite(directionVector);
            Weapon.Attack(directionVector.normalized);
            if (!Weapon.AttackSucc
                && !IsPositionCaptured((Vector2)transform.position + directionVector)
                && (IsDirectionFree(directionVector) || IsInterectiveFree(directionVector)))
            {
                newPosition = directionVector + (Vector2)transform.position;
                General.CapturedPositions.Add(FixPosition(newPosition));
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
