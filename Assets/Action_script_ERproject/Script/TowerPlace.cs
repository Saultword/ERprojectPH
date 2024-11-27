using UnityEngine;
using Valve.VR;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab; // ������Ԥ����
    public GameObject towerGhostPrefab; // ��������ӰԤ����
    public GameObject towerGhostRedPrefab; // ��ɫ��������ӰԤ����
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.RightHand; // ʹ������
    public SteamVR_Action_Boolean placeAction; // �󶨵�SteamVR��������
    public LayerMask groundLayer; // ����Ϊground��LayerMask
    public LineRenderer lineRenderer; // ������ʾ����
    public UnityEngine.UI.Text warningText; // ������ʾ������Ϣ��Text
    public equipment equipment; // Player��Equipment���
    public Transform handTransform; // �ֵ�λ��

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
                if (equipment != null && equipment.GetTurretModuleCount() >= 1)
                {
                    PlaceTower();
                    Destroy(currentTowerGhost);
                    equipment.DecreaseTurretModuleCount(); // ���ú����һ��Turret module
                }
                else
                {
                    Destroy(currentTowerGhost);
                    ShowWarning("�޷����÷�������Turret module�������㣡");
                }
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
                if (equipment != null && equipment.GetTurretModuleCount() >= 1)
                {
                    currentTowerGhost = Instantiate(towerGhostPrefab, adjustedHitPoint, Quaternion.identity);
                }
                else
                {
                    currentTowerGhost = Instantiate(towerGhostRedPrefab, adjustedHitPoint, Quaternion.identity);
                }
            }
            else
            {
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
        Instantiate(towerPrefab, currentTowerGhost.transform.position, Quaternion.identity);
    }

    void ShowWarning(string message)
    {
        if (warningText != null && handTransform != null)
        {
            warningText.text = message;
            warningText.gameObject.SetActive(true);

            // ����Text��λ�����ֵ�λ���Ϸ�
            Vector3 handPosition = handTransform.position;
            Vector3 warningPosition = handPosition + new Vector3(0, 0.2f, 0); // ����Y��ƫ����
            warningText.transform.position = warningPosition;

            Invoke("HideWarning", 2f); // 2������ؾ�����Ϣ
        }
    }

    void HideWarning()
    {
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }
    }
}
