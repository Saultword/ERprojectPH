using UnityEngine;
using Valve.VR;

public class TurretSpawner : MonoBehaviour
{
    public GameObject turretPrefab;
    public SteamVR_Action_Boolean spawnAction;
    public Transform handTransform;

    void Update()
    {
        if (spawnAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Ray ray = new Ray(handTransform.position, handTransform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                SpawnTurret(hit.point);
            }
        }
    }

    public void SpawnTurret(Vector3 position)
    {
        Instantiate(turretPrefab, position, Quaternion.identity);
    }
}