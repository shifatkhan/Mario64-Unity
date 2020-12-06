using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script takes care of teleporting the player to a specific
/// destination after x seconds.
/// 
/// @author ShifatKhan
/// </summary>
[RequireComponent(typeof(Collider))]
public class Teleporter : MonoBehaviour
{
    // Teleport after x seconds.
    [SerializeField] private float timeToTeleport = 3f;
    private float teleportTimer;

    private bool playerIsInside = false;

    public Transform teleportDestination;

    // Gets an instance of the object to teleport.
    private GameObject objectToTeleport;

    private AudioSource teleportAudio;

    private void Start()
    {
        teleportAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(playerIsInside && Time.time > teleportTimer)
        {
            // Teleport the player.
            Teleport();
            StopTeleporter();
        }
    }

    /// <summary>
    /// Starts teleport sequence
    /// </summary>
    public void StartTeleporter()
    {
        teleportTimer = Time.time + timeToTeleport;
        playerIsInside = true;
    }

    /// <summary>
    /// Stops teleport sequence
    /// </summary>
    public void StopTeleporter()
    {
        objectToTeleport = null;
        playerIsInside = false;
    }

    /// <summary>
    /// Teleport object to destination.
    /// 
    /// TODO: Add Fade animation & sfx
    /// </summary>
    public void Teleport()
    {
        // If we are teleporting the player, we need to disable the CharacterController.
        CharacterController cc = objectToTeleport.GetComponent<CharacterController>();
        if(cc != null)
        {
            cc.enabled = false;
        }

        // Teleport.
        objectToTeleport.transform.position = teleportDestination.position;
        teleportAudio.Play();

        if (cc != null)
        {
            cc.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToTeleport = other.gameObject;
            StartTeleporter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StopTeleporter();
        }
    }
}
