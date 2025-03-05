using Game.Scripts.Configs;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Configs;
using Game.Scripts.Constants;
using Game.Scripts.Items;

public class ShelfFillerService 
{
    private readonly PrefabRepository _prefabRepository;
    private ShelfConfig _shelfConfig;

    [Inject]
    public ShelfFillerService(PrefabRepository prefabRepository, GameConfig gameConfig)
    {
        _shelfConfig = gameConfig.ShelfConfig;
        _prefabRepository = prefabRepository;
    }

    public void FillShelf(ShelfView shelf)
    {
        // Проверяем, есть ли точки для спавна
        if (shelf.SpawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points on the shelf!");
            return;
        }

        // Создаём список доступных точек для спавна
        List<Transform> availableSpawnPoints = new List<Transform>(shelf.SpawnPoints);

        for (int i = 0; i < _shelfConfig.ItemsInShelfCount && availableSpawnPoints.Count > 0; i++)
        {
            // Выбираем случайную точку из доступных
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[randomIndex];

            // Удаляем использованную точку из списка
            availableSpawnPoints.RemoveAt(randomIndex);

            // Выбираем случайный предмет из базы
            var randomItem = _prefabRepository.ShelfItems[Random.Range(0, _prefabRepository.ShelfItems.Length)];
            
            // Создаём префаб предмета
            GameObject spawnedItem = Object.Instantiate(randomItem.ItemPrefab.gameObject, spawnPoint.position, Quaternion.identity);

            // Применяем случайный цвет, если он указан
            SetRandomColor(randomItem, spawnedItem);

            // Делаем предмет дочерним элементом полки (для порядка в иерархии)
            spawnedItem.transform.SetParent(spawnPoint);
        }
    }

    private static void SetRandomColor(ShelfItemData randomItem, GameObject spawnedItem)
    {
        if (randomItem.PossibleColors.Length > 0)
        {
            Color randomColor = randomItem.PossibleColors[Random.Range(0, randomItem.PossibleColors.Length)];
            ItemBase itemBase = spawnedItem.GetComponent<ItemBase>();
            if (itemBase != null)
            {
                itemBase.Renderer.material.SetColor(ShaderConstants.COLOR, randomColor); 
            }
        }
    }
}
