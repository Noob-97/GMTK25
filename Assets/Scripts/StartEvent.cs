using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartEvent : MonoBehaviour
{
    public UnityEvent PlayerTrigger;
    public bool DestroyOnExec;
    public bool EnableAllBelow;
    public bool EnableAllBelowProgressive;
    public int Progression;
    public bool Inverse;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerTrigger.Invoke();
            if (EnableAllBelow)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            if (EnableAllBelowProgressive)
            {
                transform.GetChild(Progression - 1).gameObject.SetActive(!Inverse);
            }
            if (DestroyOnExec) { Destroy(gameObject); }
        }
    }


    public void progression()
    {
        Progression++;
        if (EnableAllBelowProgressive)
        {
            if (transform.childCount >= Progression && Progression >= 1)
            {
                if (!Inverse)
                {
                    transform.GetChild(Progression - 1).gameObject.SetActive(true);
                }
                else
                {
                    Destroy(transform.GetChild(Progression - 1).gameObject);
                }
            }
        }
    }

    public void FadeMusic()
    {
        GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().DOFade(0, 5f);
    }

    public void LOOP(float time)
    {
        StartCoroutine(wait(time));
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        SimulateLoading.NextScene = "IntroRPG";
        SceneManager.LoadScene("Loading");
    }
}
