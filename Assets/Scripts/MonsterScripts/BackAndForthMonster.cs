using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthMonster : BasicMonster
{
    private Vector2[] route;
    private int pointer = 0;
    public bool IsHorizontal;
    public override void InitialiseMonster()
    {
        turnsTimer = TurnsDelay;
        Weapon = GetComponent<Dagger>();
        Weapon.InisialisePlayer();
        if(IsHorizontal)
            route = new Vector2[] {
                new Vector2 (stepLength, 0),
                new Vector2 (-stepLength, 0)
            };
        else
            route = new Vector2[] {
                new Vector2 (0, stepLength),
                new Vector2 (0, -stepLength)
            };
    }

    public override void MonsterUpdate()
    {
        if (turnsTimer == 0)
        {
            turnsTimer = TurnsDelay;
            var directionVector = route[pointer];
            if (directionVector.x == stepLength || pointer == 0)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (directionVector.x == -stepLength || pointer == 1)
                GetComponent<SpriteRenderer>().flipX = true;
            Weapon.Attack(directionVector.normalized);
            if (!Weapon.AttackSucc && IsDirectionFree(directionVector))
            {
                pointer = (pointer+1)%(route.Length);
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
