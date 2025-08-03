using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public TextMeshPro text;
    int time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        time = int.Parse(text.text);
        StartCoroutine(countdown());
    }

    IEnumerator countdown()
    {
        while (time != 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            text.text = time.ToString();
        }
    }
}
