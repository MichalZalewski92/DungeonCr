using UnityEngine;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Core;
using Assets.Source.Core;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private Item _itemOnCurrentTile;
        private List<Item> inventory = new List<Item>();

        protected override void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }

            base.OnUpdate(deltaTime);

            // Sprawdzenie, czy gracz stoi na przedmiocie
            var itemAtPosition = ActorManager.Singleton.GetActorAt<Item>(Position);
            if (itemAtPosition != null)
            {
                _itemOnCurrentTile = itemAtPosition;
                ShowPickupMessage(true); // Funkcja do wyświetlania wiadomości
            }
            else
            {
                _itemOnCurrentTile = null;
                ShowPickupMessage(false);
            }

            // Obsługa naciśnięcia klawisza E
            if (Input.GetKeyDown(KeyCode.E) && _itemOnCurrentTile != null)
            {
                PickupItem(_itemOnCurrentTile);
            }
        }

        private void ShowPickupMessage(bool show)
        {
            if (show)
            {
                UserInterface.Singleton.SetText("Naciśnij E, aby podnieść", UserInterface.TextPosition.BottomRight);
            }
            else
            {
                UserInterface.Singleton.SetText("", UserInterface.TextPosition.BottomRight); // Ukryj tekst
            }
        }


        private void PickupItem(Item item)
        {
            inventory.Add(item); // Dodanie przedmiotu do ekwipunku
            Debug.Log(inventory);
            UpdateInventoryUI(); // Aktualizacja UI ekwipunku
            ActorManager.Singleton.DestroyActor(item); // Usunięcie przedmiotu z mapy
            _itemOnCurrentTile = null;
        }

        private void UpdateInventoryUI()
        {
            string inventoryText = "Ekwipunek:\n";
            foreach (var item in inventory)
            {
                inventoryText += item.DefaultName + "\n";
            }

            UserInterface.Singleton.SetText(inventoryText, UserInterface.TextPosition.BottomRight); // wyświetla ekwipunek w prawym dolnym rogu
        }


        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
