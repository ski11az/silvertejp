using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static event Action<int> ScoreChanged;

    [SerializeField] Vase vasePrefab;
    [SerializeField] DeliveryBox deliveryBox;
    [SerializeField] ShatterBox shatterBox;

    [SerializeField] float clickRadius = 1.0f;

    [SerializeField] float spawnHeight = 5;
    [SerializeField] float spawnOffsetScaler = 1;

    Vase currentVase;
    List<Shard> currentShards;

    float timeOfLastShardSpawn = 0;

    private int score = 0;

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

    private void IncreaseScore(int value)
    {
        score += value;
        ScoreChanged?.Invoke(score);
        StartNewVase();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartNewVase();
        ScoreChanged?.Invoke(score);
    }

    private void StartNewVase()
    {
        currentVase = Instantiate(vasePrefab, new Vector3(0, spawnHeight, 0), Quaternion.identity);
        PrepareShards();
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        TrySpawnShard();
    }

    private void TrySpawnShard()
    {
        if (currentShards.Count > 0 && Time.time > timeOfLastShardSpawn + 5)
        {
            Shard spawnedShard = currentShards[0];
            spawnedShard.transform.position = new Vector3(spawnOffsetScaler * Random.Range(-1.0f, 1.0f), spawnHeight, 0);
            spawnedShard.gameObject.SetActive(true);
            timeOfLastShardSpawn = Time.time;
            currentShards.RemoveAt(0);
        }
    }
}
