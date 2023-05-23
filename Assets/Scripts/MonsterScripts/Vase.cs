using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : BasicMonster
{
    public int hp;

    public override void InitialiseMonster()
    {
        turnsTimer = TurnsDelay;
        Weapon = GetComponent<Dagger>();
        Weapon.InisialisePlayer();
    }

    public override void MonsterUpdate()
    {
        if (turnsTimer == 0) turnsTimer = TurnsDelay;
        else turnsTimer--;
    }

    public override bool CanUpdate()
    {
        return true;
    }
}
