using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    public GameObject healthScreen;

    public Text healthTxt;

    private float invulnerabilityTime;
    private float invulTimer;
    Health playerHealth;

    void Start()
    {
        healthScreen.SetActive(false);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        invulnerabilityTime = playerHealth.invulnerabilityTime;
    }

    void Update()
    {
        if(Time.time > invulTimer)
        {
            healthScreen.SetActive(false);
        }
    }

    public void ShowHealth()
    {
        healthTxt.text = playerHealth.health.ToString();

        healthScreen.SetActive(true);
        invulTimer = Time.time + invulnerabilityTime;
    }
}
