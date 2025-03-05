using UniRx;
using UnityEngine;

namespace Game.Scripts.Infrastructure
{
    public class GlobalEventSystem
    {
        // глобальный MessageBroker для передачи сообщений
        private static readonly IMessageBroker _messageBroker = new MessageBroker();

        // Публичный доступ к MessageBroker
        public static IMessageBroker Broker => _messageBroker;
    }
    
    public class StartPlayLevelEvent { }
    public class PlayerPickupItemEvent { }
}