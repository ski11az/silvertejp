using UnityEngine;

public class Shard : MonoBehaviour
{
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

    public void Deactivate()
    {
        Destroy(rb);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
