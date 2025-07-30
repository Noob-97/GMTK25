using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            body.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsTouchingGround = true;
        }
        if (collision.gameObject.CompareTag("FP"))
        {
            if (PlayerPrefs.HasKey("Phase"))
            {
                int phase = PlayerPrefs.GetInt("Phase");
                phase++;
                PlayerPrefs.SetInt("Phase", phase);
            }
            else
            {
                PlayerPrefs.SetInt("Phase", 1);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
