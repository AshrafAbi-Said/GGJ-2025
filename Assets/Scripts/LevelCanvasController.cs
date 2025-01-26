using UnityEngine;
using TMPro;
using System;

public class LevelCanvasController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    DateTime startTime;
    public TextMeshProUGUI shellText;
    public TextMeshProUGUI canText;
    public TextMeshProUGUI glassText;
    public TextMeshProUGUI plasticText;
    public TextMeshProUGUI chipsText;
    public static LevelCanvasController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        startTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan timeSpan = DateTime.Now - startTime;
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

    }
}
