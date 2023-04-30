using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public bool damaged;
    private bool canBeDamaged;
    public float damageTimer = 5f;

    public Health_slider healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if (damaged && canBeDamaged)
        {
            TakeDamage(20);
            damaged = false;
        }

        if (!canBeDamaged & damageTimer <= 0f)
        {
            DamageReset();
        }

        damageTimer -= Time.deltaTime;

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.setHealth(currentHealth);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("HitBox"))
        {
            damaged = true;
            canBeDamaged = false;
        }
    }

    private void DamageReset()
    {
        canBeDamaged = true;
        damageTimer = 5f;
    }
}
