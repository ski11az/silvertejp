using UnityEngine;

public class Shard : MonoBehaviour
{
    public bool isAttached = false;
    Vase parentVase = null;
    Rigidbody2D rb;
    static float maxSpeed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Limits 
        if(rb != null && rb.velocity.magnitude > maxSpeed)    
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }


    public void Attach(Vase vase)
    {
        Destroy(rb);
        isAttached = true;
        parentVase = vase;
        transform.parent = vase.transform;
    }

    /// <summary>
    /// Detaches shard from parent Vase object. Can only be used once at start of play.
    /// Rigidbody gets removed by Attach() but does not get readded by Detach().
    /// </summary>
    public void Detach()
    {
        parentVase = null;
        transform.parent = null;
    }
}
