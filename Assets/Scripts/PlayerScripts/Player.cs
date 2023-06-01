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
    private int movesTillHintDisapear = 4;
    private int movesLeft = 4;
    private AudioSource openDoorAudio;
    private AudioSource closedDoorAudio;
    void Start()
    {
        InitialiseCharacter();
        inventory = gameObject.GetComponent<Inventory>();
        weaponAudio = GameObject.Find("CurrentWeapon").GetComponent<AudioSource>();
        openDoorAudio = GameObject.Find("OpenDoor").GetComponent<AudioSource>();
        closedDoorAudio = GameObject.Find("ClosedDoor").GetComponent<AudioSource>();
        General = GameObject.Find("General").GetComponent<General>();
        IsAlive = true;
        Cursor.visible = false;
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
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow)))
            return new Vector2(stepLength, 0);
        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow)))
            return new Vector2(-stepLength, 0);
        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow)))
            return new Vector2(0, stepLength);
        if (Input.GetKeyDown(KeyCode.S) || (Input.GetKeyDown(KeyCode.DownArrow)))
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

        if (inventory.EquipedWeapon.AttackSucc)
        {
            MadeStep = true;
            weaponAudio.Play();
        }

        if ((inventory.EquipedWeapon.shouldMoveAfterHit || !inventory.EquipedWeapon.AttackSucc)
            && IsInterectiveFree(directionVector.normalized))
        {
            if (!isDoorLocked) 
            {
                newPosition = currentPosition + directionVector;
                inventory.TakeItemOnThisTurn = false;
                isMoving = true;
                if (movesLeft == 0)
                    hintText.text = "";
                else
                    movesLeft--;
                MadeStep = true;
            }
            return;
        }




        if ((inventory.EquipedWeapon.shouldMoveAfterHit || !inventory.EquipedWeapon.AttackSucc) &&
            IsDirectionFree(directionVector.normalized))
        {
            newPosition = currentPosition + directionVector;
            inventory.TakeItemOnThisTurn = false;
            if (movesLeft == 0) 
                hintText.text = "";
            else
                movesLeft--;
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
            {
                if(isDoorLocked)
                    openDoorAudio.Play();
                return true;
            }
            movesLeft = movesTillHintDisapear;
            closedDoorAudio.Play();
            if (door.IsColoured)
                hintText.text = $"��� ����� ���� ����������� �����";
            else
                hintText.text = $"��� �� ������� �����";

        }
        return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "HowToPlay")
            hintText.text = "����������� WASD ��� ��������� ��� �������� � ����� \n �� ��������� �������� � ����";
    }

    public void UpdateLeftMoves()
    {
        movesLeft = movesTillHintDisapear;
    }
}