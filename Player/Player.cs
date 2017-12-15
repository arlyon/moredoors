using System.Collections;
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

        public int keys;

        [SerializeField] private StartDoor start;

        public float transtitionSpeed;
        
        // Use this for initialization
        void Start () {
            head = gameObject.GetComponentInChildren<PlayerHead>();
            arms = gameObject.GetComponentInChildren<PlayerArms>();
            controller = gameObject.GetComponentInChildren<CharacterController>();
            start.Arrive(this);
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
            controller.SimpleMove(vec);
        }
    
        public void Interactions()
        {
            GameObject focused = head.GetFocused();
            if (Input.GetMouseButtonDown(0))
            {
                if (focused == null) return;
                
                var interactable = focused.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(this, InteractionType.Primary);
                } else
                {
                    Debug.Log("Non-interactable object");
                }
            }
        }

        public void tweenPosition(Vector3 position)
        {
            head.transform.localPosition = position;
        }

        public void tweenRotation(Vector3 rot)
        {
            head.transform.localEulerAngles = rot;
        }

        public void tweenRotationComplete()
        {
            head.transform.localEulerAngles = new Vector3(0,0,0);
            head.rotationY = 0;
            head.originalRotation = head.transform.localRotation;
        }


        public void TeleportTo(IDoor door)
        {
            Vector3 localPosition = head.transform.localPosition;
            Vector3 fromPosition = head.transform.position;

            Quaternion fromRotation = head.transform.rotation;

            this.transform.position = door.GetExitLocation() + Vector3.up; //todo dont hardcode
            this.transform.rotation = door.GetExitRotation();

            head.transform.position = fromPosition;
            head.transform.rotation = fromRotation;


            iTween.ValueTo(gameObject, iTween.Hash("from", head.transform.localPosition, "to", localPosition, "time", transtitionSpeed, "easetype", iTween.EaseType.easeInOutCirc,"onupdate","tweenPosition"));
            iTween.ValueTo(gameObject, iTween.Hash("from", head.transform.localEulerAngles,"to", new Vector3(0,0,0), "time", transtitionSpeed, "easetype", iTween.EaseType.easeInOutCirc,"oncomplete","tweenRotationComplete","onupdate","tweenRotation"));
        }


        public bool HasKey(IDoor door)
        {
            return this.keys > 0;
        }

        public void DestroyKey(IDoor door)
        {
            this.keys -= 1;
        }

        public void SetHeldDoorPair(IDoor door)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks if the player is holding a door.
        /// </summary>
        /// <returns></returns>
        public bool HasDoor()
        {
            return false;
        }

        public void AddKey(Key key)
        {
            this.keys += 1;
        }

        public float GetHealth()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }

        public void SetHealth(float health)
        {
            throw new System.NotImplementedException();
        }

        public void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}
