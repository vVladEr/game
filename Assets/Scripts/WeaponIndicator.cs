using Game;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponIndicator : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Dictionary<string, Sprite> WeaponSprites = new();


    private Sprite GetWeaponSprite(string weaponName) => GameObject.Find("Weapons/" + weaponName).GetComponent<SpriteRenderer>().sprite;

    private void Start()
    {
        foreach (var weapon in new[] { "Axe", "Dagger", "Spear", "Bow" })
            WeaponSprites[weapon] = GetWeaponSprite(weapon);
    }

    private void Update()
    {
        var currentWeapon = inventory.EquipedWeapon.Name;
        spriteRenderer.sprite = WeaponSprites[currentWeapon];
    }
}