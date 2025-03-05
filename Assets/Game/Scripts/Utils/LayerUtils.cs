using UnityEngine;

namespace Game.Scripts.Utils
{
    public static class LayerUtils
    {
        /// <summary>
        /// Меняет слой у объекта и всех его дочерних объектов.
        /// </summary>
        public static void SetLayerRecursively(GameObject obj, int layer)
        {
            if (obj == null) return;

            obj.layer = layer; // Меняем слой у самого объекта
            
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer); // Рекурсивно меняем у всех детей
            }
        }
    }
}