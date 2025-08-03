using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovment : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    public bool IsTouchingGround;
    bool onDoor;
    bool endingDoor;
    bool onPortal;
    public Transform repos;
    public UnityEvent end;
    public int WorldThreshold = -40;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    public void ReposPlayer()
    {
        transform.position = repos.position;
    }
    void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsTouchingGround)
        {
            print("jump");
            body.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.E) && onDoor)
        {
            if (endingDoor)
            {
                GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Play();
                end.Invoke();
                Destroy(gameObject);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && onPortal)
        {
            GameObject.FindGameObjectWithTag("portal").GetComponent<ChangeDimension>().DimensionChange();
        }

        if (transform.position.y <= WorldThreshold)
        {
            SimulateLoading.NextScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Loading");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            if (collision.GetComponent<OpenDoor>().openedFlag)
            {
                onDoor = true;
                if (collision.GetComponent<OpenDoor>().EndingDoor)
                {
                    endingDoor = true;
                }
            }
        }
        if (collision.gameObject.CompareTag("portal"))
        {
            onPortal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            if (collision.GetComponent<OpenDoor>().openedFlag)
            {
                onDoor = false;
            }
        }
        if (collision.gameObject.CompareTag("portal"))
        {
            onPortal = false;
        }
    }
}
