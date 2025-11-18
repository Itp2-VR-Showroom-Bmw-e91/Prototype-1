using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        Vector3 move = (transform.forward * z + transform.right * x) * speed;

        // Geschwindigkeit direkt setzen
        Vector3 newVel = new Vector3(move.x, rb.linearVelocity.y, move.z);
        rb.linearVelocity = newVel;
    }
}
