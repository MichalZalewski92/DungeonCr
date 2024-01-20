using DungeonCrawl.Actors.Characters;
using UnityEngine;

namespace DungeonCrawl.Actors.Items
{
    public class Key : Item
    {
        public override string DefaultName => "Key";
        public override int DefaultSpriteId => 559;

        public override void OnPickup(Player player)
        {
            // Logika dla podniesienia klucza
        }
    }
}
