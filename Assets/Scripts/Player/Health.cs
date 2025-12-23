using UnityEngine;

namespace Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100;
        [SerializeField] private float maxHealth = 100;

        void Start()
        {
            ResetHealth();
        }
        
        public void ResetHealth()
        {
            health = maxHealth;
        }
        
        public void Kill()
        {
            health = 0;
            this.gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
        }
    }
}