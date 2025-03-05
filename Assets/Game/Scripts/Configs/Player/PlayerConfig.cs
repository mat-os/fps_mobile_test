using System;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/" + nameof(PlayerConfig))]
[InlineEditor]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public MovementConfig MovementConfig { get; private set; }
    [field: SerializeField] public ItemPickupConfig ItemPickupConfig { get; private set; }
}

[Serializable]
public class ItemPickupConfig
{
    [field: SerializeField] public float PickupDistance { get; private set; }
    [field: SerializeField] public float ThrowForceForward { get; private set; }
    [field: SerializeField] public float ThrowForceVertical { get; private set; }

}
[Serializable]
public class MovementConfig
{
    [field:Header("Move")]
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float Gravity { get; private set; } = 9.8f;
    
    [field:Header("Aim")]
    [field: SerializeField] public float LookSensitivity { get; private set; } = 9.8f;
}