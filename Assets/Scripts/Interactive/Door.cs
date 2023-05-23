using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string color;
    private SpriteRenderer currentSprite;
    [SerializeField] private Sprite openDoor;
    public bool IsAllowedToWalkIn = false;
    public void Act(Dictionary<string, bool> keys)
    {
        if (CheckCondition(keys)) 
        {
            IsAllowedToWalkIn = true;
            currentSprite.sprite = openDoor;
        }
    }

    public bool CheckCondition(Dictionary<string, bool> keys)
    {
        if(IsAllowedToWalkIn)
            return true;
        return keys[color] == true;
    }

    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        ChangeColor();
    }

    void ChangeColor()
    {
        currentSprite.color = color switch 
        {
            "red" => new Color(1, 0.39f, 0.28f, 1),
            "yellow" => Color.yellow,
            "blue" => new Color(0, 0.75f, 1, 1),
            _ => Color.white
        };
    }
}
