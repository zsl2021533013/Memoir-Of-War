using UnityEngine;

namespace Event
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