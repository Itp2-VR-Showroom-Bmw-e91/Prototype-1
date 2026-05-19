using UnityEngine;

public class VRSpawner : MonoBehaviour
{
    // Das Objekt, dessen Position verwendet wird
    public Transform spawnPoint;

    void Awake()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
    }
}