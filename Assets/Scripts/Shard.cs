using UnityEngine;

public class Shard : MonoBehaviour
{
    public bool isAttached = false;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attach()
    {
        Destroy(rb);
        isAttached = true;
    }
}
