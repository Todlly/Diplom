using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface IHavingHealth
    {
    //    private int Health { get; set; }

        void GetDamage(int dagameAmount);
        void Heal(int healAmount);
        void Die();

    }
}
