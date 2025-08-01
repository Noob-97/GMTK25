using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulateLoading : MonoBehaviour
{
    public static string NextScene = "IntroPlatformer";
    public float LoadingTime = 3f; // Duration of the simulated loading time
    void Start()
    {
        StartCoroutine(SimulateLoad());
    }

    IEnumerator SimulateLoad()
    {
        // Simulate loading time
        yield return new WaitForSeconds(LoadingTime);
        
        // Load the next scene
        SceneManager.LoadScene(NextScene);
    }
}
