using MoreDoors;
using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEnemy : IEntity
    {
        GameObject getTarget();
        void setState(Enemy.States state);
        Enemy.States getState();
        void setTarget(GameObject target);
        void primAttack();
        void secdAttack();
        void walkTo(Vector3 vec);
        void runTo(Vector3 vec);
    }
}