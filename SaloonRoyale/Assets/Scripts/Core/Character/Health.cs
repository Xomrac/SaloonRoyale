using UnityEngine;

namespace Core.Character
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int currentLife;
        [SerializeField] private int maxLife;

        public void Heal(int life)
        {
            currentLife = life;
        }

        public void Deal(int life)
        {
            currentLife = life;
        }

        public int GetCurrentLife()
        {
            return currentLife;
        }
    }
}
