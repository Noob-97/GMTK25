using DG.Tweening;
using System;
using System.Collections;
using System.Runtime.InteropServices;
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
    public bool FakeError;
    public string NextScene = "IntroRPG";

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ShowAlert();
#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
#endif

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

    private void Update()
    {
        if (FakeError)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShowAlert();
#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            MessageBox(new IntPtr(0), "NullReferenceException: Object reference not set to an instance of an object\n" +
                   "GameManager.Update () (at Assets/Scripts/GameManager.cs:42)", "UnityPlayer.dll", 0x00000010);
#endif
            FakeError = false;
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
        SimulateLoading.NextScene = NextScene;
        SceneManager.LoadScene("Loading");
    }
}
