using Game;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PirsuingMonster : BasicMonster
{
    private float activationDistance = 8*0.16f;

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
            var difference = player.Position - (Vector2)transform.position;
            var directionVector = GetDirectionByDifference(difference);
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

    private Vector2 GetDirectionByDifference(Vector2 difference)
    {
        if (!IsPositionCaptured(Vector2.right * stepLength + (Vector2)transform.position)
            && difference.x > mathEps 
            && ( Weapon.IsEnemyInDirection(Vector2.right)
            || IsDirectionFree(Vector2.right) || IsInterectiveFree(Vector2.right)))
            return new Vector2(stepLength, 0);

        if (!IsPositionCaptured(Vector2.left * stepLength + (Vector2)transform.position)
            && difference.x < -mathEps
            && (Weapon.IsEnemyInDirection(Vector2.left)
            || IsDirectionFree(Vector2.left) || IsInterectiveFree(Vector2.left)))
            return new Vector2(-stepLength, 0);

        if (!IsPositionCaptured(Vector2.up * stepLength + (Vector2)transform.position)
            && difference.y > mathEps
            && (Weapon.IsEnemyInDirection(Vector2.up)
            || IsDirectionFree(Vector2.up) || IsInterectiveFree(Vector2.up)))
            return new Vector2(0, stepLength);

        if (!IsPositionCaptured(Vector2.down * stepLength + (Vector2)transform.position)
            && difference.y < -mathEps 
            && (Weapon.IsEnemyInDirection(Vector2.down)
            || IsDirectionFree(Vector2.down) || IsInterectiveFree(Vector2.down)))
            return new Vector2(0, -stepLength);

        return new Vector2(0, 0);
    }


    public override bool CanUpdate() 
    {
        if (GetDistanceToPlayer() >= activationDistance)
            return false;
        return IsAllowedToMoveByTick();
    }

    private float GetDistanceToPlayer()
        => Vector2.Distance(transform.position, player.transform.position);

}
