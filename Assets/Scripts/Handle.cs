using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public GameObject player;
    public Transform position;
    public GameObject car;
    private bool carbool;
    private Collider carcollider;


    private void Start()
    {
        
        carcollider= car.GetComponent<Collider>();
    }
    private void OnMouseDown()
    {
        Vector3 pPosition = player.transform.localPosition;
        if (Player_Movement.NotInCar)
        {
            carcollider.enabled = false;
            player.transform.localPosition = position.localPosition;
            Player_Movement.NotInCar = false;
        }
        else
        {
            player.transform.localPosition = pPosition;
            carcollider.enabled = true;
            Player_Movement.NotInCar = true;
        }
        
    }
}
