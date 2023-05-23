using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game 
{
    public class Player : Character
    {
        private Vector2 InitialPosition;
        public bool RightTime = false;
        public const float mathEps = 0.0001f;
        private AudioSource weaponAudio;
        private Inventory inventory;
        void Start()
        {
            InitialiseCharacter();
            inventory = gameObject.GetComponent<Inventory>();
            weaponAudio = GameObject.Find("CurrentWeapon").GetComponent<AudioSource>();
            General = GameObject.Find("General").GetComponent<General>();
        }

        void Update()
        {
            MoveSmoothly();
            if (IsAllowedToMove()) 
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
        }

        private bool IsAllowedToMove() 
        {
            var curTime = General.Time;
            if (curTime % DeltaTime < Eps ||
                curTime % DeltaTime > (DeltaTime - Eps))
            {
                RightTime = true;
                return true;
            }
            RightTime = false;
            return false;
        }


        private void PlayerAct()
        {
            var directionVector = GetDirectionVector();
            if (directionVector == Vector2.zero) 
                return;

            Vector2 currentPosition = transform.position;
            newPosition = currentPosition;

            if (Math.Abs(directionVector.x - stepLength) < mathEps)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (Math.Abs(directionVector.x + stepLength) < mathEps)
                GetComponent<SpriteRenderer>().flipX = true;

            if (IsInterectiveFree(directionVector.normalized))
            {
                newPosition = currentPosition + directionVector;
                inventory.TakeItemOnThisTurn = false;
                isMoving = true;
                return;
            }

            inventory.EquipedWeapon.Attack(directionVector.normalized);
            if (inventory.EquipedWeapon.AttackSucc) 
            {
                weaponAudio.Play();
            }


            if ((inventory.EquipedWeapon.shouldMoveAfterHit || !inventory.EquipedWeapon.AttackSucc) &&
                IsDirectionFree(directionVector.normalized))
            {
                newPosition = currentPosition + directionVector;
                inventory.TakeItemOnThisTurn = false;
                isMoving = true;
            }
        }

        private bool IsInterectiveFree(Vector3 dir)
        {
            var interaciveObject = Physics2D.BoxCast(coll.bounds.center + stepLength * dir.normalized, coll.bounds.size, 0f,
                Vector2.right, 0, Interactive).collider;
            if (!interaciveObject)
                return false;
            if (interaciveObject.gameObject.tag == "Door")
            {
                var door = interaciveObject.GetComponent<Door>();
                door.Act(inventory.Keys);
                return door.IsAllowedToWalkIn;
            }
            return false;
        }
    }
}