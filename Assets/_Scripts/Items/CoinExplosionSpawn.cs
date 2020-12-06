using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns coins on start.
/// 
/// @author ShifatKhan
/// </summary>
public class CoinExplosionSpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    public float force = 1f;

    private Vector3 direction;

    public void Spawn(int numberOfCoins)
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            direction = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
            print(direction);
            Coin coin = Instantiate(prefabToSpawn, transform.position, transform.rotation).GetComponent<Coin>();
            coin.AddForceOnSpawn(direction * force);
        }
    }
}
