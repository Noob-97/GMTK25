using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;
[Serializable]
public enum EntranceType
{
    Spawn,
    SceneSwitch
}
[Serializable]
public enum InteractableType
{
    PressE,
    Auto
}

[Serializable]
public class Entrance
{
    public Vector2 Position;
    public Direction Direction;
    public EntranceType Type;
    public string SceneToGo;
    public Collider2D Collider;
    public string ID;
}
[Serializable]
public class Interactable
{
    public InteractableType Type;
    public Collider2D Collider;
    public UnityEvent OnInteract;
}
public enum Direction
{
    Idle,
    Up,
    Down,
    Left,
    Right
}
public class PlayerMovement : MonoBehaviour
{
    [Header("PropertyHell")]
    public int speed;
    private Vector2 vel;
    private Rigidbody2D rb;
    [NonSerialized] public Vector2 direction;
    private Animator panelanimator;
    private Animator playeranimator;
    private string currentState;
    private string Orientation;
    [NonSerialized] public Animator JoyStickAnimator;
    [NonSerialized] public Animator ButtonEAnimator;
    private GameObject MobileControls;
    [SerializeField] public static string GoingTo;
    [SerializeField] public Entrance[] Entrances;
    [SerializeField] public Interactable[] Interactables;
    public GameObject upref;
    public GameObject downref;
    public GameObject leftref;
    public GameObject rightref;
    public GameObject bullet;
    public static int kills;
    [Header("Debug")]
    public bool MobileControlsOverride;
    public bool BlockInput;
    public bool WaitingForE;
    public bool EFired;
    void Start()
    {
        SetComponents();
    }
    public void SetComponents()
    {
        // Player Movement
        rb = GetComponent<Rigidbody2D>();
        CheckMovVel();
        playeranimator = gameObject.GetComponent<Animator>();
        // Finding & Locating Entrances
        if (GoingTo == null)
        {
            GoingTo = "Init";
        }
        // Set transform.position to Correspondant Entrance
        for (int i = 0; i < Entrances.Length; i++)
        {
            if (Entrances[i].ID == GoingTo && Entrances[i].Type == EntranceType.Spawn)
            {
                transform.position = Entrances[i].Position;
                if (Entrances[i].Direction != Direction.Idle)
                {
                    ChangeAnimation(Entrances[i].Direction.ToString() + "Idle");
                }
            }
        }
    }

    // Player Movement
    public void CheckMovVel()
    {
        vel = new Vector2(speed, speed);
    }
    public void GetMovement(CallbackContext context)
    {
        if (context.canceled)
        {
            direction = Vector2.zero;
            if (Orientation == "Up")
            {
                ChangeAnimation("UpIdle");
            }
            if (Orientation == "Left")
            {
                ChangeAnimation("LeftIdle");
            }
            if (Orientation == "Down")
            {
                ChangeAnimation("DownIdle");
            }
            if (Orientation == "Right")
            {
                ChangeAnimation("RightIdle");
            }
        }
        if (context.performed)
        {
            if (BlockInput == false)
            {
                direction = context.ReadValue<Vector2>();
                if (direction == new Vector2(0, 1))
                {
                    ChangeAnimation("Up");
                    Orientation = "Up";
                    GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
                if (direction == new Vector2(1, 0))
                {
                    ChangeAnimation("Right");
                    Orientation = "Right";
                }
                if (direction == new Vector2(0, -1))
                {
                    ChangeAnimation("Down"); 
                    Orientation = "Down";
                    GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
                if (direction == new Vector2(-1, 0))
                {
                    ChangeAnimation("Left");
                    Orientation = "Left";
                }
            }
        }
    }
    
    public void GetSHOOT(CallbackContext context)
    {
        if (context.performed)
        {
            if (BlockInput == false)
            {
                Vector2 dir = context.ReadValue<Vector2>();
                if (dir == Vector2.up)
                {
                    GameObject obj = Instantiate(bullet, upref.transform.position, Quaternion.identity);
                    obj.GetComponent<Bullet>().dir = dir;
                }
                if (dir == Vector2.down)
                {
                    GameObject obj = Instantiate(bullet, downref.transform.position, Quaternion.identity);
                    obj.GetComponent<Bullet>().dir = dir;

                }
                if (dir == Vector2.left)
                {
                    GameObject obj = Instantiate(bullet, leftref.transform.position, Quaternion.identity);
                    obj.GetComponent<Bullet>().dir = dir;

                }
                if (dir == Vector2.right)
                {
                    GameObject obj = Instantiate(bullet, rightref.transform.position, Quaternion.identity);
                    obj.GetComponent<Bullet>().dir = dir;

                }
            }
        }
    }

    public static void IncreaseKills()
    {
        kills++;
        if (kills == 11)
        {
            Destroy(GameObject.FindGameObjectWithTag("11KILLS"));
        }
    }

    public void ChangeAnimation(string newState)
    {
        if (!BlockInput)
        {
            if (currentState == newState) return;
            playeranimator.Play(newState);
            currentState = newState;
        }
    }
    private void FixedUpdate()
    {
        if (BlockInput == false)
        {
            // Player Movement
            Vector2 delta = direction * vel * Time.deltaTime;
            Vector2 newPos = rb.position + delta;
            rb.MovePosition(newPos);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        // Get Entered Entrance and Identify it to leave scene or ignore
        for (int i = 0; i < Entrances.Length; i++)
        {
            if (collision == Entrances[i].Collider)
            {
                StartCoroutine(SwitchScene(Entrances[i].SceneToGo));
                GoingTo = Entrances[i].ID;
                if (Application.platform == RuntimePlatform.Android || MobileControlsOverride)
                {
                    panelanimator.SetTrigger("IsLeaving");
                    ButtonEAnimator.SetTrigger("IsLeaving");
                    JoyStickAnimator.SetTrigger("IsLeaving");
                }
            }
        }
        for (int i = 0; i < Interactables.Length; i++)
        {
            if (collision == Interactables[i].Collider)
            {
                if (BlockInput == false)
                {
                    if (Interactables[i].Type == InteractableType.Auto)
                    {
                        Interactables[i].OnInteract.Invoke();
                        ButtonEAnimator.SetTrigger("IsLeaving");
                        JoyStickAnimator.SetTrigger("IsLeaving");
                    }
                    if (Interactables[i].Type == InteractableType.PressE)
                    {
                        GameObject.FindGameObjectWithTag("E").GetComponent<SpriteRenderer>().color = Color.white;
                        WaitingForE = true;
                    }
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < Interactables.Length; i++)
        {
            if (collision == Interactables[i].Collider)
            {
                if (BlockInput == false)
                {
                    if (Interactables[i].Type == InteractableType.PressE)
                    {
                        GameObject.FindGameObjectWithTag("E").GetComponent<SpriteRenderer>().color = Color.clear;
                        WaitingForE = false;
                    }
                }
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < Interactables.Length; i++)
        {
            if (collision == Interactables[i].Collider && !BlockInput && Interactables[i].Type == InteractableType.PressE && WaitingForE &&EFired)
            {
                GameObject.FindGameObjectWithTag("E").GetComponent<SpriteRenderer>().color = Color.clear;
                WaitingForE = false;
                Interactables[i].OnInteract.Invoke();
            }
        }
    }
    // Only use inside Interactable Press E trigger
    public void ReenableInteractE()
    {
        GameObject.FindGameObjectWithTag("E").GetComponent<SpriteRenderer>().color = Color.white;
        WaitingForE = true;
    }
    public void GetInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EFired = true;
        }
        if (context.canceled)
        {
            EFired = false;
        }
    }

    IEnumerator SwitchScene(string SceneName)
    {
        yield return new WaitForSeconds(0.34f);
        SceneManager.LoadScene(SceneName);
        SetComponents();
    }
}
