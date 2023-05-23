using Game;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponIndicator : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Dictionary<string, Sprite> WeaponSprites = new();
    private Dictionary<string, AudioClip> WeaponSounds = new();
    private AudioSource _audio;


    private Sprite GetWeaponSprite(string weaponName) => GameObject.Find("Weapons/" + weaponName).GetComponent<SpriteRenderer>().sprite;
    private AudioClip GetWeaponSound(string weaponName) => GameObject.Find("Weapons/" + weaponName).GetComponent<AudioSource>().clip;

    private void Start()
    {
        _audio = GameObject.Find("CurrentWeapon").GetComponent<AudioSource>();
    }

    private void Update()
    {
        var currentWeapon = inventory.EquipedWeapon.Name;
        if (!WeaponSprites.ContainsKey(currentWeapon))
            WeaponSprites[currentWeapon] = GetWeaponSprite(currentWeapon);
        if (!WeaponSounds.ContainsKey(currentWeapon))
            WeaponSounds[currentWeapon] = GetWeaponSound(currentWeapon);
        spriteRenderer.sprite = WeaponSprites[currentWeapon];
        _audio.clip = WeaponSounds[currentWeapon];
    }
}