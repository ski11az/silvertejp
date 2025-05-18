using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static event Action<int> ScoreChanged;
    public static event Action AllShardsUsed;

    [SerializeField] Vase[] vasePrefabs;
    [SerializeField] DeliveryBox deliveryBox;
    [SerializeField] ShatterBox shatterBox;

    [SerializeField] Shard[] junk;

    [SerializeField] float spawnHeight = 5;
    [SerializeField] float spawnOffsetScaler = 1;

    Vase currentVase;
    List<Shard> currentShards;

    float timeOfLastShardSpawn = 0;

    float spawnDelay = 5.0f;
    int score = 0;

    private void OnEnable()
    {
        deliveryBox.VaseDelivered += IncreaseScore;
        shatterBox.VaseDestroyed += StartNewVase;
    }

    private void OnDisable()
    {
        deliveryBox.VaseDelivered -= IncreaseScore;
        shatterBox.VaseDestroyed -= StartNewVase;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartNewVase();
        ScoreChanged?.Invoke(score);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        TrySpawnShard();
    }

    private void IncreaseScore(int value)
    {
        score += value;
        ScoreChanged?.Invoke(score);
        StartNewVase();
        spawnDelay *= 0.90f;
    }

    private void StartNewVase()
    {
        Vase vaseToSpawn = vasePrefabs[Random.Range(0, vasePrefabs.Length)];
        currentVase = Instantiate(vaseToSpawn, new Vector3(0, spawnHeight, 0), Quaternion.identity);
        PrepareShards();
        timeOfLastShardSpawn = Time.time;
    }

    private void PrepareShards()
    {
        currentShards = currentVase.GetShards();

        foreach (Shard shard in currentShards)
        {
            shard.Detach();
            //shard.gameObject.SetActive(false); // Could cause problems with execution order with Vase filling dictionaries
        }
    }

    private void TrySpawnShard()
    {
        if (Time.time < timeOfLastShardSpawn + spawnDelay) return;

        timeOfLastShardSpawn = Time.time;

        if (currentShards.Count <= 0)
        {
            AllShardsUsed?.Invoke();
            return;
        }

        Vector3 spawnPos = new(spawnOffsetScaler * Random.Range(-1.0f, 1.0f), spawnHeight, 0);

        if (Random.Range(0.0f, 1.0f) > 0.2f)
        {
            Shard spawnedShard = currentShards[0];
            spawnedShard.transform.position = spawnPos;
            spawnedShard.gameObject.SetActive(true);
            currentShards.RemoveAt(0);
        }
        else
        {
            Instantiate(junk[Random.Range(0, junk.Length)], spawnPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }

    }
}
