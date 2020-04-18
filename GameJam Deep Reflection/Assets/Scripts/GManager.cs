using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GManager : MonoBehaviour {
    public GameObject[] spanwers;
    public GameObject[] enemy;
    public int enemiesToSpawn, totalSpawned, activeEnemies, dice, maxPos, maxCounter, maxIncrementor, score, eDice;
    public float spawnNext, spawnNextMax;
    public Text scoreText;

    void Start () {
        spawnNext = spawnNextMax;
    }
    void SpawnNew()
    {
        dice = Random.Range(0, spanwers.Length);
        eDice = Random.Range(0,enemy.Length);
        Instantiate(enemy[eDice], spanwers[dice].transform.position, Quaternion.identity);
        activeEnemies++;
        if (maxIncrementor >= maxCounter)
        {
            maxPos++;
            maxCounter += 4;
            maxIncrementor = 0;
        }
        maxIncrementor++;
    }
	void Update () {
        scoreText.text = "Score: "+score;
        if (activeEnemies == 0)
        {
            SpawnNew();
        }
        if (activeEnemies < maxPos)
        {
            spawnNext -= Time.deltaTime * (maxPos*0.4f);
        }
        if (activeEnemies == maxPos)
        {
            spawnNext = spawnNextMax*2;
        }
        if (spawnNext <= 0)
        {
            SpawnNew();
            spawnNext = spawnNextMax;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
 }
	}
}
