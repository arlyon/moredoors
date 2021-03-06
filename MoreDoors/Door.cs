﻿using System;
using System.Linq;
using System.Xml;
using Interfaces;
using UnityEngine;

namespace MoreDoors
{
    /// <inheritdoc cref="IDoor" />
    /// <summary>
    /// The main door class.
    /// </summary>
    public class Door : MonoBehaviour, IDoor, IInteractable
    {
        /// <summary>
        /// The next door.
        /// </summary>
        [SerializeField]
        private Door _nextDoor;
        
        /// <summary>
        /// The color of the door.
        /// </summary>
        [SerializeField]
        private Color _color;
        
        /// <summary>
        /// Gets or sets the color and updates the material.
        /// </summary>
        protected Color Color
        {
            get { return _color;  }
            set
            {
                this.GetComponent<Renderer>().material.SetColor("_Color", value);
                this._color = value;
            }
        }

        /// <summary>
        /// Sets the color from the editor and pairs with the next door.
        /// </summary>
        private void Start ()
        {
            if (this._nextDoor == null) return;
            this.Color = _color;
            this._nextDoor.Pair(this, _color);
        }
	
        /// <summary>
        /// 
        /// </summary>
        private void Update () {
        }

        /// <inheritdoc />
        /// <summary>
        /// Called when a player goes through a door.
        /// </summary>
        public virtual void Leave(IPlayer player)
        {
            this._nextDoor.Arrive(player);
        }

        /// <inheritdoc />
        /// <summary>
        /// Called when a player arrives at a door.
        /// </summary>
        /// <param name="player"></param>
        public virtual void Arrive(IPlayer player)
        {
            this.Unlock();
            player.TeleportTo(this);
        }

        /// <inheritdoc />
        /// <summary>
        /// Calls unlock on the door knob.
        /// </summary>
        public void Unlock()
        {
            this.GetComponentInChildren<Knob>().Unlock();
        }

        /// <summary>
        /// Locks the door. May be useful, but Ill leave it for now.
        /// </summary>
        /// <param name="player"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Lock(IPlayer player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Passes the unlock command down to the knob.
        /// </summary>
        /// <param name="player"></param>
        public void Unlock(IPlayer player)
        {
            this.GetComponentInChildren<Knob>().Unlock(player);
        }

        /// <inheritdoc />
        /// <summary>
        /// Pairs doors.
        /// </summary>
        /// <param name="door"></param>
        /// <param name="c"></param>
        public virtual void Pair(IDoor door , Color c)
        {
            var curr = (IDoor) this;
            var next = door;
            IDoor tmp;

            while (!next.Equals(this) && next != null)
            {
                curr.SetColor(c);
                curr.SetNext(next);
                
                tmp = next.GetNext();
                curr = next;
                next = tmp;
            }

            if (next == null)
            {
                curr.SetNext(this);
            }
        }

        public IDoor GetNext()
        {
            return _nextDoor;
        }

        public void SetColor(Color color)
        {
            this.Color = color;
        }

        public void SetNext(IDoor next)
        {
            try
            {
                this._nextDoor = (Door) next;
            }
            catch
            {
                //TODO door is either a start or end door.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector3 GetExitLocation()
        {
            return this.gameObject.transform.position + transform.forward;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Quaternion GetExitRotation()
        {
            return this.transform.rotation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="it"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public virtual void Interact(IPlayer player, InteractionType it)
        {
            if (player.HasDoor())
            {
                switch (it)
                {
                    case InteractionType.Primary:
                        // TODO overwrite pair
                        break;
                    case InteractionType.Secondary:
                        // TODO add to pair
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("it", it, null);
                }
            }
            else
            {
                player.SetHeldDoorPair(this);
            }
            
        }

        /// <summary>
        /// Either tell the player where the next door is in 
        /// relation to them or that the door isn't paired.
        /// </summary>
        /// <returns>A string with info.</returns>
        public virtual string GetInfo()
        {
            if (this._nextDoor != null)
            {
                var dirVector = this.gameObject.transform.position - this._nextDoor.gameObject.transform.position;
                var angle = Vector2.Angle(Vector2.up, new Vector2(dirVector.x, dirVector.z));
                if (this.transform.position.x > this._nextDoor.transform.position.x) // if next door is west
                    angle = (-angle + 360) % 360; // adjust for absolute
                
                var directions = new int[] {0, 45, 90, 135, 180, 225, 270, 315};
                var closest = directions.OrderBy(dir => Math.Abs(dir % 360 - angle)).First();

                var cardinal = "a while";

                switch (closest)
                {
                    case 0:
                        cardinal = "North";
                        break;
                    case 45:
                        cardinal = "North East";
                        break;
                    case 90:
                        cardinal = "East";
                        break;
                    case 135:
                        cardinal = "South East";
                        break;
                    case 180:
                        cardinal = "South";
                        break;
                    case 225:
                        cardinal = "South West";
                        break;
                    case 270:
                        cardinal = "West";
                        break;
                    case 315:
                        cardinal = "North West";
                        break;
                }

                return string.Format("Leads to another door {0} ({1}) - {2} from here.", cardinal, this._nextDoor.name, angle);
            }
            else
            {
                return "An unpaired door.";
            }
        }
    }
}