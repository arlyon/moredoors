﻿using Interfaces;
using UnityEngine;

namespace MoreDoors
{
    public class StartDoor : Door
    {
        private void Start()
        {
            base.Color = Color.black;
        }
        
        public override void Pair(Door door, Color c)
        {
            return;
        }

        public override void Interact(IPlayer player, InteractionType it)
        {
            return;
        }

        public override string GetInfo()
        {
            return "This is where you started!";
        }
    }
}