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
        carbool = player.GetComponent<Player_Movement>().NotInCar;
        carcollider= car.GetComponent<Collider>();
    }
    private void OnMouseDown()
    {
        Vector3 pPosition = player.transform.localPosition;
        if (carbool)
        {
            carcollider.isTrigger = false;
            player.transform.localPosition = position.localPosition;
            carbool = false;
        }
        else
        {
            player.transform.localPosition = pPosition;
            carcollider.isTrigger = true;
            carbool = true;
        }
        
    }
}
