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
        //maxSpeed = maxSpeed + 0.5;
        Debug.Log(maxSpeed);
    }

    public void Detach()
    {
        parentVase = null;
    }
}
