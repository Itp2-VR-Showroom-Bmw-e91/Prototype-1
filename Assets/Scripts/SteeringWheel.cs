using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    public float maxRotation = 450f;
    public float turnSpeed = 6f;
    public float returnSpeed = 4f;

    float currentRotation;
    float targetRotation;

    void Update()
    {
        if (Player_Movement.NotInCar) return;

        float input = 0f;

        if (Input.GetKey(KeyCode.Q)) input = -1f;
        if (Input.GetKey(KeyCode.E)) input = 1f;

        if (input != 0f)
        {
            targetRotation = input * maxRotation;
            currentRotation = Mathf.Lerp(
                currentRotation,
                targetRotation,
                Time.deltaTime * turnSpeed
            );
        }
        else
        {
            currentRotation = Mathf.Lerp(
                currentRotation,
                0f,
                Time.deltaTime * returnSpeed
            );
        }

        // 🔹 LOKALE Rotation um eigene Z-Achse
        transform.localEulerAngles =
            new Vector3(0f, 0f, -currentRotation);
    }
}
