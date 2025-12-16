using UnityEngine;
using TMPro;

public class FrequencyController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshPro frequencyText;

    [Header("Buttons (Cubes mit Collider)")]
    public GameObject leftButton;
    public GameObject rightButton;

    [Header("Frequency Settings")]
    public float frequency = 99.5f;
    public float step = 0.1f;
    public float minFrequency = 88.0f;
    public float maxFrequency = 108.0f;

    [Header("Optional Radio")]
    public Radio radio;

    void Start()
    {
        UpdateDisplay();
        UpdateRadio();
    }

    void UpdateDisplay()
    {
        if (frequencyText != null)
            frequencyText.text = frequency.ToString("F1") + " MHz";
    }

    void UpdateRadio()
    {
        if (radio != null)
            radio.UpdateFrequency(frequency);
    }

    public void IncreaseFrequency()
    {
        frequency += step;
        if (frequency > maxFrequency)
            frequency = minFrequency;

        UpdateDisplay();
        UpdateRadio();
    }

    public void DecreaseFrequency()
    {
        frequency -= step;
        if (frequency < minFrequency)
            frequency = maxFrequency;

        UpdateDisplay();
        UpdateRadio();
    }

    // --- Button Clicks ---
    void OnMouseDown()
    {
        if (Camera.main == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
            return;

        if (hit.collider.gameObject == leftButton)
            DecreaseFrequency();

        if (hit.collider.gameObject == rightButton)
            IncreaseFrequency();
    }
}

