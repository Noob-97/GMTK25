using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool isDoorOpen;
    public bool openedFlag;
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
