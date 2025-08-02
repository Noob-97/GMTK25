using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 dir;
    public float speed = 10f;
    public Rigidbody2D rb;
    private void Update()
    {
        rb.linearVelocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
