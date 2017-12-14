namespace Interfaces
{
    /// <summary>
    /// Contains common functions for any interactible entity.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// The function called on an 
        /// interactible entity to use it.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="it"></param>
        void Interact(IPlayer player, InteractionType it);
        
        /// <summary>
        /// Returns information about an entity.
        /// </summary>
        /// <returns>A string describing it.</returns>
        string GetInfo();
    }
}