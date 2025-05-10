using UnityEngine;

public class Shard : MonoBehaviour
{
    public bool isAttached = false;

    [SerializeField, Tooltip("Must be defined for base shard.")]
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (parentVase) return;

        if (collision.collider.gameObject.layer != LayerMask.NameToLayer("Shard")) return; // Has to get layer through collider. gameObject has layer of root rb (Vase)

        Shard collidedShard = collision.collider.GetComponent<Shard>();
        collidedShard.parentVase.AttachShard(this);
        Attach(collidedShard.parentVase);
    }
}
