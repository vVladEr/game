using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game 
{
    public class Player : Character
    {
        public Vector2 InitialPosition;
        private bool isAllowedToMove = false;
        public Vector2 Position => transform.position;

        public const float DeltaTime = 1f;
        public const float Eps = 0.1f;
        public const float mathEps = 0.0001f;
        public BasicWeapon Weapon;
        void Start()
        {
            InitialiseCharacter();
            transform.position = InitialPosition;
            Weapon = gameObject.AddComponent<Axe>();
            Weapon.Inisialise();
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
                curTime % DeltaTime > DeltaTime - Eps)
                isAllowedToMove = true;
            return isAllowedToMove;
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

            Weapon.Attack(directionVector.normalized);
            if (!Weapon.AttackSucc && IsDirectionFree(directionVector.normalized))
            {
                newPosition = currentPosition + directionVector;
            }
            transform.position = newPosition;
            isAllowedToMove = false;
        }
    }
}
