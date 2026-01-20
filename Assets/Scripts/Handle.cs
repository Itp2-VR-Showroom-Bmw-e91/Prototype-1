using UnityEngine;

public class Handle : MonoBehaviour
{
    public GameObject player;
    public Transform seatPosition;
    public GameObject car;

    private Collider carCollider;

    void Start()
    {
        carCollider = car.GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        if (Player_Movement.NotInCar)
        {
            player.transform.position = seatPosition.position;
            carCollider.enabled = false;
            Player_Movement.NotInCar = false;
        }
        else
        {
            carCollider.enabled = true;
            Player_Movement.NotInCar = true;
        }
    }
}
