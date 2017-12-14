using System;
using Interfaces;
using UnityEngine;

namespace MoreDoors
{
    public class Knob : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private bool _locked;
        
        /// <summary>
        /// 
        /// </summary>
        private void Start () {
            if (!_locked)
            {
                this.Unlock();
            }
        }
	
        /// <summary>
        /// 
        /// </summary>
        private void Update () {
		    
        }

        /// <summary>
        /// Gets the door for a door knob.
        /// </summary>
        /// <returns></returns>
        private Door GetDoor()
        {
            return this.GetComponentInParent<Door>();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Unlock(IPlayer player)
        {
            if (!player.HasKey(this.GetDoor())) return;
            player.DestroyKey(this.GetDoor());
            this.Unlock();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Unlock()
        {
            if (this._locked) return;
            this.GetComponentInChildren<Lock>().SetLocked(false);
            this._locked = false;
        }

        /// <summary>
        /// Opens the door and teleports.
        /// </summary>
        public void Interact(IPlayer player, InteractionType it)
        {
            if (this._locked && player.HasKey(this.GetDoor())) this.Unlock(player);
            else this.GetDoor().Leave(player);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets info.
        /// </summary>
        public string GetInfo()
        {
            return this._locked ? "It's locked! Find a key." : "Leads to another door.";
        }
    }
}
