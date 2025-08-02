using UnityEngine;
public enum EnemyType
{
    Follow
}
public class Enemy : MonoBehaviour
{
    public EnemyType Type;
    public float speed = 2f;
    public int hits = 3;
    public StartEvent assignedRoom;
    // Update is called once per frame
    void Update()
    {
        if (Type == EnemyType.Follow)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        if (hits == 0)
        {
            assignedRoom.progression();
            GameObject.FindGameObjectWithTag("progress").GetComponent<StartEvent>().progression();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            hits--;
        }
    }
}
