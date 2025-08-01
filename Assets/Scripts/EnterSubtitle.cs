using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum TriggerMethod
{
    OnTriggerEnter,
    Start,
    Awake,
    Manual
}
public class EnterSubtitle : MonoBehaviour
{
    public TriggerMethod Method;
    public GameObject SubtitlePrefab;
    public string SubtitleText;
    public float TextDuration = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Method == TriggerMethod.Start)
        {
            ShowSubtitle();
        }
    }

    void Awake()
    {
        if (Method == TriggerMethod.Awake)
        {
            ShowSubtitle();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Method == TriggerMethod.OnTriggerEnter && other.CompareTag("Player"))
        {
            ShowSubtitle();
        }
    }

    public void ShowSubtitle()
    {
        GameObject obj = Instantiate(SubtitlePrefab, GameObject.FindGameObjectWithTag("SubBox").transform);
        obj.GetComponent<TextMeshProUGUI>().text = SubtitleText;
        Transform SubBox = GameObject.FindGameObjectWithTag("SubBox").transform;
        SubBox.GetComponent<Image>().DOFade(0.85f, 0.25f);
        StartCoroutine(DeleteSub(obj));
    }

    IEnumerator DeleteSub(GameObject sub)
    {
        yield return new WaitForSeconds(TextDuration);
        Destroy(sub);
        Transform box = GameObject.FindGameObjectWithTag("SubBox").transform;
        if (box.childCount - 1 == 0)
        {
            box.GetComponent<Image>().DOFade(0, 0.25f);
        }
    }
}
