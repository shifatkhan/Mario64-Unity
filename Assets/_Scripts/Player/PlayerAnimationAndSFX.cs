using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes care of general animation for Mario.
/// 
/// @author ShifatKhan
/// @author ShifatKhan
/// </summary>
public class PlayerAnimationAndSFX : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator animator;

    private PlayerMovement playerMovement;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] punch;
    [SerializeField] private AudioClip[] jump;

    private int audioIndex;

    public bool isPunching { get; private set; }

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Punch
        if (Input.GetButtonDown("Fire1") && playerMovement.isGrounded && !isPunching)
        {
            isPunching = true;
        }

        // Update animator
        animator.SetBool("Jump", !playerMovement.isGrounded);
        animator.SetBool("Run", playerMovement.movement != Vector3.zero);
        animator.SetBool("Punch", isPunching);

        if (Input.GetButton("Jump") && playerMovement.isGrounded && !audioSource.isPlaying)
        {
            audioIndex = Random.Range(0, jump.Length);

            audioSource.loop = false;
            audioSource.PlayOneShot(jump[audioIndex]);
        }
        else if (isPunching && !audioSource.isPlaying)
        {
            audioIndex = Random.Range(0, punch.Length);

            audioSource.loop = false;
            audioSource.PlayOneShot(punch[audioIndex]);
        }
    }

    /// <summary>
    /// Called at the last frame of the punch animation.
    /// This is to avoid spamming.
    /// </summary>
    public void FinishPunchAnimation()
    {
        isPunching = false;
    }
}
