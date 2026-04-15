using UnityEngine;

public class CarDoor : MonoBehaviour
{
    public float openAngle = 70f;
    public float speed = 2f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
    }

    void Update()
    {
        if (isOpen)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, openRotation, Time.deltaTime * speed);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, closedRotation, Time.deltaTime * speed);
        }
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }
}