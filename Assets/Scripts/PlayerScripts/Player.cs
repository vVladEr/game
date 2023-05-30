using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private bool MadeStep = false;
    public bool RightTime = false;

    private AudioSource weaponAudio;
    private Inventory inventory;
    [SerializeField] public Text hintText;
    public bool IsAlive;
    private bool isDoorLocked;
    void Start()
    {
        InitialiseCharacter();
        inventory = gameObject.GetComponent<Inventory>();
        weaponAudio = GameObject.Find("CurrentWeapon").GetComponent<AudioSource>();
        General = GameObject.Find("General").GetComponent<General>();
        IsAlive = true;
    }

    void Update()
    {
        MoveSmoothly();
        if (IsAllowedToMove() && IsAlive) 
        {
            PlayerUpdate();
        }

    }

    private Vector2 GetDirectionVector()
    {
        if (Input.GetKeyDown(KeyCode.D))
            return new Vector2(stepLength, 0);
        if (Input.GetKeyDown(KeyCode.A))
            return new Vector2(-stepLength, 0);
        if (Input.GetKeyDown(KeyCode.W))
            return new Vector2(0, stepLength);
        if (Input.GetKeyDown(KeyCode.S))
            return new Vector2(0, -stepLength);

        return new Vector2(0, 0);
    }

    private void PlayerUpdate() 
    {
        if (Input.anyKeyDown)
            PlayerAct();
        else if (IsAfterTheTick()) 
            inventory.EquipedWeapon.DropAdditinalDamage();
        General.CapturedPositions.Clear();
    }

    private bool IsAllowedToMove() 
    {
        var curTime = General.Time;
        if (curTime % DeltaTime < Eps ||
            curTime % DeltaTime > (DeltaTime - Eps))
        {
            RightTime = true;
            if (!MadeStep)
            {
                return true;
            }
        }
        else
        {
            MadeStep = false;
            RightTime = false;
        }

        return false;
    }


    private void PlayerAct()
    {
        var directionVector = GetDirectionVector();
        if (directionVector == Vector2.zero) 
            return;

        Vector2 currentPosition = transform.position;
        newPosition = currentPosition;

        FlipSprite(directionVector);

        inventory.EquipedWeapon.Attack(directionVector.normalized);
        if (IsInterectiveFree(directionVector.normalized))
        {
            if (!isDoorLocked) 
            {
                newPosition = currentPosition + directionVector;
                inventory.TakeItemOnThisTurn = false;
                isMoving = true;
                hintText.text = "";
                MadeStep = true;
            }
            return;
        }

        if (inventory.EquipedWeapon.AttackSucc) 
        {
            MadeStep = true;
            weaponAudio.Play();
        }


        if ((inventory.EquipedWeapon.shouldMoveAfterHit || !inventory.EquipedWeapon.AttackSucc) &&
            IsDirectionFree(directionVector.normalized))
        {
            newPosition = currentPosition + directionVector;
            inventory.TakeItemOnThisTurn = false;
            hintText.text = "";
            isMoving = true;
            MadeStep = true;
        }
    }

    private bool IsInterectiveFree(Vector3 dir)
    {
        var interaciveObject = Physics2D.BoxCast(coll.bounds.center + stepLength * dir.normalized, coll.bounds.size, 0f,
            Vector2.right, 0, Interactive).collider;
        if (!interaciveObject)
            return false;
        if (interaciveObject.gameObject.tag == "Door" ||
            interaciveObject.gameObject.tag == "ExitDoor")
        {
            var door = interaciveObject.GetComponent<Door>();
            isDoorLocked = !door.IsAllowedToWalkIn;
            door.Act(inventory.Keys);
            if (door.IsAllowedToWalkIn)
                return true;
            if (door.IsColoured)
                hintText.text = $"You need {door.Color} key to open this door";
            else if (door.Price == 1)
                hintText.text = $"You need 1 coin to open this door";
            else
                hintText.text = $"You need {door.Price} coins to open this door";
        }
        return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "HowToPlay")
            hintText.text = "Use WASD to move and attack";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "HowToPlay")
            hintText.text = "";
    }
}