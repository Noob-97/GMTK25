using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    // Update is called once per frame
    void Update()
    {
        
        
            body.linearVelocity = new Vector3(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

    }
}
