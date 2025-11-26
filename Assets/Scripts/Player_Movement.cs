using NUnit.Framework.Constraints;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody rb;
    public bool NotInCar = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (NotInCar)
        {
            float x = Input.GetAxis("Horizontal"); // A/D
            float z = Input.GetAxis("Vertical");   // W/S

            Vector3 move = (transform.forward * z + transform.right * x) * speed;

            // Geschwindigkeit direkt setzen
            Vector3 newVel = new Vector3(move.x, rb.linearVelocity.y, move.z);
            rb.linearVelocity = newVel;
        }
    }
}
