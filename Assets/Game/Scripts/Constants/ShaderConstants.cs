using UnityEngine;

namespace Game.Scripts.Constants
{
    public static class ShaderConstants
    {
        public static readonly int OUTLINE_WIDTH = Shader.PropertyToID("_OutlineWidth1");
        public static readonly int COLOR = Shader.PropertyToID("_Color");
    }
}