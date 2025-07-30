using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float speed;
    public  Rigidbody2D body;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
    }
}
