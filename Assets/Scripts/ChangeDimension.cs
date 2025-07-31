using UnityEngine;

public class ChangeDimension : MonoBehaviour
{
    public GameObject dimensionA;
    public GameObject dimensionB;
    public Transform instantiatorRef; // instantiate dimension at following pos
    static bool ChangedDimension; // false = dimensionA; true = dimensionB
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("Press E!");
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("change");
            if (ChangedDimension && GameObject.FindGameObjectWithTag("dA") == null)
            {
                ChangedDimension = false;
                Instantiate(dimensionA, instantiatorRef.position, Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("dB"));
            }
            else if (GameObject.FindGameObjectWithTag("dB") == null)
            {
                ChangedDimension = true;
                Instantiate(dimensionB, instantiatorRef.position, Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("dA"));
            }
        }
    }
}
