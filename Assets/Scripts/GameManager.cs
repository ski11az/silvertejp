using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] Vase vase;
    [SerializeField] float clickRadius = 1.0f;

    List<Shard> shards;

    Plane gamePlane = new(Vector3.forward, Vector3.zero);

    float timeOfLastSpawn = 0;
    int shardToSpawn = 0;
    [SerializeField] float spawnHeight = 5;
    [SerializeField] float spawnOffsetScaler = 1;

    // Start is called before the first frame update
    void Start()
    {
        PrepareShards();
    }

    private void PrepareShards()
    {
        shards = vase.GetShards();

        foreach (Shard shard in shards)
        {
            shard.Detach();
            shard.gameObject.SetActive(false); // Could cause problems with execution order with Vase filling dictionaries
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        if (shardToSpawn < shards.Count && Time.time > timeOfLastSpawn + 5)
        {
            Shard spawnedShard = shards[shardToSpawn];
            spawnedShard.transform.position = new Vector3(spawnOffsetScaler * Random.Range(-1.0f, 1.0f), spawnHeight, 0);
            spawnedShard.gameObject.SetActive(true);
            shardToSpawn++;
            timeOfLastSpawn = Time.time;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    TryAttachShard();
        //}
    }

    private void TryAttachShard()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        gamePlane.Raycast(mouseRay, out float rayDist);
        Vector3 hitPos = mouseRay.GetPoint(rayDist);
        Collider2D[] cols = Physics2D.OverlapCircleAll(hitPos, clickRadius, 1 << LayerMask.NameToLayer("Shard"));

        Shard fallingShard = null;
        Shard closestShard = null;
        float closestDist = 2 * clickRadius; // Set arbitrary starting distance to compare first shard with

        foreach (Collider2D col in cols)
        {
            Shard currentShard = col.GetComponent<Shard>();
            float currentDist = Vector3.Magnitude(currentShard.transform.position - hitPos);

            // Find 1 falling shard and 1 attached shard in clicked region
            if (currentShard.isAttached == false) fallingShard = currentShard;
            else if (currentDist < closestDist)
            {
                closestDist = currentDist;
                closestShard = currentShard;
            }
        }

        if (!fallingShard || !closestShard) return; // Stop if either is not found

        vase.AttachShard(fallingShard);
    }
}
