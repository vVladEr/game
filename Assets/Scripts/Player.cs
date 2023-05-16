using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game 
{
    public class Player : Character
    {
        public Vector2 InitialPosition;
        public Vector2 PreviousPosition;
        private float lastTime = 0f;
        private bool isAllowedToMove = true;
        public Vector2 Position => transform.position;

        public const float DeltaTime = 1f;
        public const float Eps = 0.1f;
        public const float mathEps = 0.0001f;
        private int damage = 1;
        private bool didActionOnThisTurn = false;
        void Start()
        {
            InitialiseCharacter();
            transform.position = InitialPosition;
        }

        void Update()
        {
            IsAllowedToMove();
            if (isAllowedToMove)
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
            {
                PlayerAttack();
                if(!didActionOnThisTurn)
                    PlayerMove();
            }
            didActionOnThisTurn = false;
        }

        private void IsAllowedToMove() 
        {
            var curTime = Time.time;
            if (curTime - lastTime > Eps && (curTime-lastTime+Eps)%DeltaTime < 2*Eps) 
            {
                Debug.Log(curTime - lastTime);
                isAllowedToMove = true;
                lastTime = curTime;
            }
        }

        private void Hit(Collider2D hit) 
        {
            if (hit.GetComponent<EnemyHp>()) 
            {
                hit.GetComponent<EnemyHp>().TakeHit(damage);
            }
        }
        private void PlayerMove()
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition;
            var directionVector = GetDirectionVector();
            if (Math.Abs(directionVector.x - stepLength) < mathEps)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (Math.Abs(directionVector.x + stepLength) < mathEps)
                GetComponent<SpriteRenderer>().flipX = true;
            if (directionVector != new Vector2(0, 0) && movementDictionary[directionVector]())
            {
                PreviousPosition = currentPosition;
                newPosition = currentPosition + directionVector;
            }
            isAllowedToMove = false;
            transform.position = newPosition;
        }

        private void PlayerAttack()
        {
            var directionVector = GetDirectionVector();
            if (Math.Abs(directionVector.x - stepLength) < mathEps)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (Math.Abs(directionVector.x + stepLength) < mathEps)
                GetComponent<SpriteRenderer>().flipX = true;
            if (directionVector != new Vector2(0, 0) && attackDictionary[directionVector]())
            {
                Hit(attackDictionary[directionVector]());
                didActionOnThisTurn = true;
            }
        }
    }
}
