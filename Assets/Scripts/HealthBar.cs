using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
    
    int maxHealth;
    int health;
    public void SetInitial(int health)
    {
        maxHealth = health;
    }
    public void SetHealth(int newHealth)
    {
        health = newHealth < 0 ? 0 : newHealth;
        Vector2 barValue = healthBar.transform.localScale;
        barValue.x = (float)health / maxHealth;
        healthBar.transform.localScale = barValue;
    }
}
