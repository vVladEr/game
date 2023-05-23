using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int necessaryKeyNumber;
    private SpriteRenderer currentSprite;
    [SerializeField] private Sprite openDoor;
    public bool IsAllowedToWalkIn = false;
    public void Act(int keyAmount)
    {
        if (CheckCondition(keyAmount)) 
        {
            IsAllowedToWalkIn = true;
            currentSprite.sprite = openDoor;
        }
    }

    public bool CheckCondition(int keyAmount)
    {
        if(IsAllowedToWalkIn)
            return true;
        return keyAmount == necessaryKeyNumber;
    }

    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
    }
}
