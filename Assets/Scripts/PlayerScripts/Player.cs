using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game 
{
    public class Player : Character
    {
        public Vector2 InitialPosition;
        private bool isAllowedToMove = false;
        public bool RightTime = false;
        public Vector2 Position => transform.position;

        public const float DeltaTime = 1f;
        public const float Eps = 0.15f;
        public const float mathEps = 0.0001f;
        private Inventory inventory;
        void Start()
        {
            InitialiseCharacter();
            inventory = gameObject.GetComponent<Inventory>();
        }

        void Update()
        {
            if (IsAllowedToMove())
                PlayerUpdate();
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
        }

        private bool IsAllowedToMove() 
        {
            var curTime = Time.time;
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
            Vector2 newPosition = currentPosition;

            if (Math.Abs(directionVector.x - stepLength) < mathEps)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (Math.Abs(directionVector.x + stepLength) < mathEps)
                GetComponent<SpriteRenderer>().flipX = true;

            inventory.EquipedWeapon.Attack(directionVector.normalized);
            if (!inventory.EquipedWeapon.AttackSucc &&
                IsDirectionFree(directionVector.normalized))
            {
                newPosition = currentPosition + directionVector;
                inventory.TakeWeaponOnThisTurn = false;
            }
            transform.position = newPosition;
        }
    }
}
