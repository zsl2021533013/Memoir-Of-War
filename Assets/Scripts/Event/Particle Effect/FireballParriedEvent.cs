using UnityEngine;

namespace Event.Particle_Effect
{
    public class FireballParriedEvent
    {
        public Transform owner;

        public FireballParriedEvent(Transform owner)
        {
            this.owner = owner;
        }
    }
}