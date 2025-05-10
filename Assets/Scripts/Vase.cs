using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    [SerializeField] Shard[] shards;

    int shardToAttach = 0;

    [SerializeField] float posMargin = 0.1f;
    [SerializeField] float rotMargin = 10.0f;

    Dictionary<Shard, Vector3> posByShard = new(); // Stores original local positions of shards relative vase
    Dictionary<Shard, float> rotByShard = new(); // Stores original local ritations of shards relative vase

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
        if (Input.GetMouseButtonDown(0))
        {
            if (AttachShard(shards[shardToAttach]))
            {
                shardToAttach++;
            }
        }
    }

    public bool AttachShard(Shard shard)
    {
        Transform shardTf = shard.transform;

        Vector3 destPos = posByShard[shard];
        float destRot = rotByShard[shard];

        if (CalcPosDiff(shardTf.localPosition, destPos) < posMargin &&
            CalcRotDiff(shardTf.localRotation.eulerAngles.z, destRot) < rotMargin)
        {
            shard.Deactivate();
            shardTf.SetLocalPositionAndRotation(destPos, Quaternion.Euler(0, 0, destRot));

            return true;
        }

        return false;
    }
    
    float CalcPosDiff(Vector3 current, Vector3 target)
    {
        return Vector3.Magnitude(target - current);
    }

    float CalcRotDiff(float current, float target)
    {
        float diff = Mathf.Abs(target - current);
        if (diff > 180) diff = 360 - diff;

        return diff;
    }
}
