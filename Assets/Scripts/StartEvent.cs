using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartEvent : MonoBehaviour
{
    public UnityEvent PlayerTrigger;
    public bool DestroyOnExec;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerTrigger.Invoke();
            if (DestroyOnExec) { Destroy(gameObject); }
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
