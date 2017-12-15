namespace Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        float GetHealth();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        void TakeDamage(float damage);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="health"></param>
        void SetHealth(float health);
        
        /// <summary>
        /// 
        /// </summary>
        void Die();
    }
}