using UnityEngine;
using Valve.VR;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab; // 防御塔预制体
    public GameObject towerGhostPrefab; // 防御塔虚影预制体
    public GameObject towerGhostRedPrefab; // 红色防御塔虚影预制体
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.RightHand; // 使用右手
    public SteamVR_Action_Boolean placeAction; // 绑定的SteamVR按键动作
    public LayerMask groundLayer; // 设置为ground的LayerMask
    public LineRenderer lineRenderer; // 用于显示射线
    public UnityEngine.UI.Text warningText; // 用于显示警告信息的Text
    public equipment equipment; // Player的Equipment组件
    public Transform handTransform; // 手的位置

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
                    equipment.DecreaseTurretModuleCount(); // 放置后减少一个Turret module
                }
                else
                {
                    Destroy(currentTowerGhost);
                    ShowWarning("无法放置防御塔，Turret module数量不足！");
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

            // 设置Text的位置在手的位置上方
            Vector3 handPosition = handTransform.position;
            Vector3 warningPosition = handPosition + new Vector3(0, 0.2f, 0); // 调整Y轴偏移量
            warningText.transform.position = warningPosition;

            Invoke("HideWarning", 2f); // 2秒后隐藏警告信息
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
