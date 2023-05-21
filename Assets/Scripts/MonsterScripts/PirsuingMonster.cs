using Game;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PirsuingMonster : BasicMonster
{
    private float activationDistance = 0.64f;

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
            if (directionVector.x == stepLength)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (directionVector.x == -stepLength)
                GetComponent<SpriteRenderer>().flipX = true;
            Weapon.Attack(directionVector.normalized);
            if (!Weapon.AttackSucc)
            {
                Debug.Log(directionVector);
                newPosition = directionVector + (Vector2)transform.position;
                isMoving = true;
            }
        }
        else
            turnsTimer--;

    }

    private Vector2 GetDirectionByDifference(Vector2 difference)
    {
        Debug.Log(difference);
        if (difference.x > mathEps 
            && IsDirectionFree(new Vector2(1, 0)))
            return new Vector2(stepLength, 0);

        if (difference.x < -mathEps
            && IsDirectionFree(new Vector2(-1, 0)))
            return new Vector2(-stepLength, 0);

        if (difference.y > mathEps
            && IsDirectionFree(new Vector2(0, 1))) 
            return new Vector2(0, stepLength);

        if (difference.y < -mathEps 
            && IsDirectionFree(new Vector2(0, -1))) 
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
