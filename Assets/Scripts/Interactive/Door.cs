using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] public string Color;
    private SpriteRenderer currentSprite;
    [SerializeField] private Sprite openDoor;
    public bool IsAllowedToWalkIn = false;
    [SerializeField] public int Price = 0;
    [SerializeField] public bool IsColoured;
    [SerializeField] private Text priceText;
    public void Act(Dictionary<string, bool> keys)
    {
        if (IsAllowedToWalkIn || CheckColourCondition(keys) ||
            (!IsColoured && CheckPriceCondition()))
        {
            IsAllowedToWalkIn = true;
            currentSprite.sprite = openDoor;
        }
    }

    private bool CheckColourCondition(Dictionary<string, bool> keys)
    {
        if(IsAllowedToWalkIn)
            return true;
        if(keys.ContainsKey(Color))
            return keys[Color] == true;
        return false;
    }

    private bool CheckPriceCondition() 
    {
        var inventory = FindFirstObjectByType<Player>().GetComponent<Inventory>();
        if (inventory.CoinsCounter >= Price) 
        {
            inventory.CoinsCounter -= Price;
            return true;
        }
        return false;
    }

    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        ChangeColor();
        if (Color == "")
            IsColoured = false;
        else
            IsColoured = true;
        if (!IsColoured) 
        {
            priceText.text = $"x{Price}";
        }
    }

    void ChangeColor()
    {
        currentSprite.color = Color switch 
        {
            "red" => new Color(1, 0.39f, 0.28f, 1),
            "yellow" => UnityEngine.Color.yellow,
            "blue" => new Color(0, 0.75f, 1, 1),
            _ => UnityEngine.Color.white
        };
    }
}
