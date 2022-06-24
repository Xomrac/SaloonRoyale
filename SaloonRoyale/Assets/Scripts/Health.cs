using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxLife;

    public Action<int> OnHealthChanged;
    
    private int _currentLife;

    private void Awake()
    {
        _currentLife = maxLife;
    }

    public void Heal(int life)
    {
        _currentLife += life;
        _currentLife = Mathf.Clamp(_currentLife, 0, maxLife);
        OnHealthChanged?.Invoke(_currentLife);
    }

    public void Deal(int life)
    {
        _currentLife -= life;
        OnHealthChanged?.Invoke(_currentLife);
    }

    public int GetCurrentLife()
    {
        return _currentLife;
    }
}
