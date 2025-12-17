using UnityEngine;
using TMPro;

public class FrequencyController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshPro frequencyText;

    [Header("Buttons (Cubes mit Collider)")]
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject startButton;

    [Header("Frequency Settings")]
    public float frequency = 99.5f;
    public float step = 0.1f;
    public float minFrequency = 88.0f;
    public float maxFrequency = 108.0f;


    public Radio radio;


    public bool startButtonb;
    public bool leftButtonb;
    public bool rightButtonb;


    void Start()
    {

    }
    private void Update()
    {
        BoolReset(rightButtonb);
        BoolReset(startButtonb);   
        BoolReset(leftButtonb);
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (frequencyText != null && frequency > minFrequency && frequency < maxFrequency)
        {
            frequencyText.text = frequency.ToString("F1") + " MHz";
        }
    }

    private void BoolReset(bool reset)
    {
        if (reset)
        {
            reset = false;
        }
    }

}
  

