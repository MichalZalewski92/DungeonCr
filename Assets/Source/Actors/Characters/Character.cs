using DungeonCrawl.Core;
using System.Diagnostics;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Character : Actor
    {
        public int Health { get; private set; }


        public void Attack(Character target)
        {
            target.ApplyDamage(5);
            
        }

        public void ApplyDamage(int damage)
        {
            Health -= damage;
           
            

            if (Health <= 0)
            {
                
                OnDeath();

                ActorManager.Singleton.DestroyActor(this);
            }
        }

        protected abstract void OnDeath();

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
