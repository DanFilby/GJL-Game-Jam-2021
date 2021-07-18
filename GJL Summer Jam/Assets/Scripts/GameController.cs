using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject enemies;

    private static int numEnemies;


    void Start()
    {
        numEnemies = enemies.transform.childCount;
    }

    public static void KillAnEnemy()
    {
        Debug.Log($"{numEnemies}");
        if(--numEnemies <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
