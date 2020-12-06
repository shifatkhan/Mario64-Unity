using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Takes care of the scoring system.
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    static GameManager gm;
    public static int coins = 0;
    private static Text coinText;

    public static int normalCoins = 0;
    public static int redCoins = 0;

    public GameObject starPrefab;
    public static GameObject star;

    private void Awake()
    {
        star = starPrefab;
    }

    private void Start()
    {
        //coinText = GameObject.FindGameObjectWithTag("").GetComponent<Text>();
        coins = 0;
        normalCoins = 0;
        redCoins = 0;
    }

    public static void AddCoins(int amount)
    {
        if (amount == 1)
            normalCoins++;
        else if (amount == 3)
            redCoins++;

        coins += amount;

        if (coins >= 100)
            SpawnStar();
    }

    public static void RemoveCoins(int amount)
    {
        coins -= amount;

        if (coins < 0)
            coins = 0;
    }

    public static void SpawnStar()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 starPos = player.position;
        starPos.y += 3f;

        Instantiate(star, starPos, player.rotation);
    }
}
