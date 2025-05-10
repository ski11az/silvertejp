using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    [SerializeField] Shard[] shards;

    Dictionary<Shard, Vector3> posByShard; // Stores original local positions of shards relative vase
    Dictionary<Shard, float> rotByShard; // Stores original local positions of shards relative vase

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Shard shard in shards)
        {
            Transform tf = shard.transform;

            posByShard.Add(shard, tf.localPosition);
            rotByShard.Add(shard, tf.localRotation.eulerAngles.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachShard(Shard shard)
    {

    }
}
