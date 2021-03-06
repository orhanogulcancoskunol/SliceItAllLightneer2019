using _game.Scripts.Managers;
using UnityEngine;

namespace _game.Scripts.Boosters
{
    public class TrampolineBoostObject : BoostObject
    {
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            Boost(new Vector3(0, forceMultiplier, 1));
        }
    }
}
