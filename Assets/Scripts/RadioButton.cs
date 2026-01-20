using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RadioButton : MonoBehaviour
{
    [SerializeField] private FrequencyController frequencyController;
    [SerializeField] private float frequency;

    private void OnMouseDown()
    {
        if (frequencyController == null)
        {
            Debug.LogError("FrequencyController nicht zugewiesen!", this);
            return;
        }

        frequencyController.SetFrequency(frequency);
        Debug.Log("Button pressed!");
    }
}
