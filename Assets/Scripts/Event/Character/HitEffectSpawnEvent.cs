using UnityEngine;

namespace Event.Character
{
    public class HitEffectSpawnEvent
    {
        public Vector3 pos;

        public HitEffectSpawnEvent(Vector3 pos)
        {
            this.pos = pos;
        }
    }
}