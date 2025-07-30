using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    public bool IsTouchingGround;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && IsTouchingGround)
        {
            print("jump");
            body.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsTouchingGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsTouchingGround = false;
        }
    }
}
