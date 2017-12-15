using Interfaces;
using UnityEngine;

namespace MoreDoors
{
    public class EndDoor : Door
    {
        private void Start()
        {
            base.Color = Color.white;
        }

        public override void Leave(IPlayer player)
        {
            this.GetComponent<AudioSource>().Play();
            Debug.Log("Win");
        }

        public override void Arrive(IPlayer player)
        {
            //TODO error
        }

        public override void Pair(IDoor door, Color c)
        {
            return;
            // TODO error sound
        }

        public override void Interact(IPlayer player, InteractionType it)
        {
            // TODO WIN
        }

        public override string GetInfo()
        {
            return "Get through this door to win!";
        }
    }
}