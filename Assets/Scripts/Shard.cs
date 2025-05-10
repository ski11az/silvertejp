using UnityEngine;

public class Shard : MonoBehaviour
{
    public bool isAttached = false;
    Vase parentVase = null;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Attach(Vase vase)
    {
        Destroy(rb);
        isAttached = true;
        parentVase = vase;
    }

    public void Detach()
    {
        parentVase = null;
    }
}
