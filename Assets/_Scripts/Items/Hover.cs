using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Small script that makes an object hover.
/// 
/// @author ShifatKhan
/// </summary>
public class Hover : MonoBehaviour
{
    public float height = 1f;

    private void Start()
    {
    }

    void Update()
    {
        // TODO: Fix hover
        Vector3 pos = new Vector3(transform.position.x, transform.position.y * Mathf.Pow(Mathf.Sin(Time.time), 2) * height, transform.position.z);
        this.transform.position = pos;
    }
}
