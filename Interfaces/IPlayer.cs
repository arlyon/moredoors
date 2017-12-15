using MoreDoors;

namespace Interfaces
{
    public interface IPlayer : IEntity
    {
        void TeleportTo(IDoor door);
        bool HasKey(IDoor door);
        void DestroyKey(IDoor door);
        void SetHeldDoorPair(IDoor door);
        bool HasDoor();
        void AddKey(Key key);
    }
}