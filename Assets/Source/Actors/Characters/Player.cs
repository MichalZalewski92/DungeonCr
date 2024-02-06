using UnityEngine;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Core;
using Assets.Source.Core;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UIElements;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private Item _itemOnCurrentTile;
        private List<Item> inventory = new List<Item>();
        private int attackStrength = 5;
        public new int Health = 50;
        public Vector2Int PlayerPosition { get { return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)); } }

        protected override void OnUpdate(float deltaTime)
        {
            
            bool playerMoved = false;

            if (Input.GetKeyDown(KeyCode.W))
            {
                TryMove(Direction.Up);
                playerMoved = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                TryMove(Direction.Down);
                playerMoved = true;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                TryMove(Direction.Left);
                playerMoved = true;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                TryMove(Direction.Right);
                playerMoved = true;
                
            }

            
            if (playerMoved)
            {
                
                ActorManager.Singleton.MoveSkeletons();
            }

            base.OnUpdate(deltaTime);
            
            List<Vector2Int> playerNeighbors = GetNeighbors(PlayerPosition);

            var skeletonAtPosition = ActorManager.Singleton.GetActorAt<Skeleton>(Position);
            if (skeletonAtPosition != null)//problem z uzyskaniem pozycji szkieletow przed najsciem na szkieleta
            {
                Attack(skeletonAtPosition);
                Debug.Log($"Position: {Position}");
                Debug.Log($"Skeleton at position: {skeletonAtPosition.Position}");
                Debug.Log($"Skeleton health: {skeletonAtPosition.Health}");
                Debug.Log($"Player neighbors: {string.Join(", ", playerNeighbors)}");
                //sprobowac pobrac pozycje gracza i szkieleta z actor manager pozniej zzrobic ifa i uzyc ataku jak bedzie obok
                // Atakuj szkieleta, tylko jeśli gracz jest obok
                Attack(skeletonAtPosition);
                Debug.Log(skeletonAtPosition.Health);
            }


            // Sprawdzenie, czy gracz stoi na przedmiocie
            var itemAtPosition = ActorManager.Singleton.GetActorAt<Item>(Position);
            if (itemAtPosition != null)
            {
                Debug.Log($"Position: {Position}");
                Debug.Log($"Skeleton at position: {skeletonAtPosition}");
                Debug.Log($"Player neighbors: {string.Join(", ", playerNeighbors)}");

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

        private static List<Vector2Int> GetNeighbors(Vector2Int position)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            neighbors.Add(new Vector2Int(Mathf.RoundToInt(position.x) + 1, Mathf.RoundToInt(position.y))); // Prawo
            neighbors.Add(new Vector2Int(Mathf.RoundToInt(position.x) - 1, Mathf.RoundToInt(position.y))); // Lewo
            neighbors.Add(new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y) + 1)); // Góra
            neighbors.Add(new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y) - 1)); // Dół

            return neighbors;
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
            Debug.Log("Inventory Count: " + inventory.Count);
            foreach (var item in inventory)
            {
                inventoryText += item.DefaultName + "\n";
            }

            UserInterface.Singleton.SetText(inventoryText, UserInterface.TextPosition.BottomRight); // wyświetla ekwipunek w prawym dolnym rogu
        }


        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
            
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }

}
