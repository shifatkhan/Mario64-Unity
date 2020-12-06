using UnityEngine;
using System.Collections;

/// <summary>
/// Class allows the developer to edit the way points with localWaypoints. Once the game starts, we copy these localWaypoints 
/// to a globalWaypoints since we don't want the points to move with the platform moving (or else the platform will keep moving.)
/// 
/// @author ShifatKhan
/// </summary>
public class Platform : MonoBehaviour
{
    [SerializeField]
    private Vector3[] localWaypoints; // Set in the editor
    private Vector3[] globalWaypoints;

    [SerializeField] private float speed = 1.5f;
    public int currentWaypointIndex;

    private void Start()
    {
        // Make a deep copy by offsetting the vectors and making it global
        globalWaypoints = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }

        currentWaypointIndex = 0;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, globalWaypoints[currentWaypointIndex]) <= 0.3) // 0.3 is a fail-safe method to prevent the platform to go through the way point. Can be resolved with FixedUpdate.
        {
            currentWaypointIndex = ++currentWaypointIndex % globalWaypoints.Length; // Allows index to loop back to 0.
        }

        transform.Translate((globalWaypoints[currentWaypointIndex] - transform.position).normalized * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.transform.SetParent(transform);
        }
    }


    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.transform.SetParent(null);
        }
    }

    /** Debugging function that draws the waypoints gizmos for debugging.
     */
    void OnDrawGizmos()
    {
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = .3f;

            for (int i = 0; i < localWaypoints.Length; i++)
            {
                // When the game is not running, the waypoints will follow the platform (since we want to drag and drop)
                // If the game is running, the waypoints should stay in place.
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;

                // Draw a '+' to show the points.
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }
}
