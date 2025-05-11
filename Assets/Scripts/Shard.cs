using UnityEngine;

public class Shard : MonoBehaviour
{
    [SerializeField] AudioClip collisionSound;
    [SerializeField] AudioClip tapeSound;
    [SerializeField] GameObject ductTapePrefab;

    public bool isAttached = false;

    [SerializeField, Tooltip("Must be defined for base shard.")]
    Vase parentVase = null;
    Rigidbody2D rb;
    static float maxSpeed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y < -20.0f) Destroy(gameObject);
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

        AudioManager.Instance.PlayClip(collisionSound);
        AudioManager.Instance.PlayClip(tapeSound);

        Shard collidedShard = collision.collider.GetComponent<Shard>();

        collidedShard.parentVase.AttachShard(this);
        Attach(collidedShard.parentVase);

        Vector3 pos1 = transform.position;
        Vector3 pos2 = collidedShard.transform.position;

        Vector3 midpoint = (pos1 + pos2) * 0.5f;
        float dist = (pos2 - pos1).magnitude;
        Vector3 right = (pos2 - pos1).normalized;

        GameObject spawnedTape = Instantiate(ductTapePrefab, parentVase.transform);
        spawnedTape.transform.position = collision.contacts[0].point;
        spawnedTape.transform.right = right;
        //spawnedTape.transform.localScale = new Vector3(.35f, 1, 1);
    }
}
