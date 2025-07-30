using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>().IsTouchingGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>().IsTouchingGround = false;
    }
}
