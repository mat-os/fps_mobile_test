using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Bootstrapper
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunnerService
    {
        public static CoroutineRunner Instance;
        
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        private void OnDisable() => StopAllCoroutines();

        /// <summary>
        /// Добавить таймер с задержкой выполнения действия
        /// </summary>
        /// <param name="delay">Время задержки в секундах</param>
        /// <param name="action">Действие, которое нужно выполнить</param>
        public void AddTimer(float delay, Action action)
        {
            StartCoroutine(StartTimer(delay, action));
        }
        private IEnumerator StartTimer(float delay, Action action)
        {
            yield return new WaitForSeconds(delay); // Задержка
            action?.Invoke(); // Выполнение действия
        }
    }
}