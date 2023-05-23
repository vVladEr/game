using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicInteractive : MonoBehaviour
{
    public bool IsAllowedToWalkIn = false;

    public abstract bool CheckCondition();

    public abstract bool Act();

}
