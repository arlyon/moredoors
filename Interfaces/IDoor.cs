using MoreDoors;
using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Contains common functions for doors.
    /// </summary>
    public interface IDoor
    {
        /// <summary>
        /// Called when a played goes through a door.
        /// </summary>
        void Leave(IPlayer player);

        /// <summary>
        /// Called when a player arrives at a door.
        /// </summary>
        /// <param name="player"></param>
        void Arrive(IPlayer player);

        /// <summary>
        /// Locks a door.
        /// </summary>
        /// <param name="player"></param>
        void Lock(IPlayer player);

        /// <summary>
        /// Unlocks the door.
        /// </summary>
        /// <param name="player"></param>
        void Unlock(IPlayer player);
        
        /// <summary>
        /// Unlocks the door.
        /// </summary>
        void Unlock();

        /// <summary>
        /// Pairs two doors.
        /// </summary>
        /// <param name="door"></param>
        /// <param name="c"></param>
        void Pair(IDoor door, Color c);

        /// <summary>
        /// Gets the currently linked door.
        /// </summary>
        /// <returns></returns>
        IDoor GetNext();

        void SetColor(Color color);
        void SetNext(IDoor next);
        Vector3 GetExitLocation();
        Quaternion GetExitRotation();
    }
}