using DungeonCrawl.Actors.Characters;
using UnityEngine;

namespace DungeonCrawl.Actors.Items
{
    public class Sword : Item
    {
        public override string DefaultName => "Sword";
        public override int DefaultSpriteId => 321;

        public override void OnPickup(Player player)
        {
            // Logika dla podniesienia miecza
        }
    }
}
