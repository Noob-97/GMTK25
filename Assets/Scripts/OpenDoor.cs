using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour
{
    public bool isDoorOpen;
    public bool openedFlag;
    public bool EndingDoor;
    private void Update()
    {
        if (isDoorOpen)
        {
            GetComponent<Animator>().Play("DoorOpen");
            isDoorOpen = false;
            openedFlag = true;
        }
    }
}
