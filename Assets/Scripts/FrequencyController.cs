using UnityEngine;

public class FrequencyController : MonoBehaviour
{
    [Header("Current Frequency")]
    public float frequency;

    public void SetFrequency(float newFrequency)
    {
        frequency = newFrequency;
    }
}
