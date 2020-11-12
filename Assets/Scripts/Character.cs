using UnityEngine;

abstract public class Character : MonoBehaviour
{
    [SerializeField] protected HealthBar healthBarPref;
    [SerializeField] protected Vector3 healthOffset;
    [SerializeField] protected int damage;
    [SerializeField] protected int maxHealth;
    protected HealthBar healthBar;
    protected int health;
    public bool isDead;

    protected void Setup(Vector3 barPos)
    {
        isDead = false;
        if(healthBar == null)
        {
            healthBar = Instantiate(healthBarPref, barPos, Quaternion.identity);
        }
        health = maxHealth;
        healthBar.SetInitial(maxHealth);
    }

    virtual public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
            isDead = true;
        }
        healthBar.SetHealth(health);
    }

    virtual public void Die()
    {
        healthBar.gameObject.SetActive(false);
    }


}
