using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    [SerializeField] List<Shard> shards;

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

    public void AttachShard(Shard shard)
    {
        Transform shardTf = shard.transform;

        Vector3 destPos = posByShard[shard];
        float destRot = rotByShard[shard];

        if (CalcPosDiff(shardTf.localPosition, destPos) < posMargin &&
            CalcRotDiff(shardTf.localRotation.eulerAngles.z, destRot) < rotMargin)
        {
            shardTf.SetLocalPositionAndRotation(destPos, Quaternion.Euler(0, 0, destRot));
        }
        
        shard.Attach();
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

    public List<Shard> GetShards() // NOTE: May need to clear vase's list when detaching so this should maybe return copy
    {
        return shards;
    }
}
