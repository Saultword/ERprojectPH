using UnityEngine;
using Valve.VR;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab; // ������Ԥ����
    public GameObject towerGhostPrefab; // ��������ӰԤ����
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.RightHand; // ʹ������
    public SteamVR_Action_Boolean placeAction; // �󶨵�SteamVR��������
    public LayerMask groundLayer; // ����Ϊground��LayerMask
    public LineRenderer lineRenderer; // ������ʾ����

    private GameObject currentTowerGhost;

    void Update()
    {
        if (placeAction.GetState(handType))
        {
            ShowRay();
        }
        else
        {
            lineRenderer.enabled = false;
            if (currentTowerGhost != null)
            {
                PlaceTower();
                Destroy(currentTowerGhost);
            }
        }
    }

    void ShowRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 adjustedHitPoint = new Vector3(hit.point.x, hit.point.y - 15, hit.point.z - 15);
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, hit.point);

            if (currentTowerGhost == null)
            {
                Debug.Log("ini ghst");
                //currentTowerGhost = Instantiate(towerGhostPrefab, hit.point, Quaternion.identity);
                currentTowerGhost = Instantiate(towerGhostPrefab, adjustedHitPoint, Quaternion.identity);
            }
            else
            {
                Debug.Log("change position");
                currentTowerGhost.transform.position = adjustedHitPoint;
            }
        }
        else
        {
            lineRenderer.enabled = false;
            if (currentTowerGhost != null)
            {
                Destroy(currentTowerGhost);
            }
        }
    }

    void PlaceTower()
    {
        Debug.Log("place tower");
        Instantiate(towerPrefab, currentTowerGhost.transform.position, Quaternion.identity);
        Debug.Log("Spawn Position: " + currentTowerGhost.transform.position);
    }
}