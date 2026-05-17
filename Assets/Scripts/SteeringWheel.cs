using UnityEngine;

public class SteeringWheel: MonoBehaviour
{
    public float maxAngle = 450f;
    public float currentAngle;

    void Update()
    {
        Vector3 localRot = transform.localEulerAngles;

        float z = localRot.z;

        // 0–360 → -180–180
        if (z > 180f)
            z -= 360f;

        currentAngle = Mathf.Clamp(z, -maxAngle, maxAngle);

        // WICHTIG: nur Z setzen, X/Y behalten nicht manipulieren
        currentAngle = -currentAngle;
        transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
}
}