using System;
using _game.Scripts.Managers;
using UnityEngine;

namespace _game.Scripts.Boosters
{
    public abstract class BoostObject : MonoBehaviour
    {
        [SerializeField] protected float forceMultiplier = 4;
        protected virtual void OnTriggerEnter(Collider other)
        {
            GetComponent<Collider>().enabled = false;
        }

        protected static void Boost(Vector3 val)
        {
            var force = CharacterManager.Instance.CurrentCharacter.GetStaticForceForHit();
            CharacterManager.Instance.CurrentCharacter.Jump(new Vector3(force.x*val.x,force.y*val.y,force.z*val.z));
        }
    }
}
