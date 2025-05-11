using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class Vase : MonoBehaviour
{
    [SerializeField] List<Shard> shards; // Contains shards belonging to this vase

    [SerializeField] float posMargin = 0.1f;
    [SerializeField] float rotMargin = 10.0f;

    Dictionary<Shard, Vector3> posByShard = new(); // Stores original local positions of shards relative vase
    Dictionary<Shard, float> rotByShard = new(); // Stores original local ritations of shards relative vase
    
    List<Shard> attachedShards = new(); // Contains shards currently attached to the vase during play

    [SerializeField] int score = 0;
    public static event Action ScoreEvent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InitializeShards();
    }

    private void InitializeShards()
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
        // Must be attached first to get correct localTranform values
        attachedShards.Add(shard);
        shard.Attach(this);

        Transform shardTf = shard.transform;

        Vector3 destPos = posByShard[shard];
        float destRot = rotByShard[shard];

        float posDelta = CalcPosDiff(shardTf.localPosition, destPos); // This can also be used for scoring
        float rotDelta = CalcRotDiff(shardTf.localRotation.eulerAngles.z, destRot); // This can also be used for scoring

        // Check if perfect attach
        if (posDelta < posMargin && rotDelta < rotMargin)
        {
            shardTf.SetLocalPositionAndRotation(destPos, Quaternion.Euler(0, 0, destRot));
            posDelta = 0;
            rotDelta = 0;
            score = score + 2;
        }
        // Normal score is +1, perfect is 3 = 2 + 1;
        score = score + 1;
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

    /// <summary>
    /// Returns the score of the vase with some random tip
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return score * 10 + UnityEngine.Random.Range(0, 10);
    }

    /// <summary>
    /// Returns a copy of the list containing attached Shards.
    /// </summary>
    /// <returns></returns>
    public List<Shard> GetShards() // NOTE: May need to clear vase's list when detaching so this should maybe return copy
    {
        return new List<Shard>(shards);
    }
}
