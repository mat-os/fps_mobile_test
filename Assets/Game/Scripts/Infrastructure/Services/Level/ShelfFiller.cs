using Game.Scripts.Configs;
using UnityEngine;
using Zenject;

public class ShelfFiller 
{
    private int itemsPerShelf = 5; // Количество предметов на полке
    private PrefabRepository _prefabRepository;

    [Inject]
    public ShelfFiller(PrefabRepository prefabRepository)
    {
        _prefabRepository = prefabRepository;
    }
    public void FillShelf(Shelf shelf)
    {
        // Проверяем, есть ли точки для спавна
        if (shelf.SpawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points on the shelf!");
            return;
        }

        for (int i = 0; i < itemsPerShelf && i < shelf.SpawnPoints.Length; i++)
        {
            // Выбираем случайный предмет из базы
            var randomItem = _prefabRepository.ShelfItems[Random.Range(0, _prefabRepository.ShelfItems.Length)];
            
            // Создаём префаб предмета
            GameObject spawnedItem = Object.Instantiate(randomItem.ItemPrefab, shelf.SpawnPoints[i].position, Quaternion.identity);

            // Применяем случайный цвет, если он указан
            if (randomItem.PossibleColors.Length > 0)
            {
                Color randomColor = randomItem.PossibleColors[Random.Range(0, randomItem.PossibleColors.Length)];
                Renderer renderer = spawnedItem.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = randomColor;
                }
            }

            // Делаем предмет дочерним элементом полки (для порядка в иерархии)
            spawnedItem.transform.SetParent(shelf.SpawnPoints[i]);
        }
    }
}