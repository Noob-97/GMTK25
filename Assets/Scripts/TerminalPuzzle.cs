using DG.Tweening;
using System.Collections;
using System.Diagnostics.Contracts;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class TerminalPuzzle : MonoBehaviour
{
    public PlayableDirector win;
    public PlayableDirector web;
    public GameObject textwin;
    public GameObject textweb;
    public GameObject endwin;
    public GameObject endweb;
    public int PASSWORD;
    public PlayableDirector completed;

    private void Start()
    {
        PASSWORD = Random.Range(1000, 9999);
    }

    public void PlayCustomCutscene(float delay)
    {
        StartCoroutine(cutscene(delay));
    }

    IEnumerator cutscene(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            web.Play();
            textweb.SetActive(true);
            print("PASSWORD:" + PASSWORD + "\nfor terminal!!!!!");
        }
        else
        {
            win.Play();
            textwin.SetActive(true);
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/terminalPASSWORD-untitled.txt";

            File.WriteAllText(path, "this is the terminal password!!!: " + PASSWORD.ToString() + "\ni hope i don't forget it lol");
#endif
        }
    }

    public void CheckPassword(string input)
    {
        if (input == PASSWORD.ToString())
        {
            Destroy(textwin);
            Destroy(textweb);
            completed.Play();
            GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().DOFade(0, 15f);
            End(40);
        }
    }

    public void End(float delay)
    {
        StartCoroutine(customendmessage(delay));
    }

    IEnumerator customendmessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            endweb.SetActive(true);
        }
        else
        {
            endwin.SetActive(true);
        }
        PlayerPrefs.SetInt("end", 1);
    }
}
