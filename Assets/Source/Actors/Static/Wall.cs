namespace DungeonCrawl.Actors.Static
{
    public class Wall : Actor
    {
        public override int DefaultSpriteId => 825;
        public override string DefaultName => "Wall";

        public override bool OnCollision(Actor anotherActor)
        {
            // Zawsze blokuj ruch, gdy inny aktor próbuje wejść na pole ściany
            return false;
        }
    }
}
