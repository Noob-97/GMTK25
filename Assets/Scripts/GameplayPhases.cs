using System;
using UnityEngine;
[Serializable]
public class PhaseInfo
{
    public GameObject[] ObjectsInPhase;
}
public class GameplayPhases : MonoBehaviour
{
    public PhaseInfo[] phases;
    void Start()
    {
        int phase = PlayerPrefs.GetInt("Phase", 0);
        foreach (GameObject obj in phases[phase].ObjectsInPhase)
        {
            obj.SetActive(true);
        }
    }
}
