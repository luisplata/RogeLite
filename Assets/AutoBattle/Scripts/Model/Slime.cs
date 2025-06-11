using System;

namespace AutoBattle.Scripts.Core.Entities
{
    public class Slime
    {
        public string Id { get; private set; }
        public int Health { get; private set; }
        public int Attack { get; private set; }

        public Slime(string id, int health, int attack)
        {
            Id = id;
            Health = health;
            Attack = attack;
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
        }

        public bool IsAlive => Health > 0;
    }
}