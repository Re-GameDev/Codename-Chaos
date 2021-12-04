using System;
using UnityEngine;

namespace RageBall 
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealthPoints = 100;
        int points = 100;
        public delegate void HealthEventHandler( object sender );
        public event HealthEventHandler OnHealthChanged;
        public event HealthEventHandler OnHealthDepleted;
        public GameEvent PlayerDeathEvent;

        void OnEnable()
        {
            points = maxHealthPoints;
        }

        public void TakeDamage( int damage  )
        {
            points -= damage;
            points = Mathf.Max( points, 0 );
            if( points == 0 )
            {
                OnHealthDepleted?.Invoke(this);
                PlayerDeathEvent?.Invoke();
            }
            else
                OnHealthChanged?.Invoke(this);
        }

        public void RestoreHealth( int regain )
        {
            points += regain;
            points = Mathf.Min( points, maxHealthPoints );
        }
    }
}
