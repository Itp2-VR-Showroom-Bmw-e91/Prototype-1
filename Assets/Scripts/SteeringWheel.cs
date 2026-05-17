using UnityEngine;

public class SteeringWheelVisual : MonoBehaviour
{
    public float maxAngle = 450f;
    public float sensitivity = 2f;

    private float angle;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        angle += input * sensitivity;
        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(0f, angle, 0f);
    }
}