using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes care of a particle's life cycle.
/// 
/// @author ShifatKhan
/// </summary>
public class ParticleController : MonoBehaviour
{
    public float disableTime = 3f;
    private float disableTimer;

    void Start()
    {
        disableTimer = Time.time + disableTime;
    }

    void Update()
    {
        if(Time.time > disableTimer)
        {
            DiableParticle();
        }
    }

    public void DiableParticle()
    {
        gameObject.SetActive(false);
    }
}
