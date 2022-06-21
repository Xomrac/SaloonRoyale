using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentLife;
    [SerializeField] private int maxLife;

    public void Heal(int life)
    {
        currentLife += life;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
    }

    public void Deal(int life)
    {
        currentLife -= life;
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }
}
