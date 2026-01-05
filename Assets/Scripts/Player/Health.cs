using UnityEngine;

namespace Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int hearts = 3;
        [SerializeField] private int maxHearts = 3;

        void Start()
        {
            ResetHealth();
        }
        
        void ResetHealth()
        {
            hearts = maxHearts;
        }
        
        public void Kill()
        {
            OnHit();
            if (hearts > 0)
            {
                this.gameObject.SendMessage("OnFallen", SendMessageOptions.DontRequireReceiver);
            } else
            {
                this.gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
                ResetHealth();
            }
        }
        
        void OnHit()
        {
            hearts --;
        }

        public int Hearts
        {
            get { return hearts; }
        }

        public int MaxHearts
        {
            get { return maxHearts; }
        }
    }
}