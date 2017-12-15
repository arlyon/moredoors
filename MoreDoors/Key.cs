using System;
using Interfaces;
using UnityEngine;

namespace MoreDoors
{
    public class Key : MonoBehaviour, IInteractable
    {
        private int state;
        private Vector3 startPos;
        
        private void Start()
        {
            this.startPos = this.transform.position;
        }

        private void Update()
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, state, 0));
            this.transform.position = startPos + Vector3.up * (float) (0.2 + 0.2 * Math.Sin((float) state / 50));
            state += 1;
        }

        public void Interact(IPlayer player, InteractionType it)
        {
            player.AddKey(this);
            Destroy(this.gameObject);
        }

        public string GetInfo()
        {
            return "A key.";
        }
    }
}