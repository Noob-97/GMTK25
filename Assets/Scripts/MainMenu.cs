using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        if (PlayerPrefs.HasKey("end"))
        {
            SceneManager.LoadScene("Loading");
        }
    }

    public void Play()
    {
        SimulateLoading.NextScene = "IntroPlatformer";
        SceneManager.LoadScene("Loading");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
