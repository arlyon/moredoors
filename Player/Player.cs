﻿using System.Collections;
using System.Collections.Generic;
using Interfaces;
using MoreDoors;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IPlayer {
    
        public PlayerHead head;
    
        public PlayerArms arms;
    
        private CharacterController controller;
    
        public float sensitivity = 1;
    
        public float speed;
        
        // Use this for initialization
        void Start () {
            head = gameObject.GetComponentInChildren<PlayerHead>();
            arms = gameObject.GetComponentInChildren<PlayerArms>();
            controller = gameObject.GetComponentInChildren<CharacterController>();
        }
    
        // Update is called once per frame
    
        void Update () {
            Rotation();
            Movement();
            Interactions();
        }
        /// <summary>
        /// Rotates Player and Head.
        /// </summary>
        void Rotation()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity;
            head.Move(mouseDelta.y);
            Vector3 delta = new Vector3(0, mouseDelta.x, 0);
            transform.Rotate(delta);
        }
        /// <summary>
        /// Moves player with WASD. and Lerft Shift to Sprint.
        /// </summary>
        void Movement()
        {
            Vector3 vec = new Vector3();
    
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    vec.z += 5;
                } else
                {
                    vec.z += 1;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                vec.z -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                vec.x += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                vec.x -= 1;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                vec = vec.normalized * speed * 2f;
            } else
            {
                vec = vec.normalized * speed;
            }
            vec = gameObject.transform.TransformDirection(vec);
            controller.Move(vec * Time.deltaTime);
        }
    
        public void Interactions()
        {
            GameObject focused = head.GetFocused();
            if (Input.GetMouseButtonDown(0))
            {
                if (focused != null)
                {
                    IInteractable interact = focused.GetComponent<IInteractable>();
                    if (interact != null)
                    {
                        interact.Interact(this, InteractionType.Primary);
                    } else
                    {
                        Debug.Log("Non-interactable object");
                    }
                }
            }
        }
    
        public void TeleportTo(IDoor door)
        {
            throw new System.NotImplementedException();
        }

        public bool HasKey(IDoor door)
        {
            throw new System.NotImplementedException();
        }

        public void DestroyKey(IDoor door)
        {
            throw new System.NotImplementedException();
        }

        public void SetHeldDoorPair(IDoor door)
        {
            throw new System.NotImplementedException();
        }
    }
}