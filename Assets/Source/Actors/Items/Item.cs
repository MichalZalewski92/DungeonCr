using DungeonCrawl.Actors.Characters;
using UnityEngine;

namespace DungeonCrawl.Actors.Items
{
    public abstract class Item : Actor
    {
     

        // Metoda wywoływana, gdy gracz wejdzie na pole z przedmiotem
        public abstract void OnPickup(Player player);
    }
}
