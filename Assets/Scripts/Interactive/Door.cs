using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string color;
    private SpriteRenderer currentSprite;
    [SerializeField] private Sprite openDoor;
    public bool IsAllowedToWalkIn = false;
    [SerializeField] private int price = 0;
    public void Act(Dictionary<string, bool> keys)
    {
        if (IsAllowedToWalkIn || CheckColourCondition(keys) ||
            (color == "" && CheckPriceCondition()))
        {
            IsAllowedToWalkIn = true;
            currentSprite.sprite = openDoor;
        }
    }

    private bool CheckColourCondition(Dictionary<string, bool> keys)
    {
        if(IsAllowedToWalkIn)
            return true;
        if(keys.ContainsKey(color))
            return keys[color] == true;
        return false;
    }

    private bool CheckPriceCondition() 
    {
        var inventory = FindFirstObjectByType<Player>().GetComponent<Inventory>();
        if (inventory.CoinsCounter >= price) 
        {
            inventory.CoinsCounter -= price;
            return true;
        }
        return false;
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
