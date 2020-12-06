using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A health system that keeps track of an entity's health.
/// 
/// @author ShifatKhan
/// </summary>
public class Health : MonoBehaviour
{
    [Header("Health")]
    public int health;
    private int initHealth;
    [SerializeField] private float deathTime = 0.5f;
    private float deathTimer;
    private bool dead = false;
    [Header("Visual")]
    [SerializeField] private GameObject deathFX;

    public bool invulnerabilityEnabled = false;
    public float invulnerabilityTime = 2.5f;
    private float invulTimer;
    public bool invulnerable = false;

    [SerializeField] private float blinkRate =  0.2f;
    private float blinkTimer;

    private Rigidbody rb;
    private Renderer renderer;

    [Header("Audio")]
    [SerializeField] private AudioClip[] damageAudio;
    [SerializeField] private AudioClip deathAudio;
    private AudioSource audioSource;
    private int audioIndex;

    private HealthUIScript healthUI;

    private void Start()
    {
        initHealth = health;
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<Renderer>();
        audioSource = GetComponent<AudioSource>();

        if (gameObject.CompareTag("Player"))
            healthUI = GameObject.FindGameObjectWithTag("UI").GetComponent<HealthUIScript>();
    }

    private void Update()
    {
        if(dead && Time.time > deathTimer)
        {
            Die();
        }

        // Invulnerability logic
        if (invulnerabilityEnabled)
        {
            if (invulnerable)
            {

                if (Time.time > blinkTimer)
                {
                    blinkTimer += blinkRate;
                    renderer.enabled = !renderer.enabled;
                }

                if (Time.time > invulTimer)
                {
                    invulnerable = false;
                }
            }
            else
            {
                renderer.enabled = true;
            }
        }
    }

    public void Heal(int heal)
    {
        health += heal;
    }

    public void Damage(int damage, Vector3 knockBack)
    {
        // Dont take damage if invulnerable
        if (invulnerable)
            return;

        // Decrement health
        health -= damage;

        if (healthUI != null)
            healthUI.ShowHealth();

        // Knockback
        if (rb != null)
        {
            rb.AddForce(knockBack, ForceMode.Impulse);
        }
        
        // Play hit sound
        if (audioSource != null && damageAudio.Length > 0)
        {
            audioIndex = Random.Range(0, damageAudio.Length);
            audioSource.PlayOneShot(damageAudio[audioIndex]);
        }

        // Start invulnerability
        if (invulnerabilityEnabled)
        {
            invulnerable = true;
            invulTimer = Time.time + invulnerabilityTime;

            blinkTimer = Time.time + blinkRate;
        }
        
        // Check death
        if (health <= 0)
        {
            if (audioSource != null && deathAudio != null)
            {
                audioSource.PlayOneShot(deathAudio);

            }

            deathTimer = Time.time + deathTime;
            dead = true;
        }
    }

    public void Die()
    {
        health = 0;
        Instantiate(deathFX, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    public void Resurrect()
    {
        health = initHealth;
        gameObject.SetActive(false);
        dead = false;
    }
}
