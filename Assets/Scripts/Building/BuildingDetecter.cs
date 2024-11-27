using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetecter : MonoBehaviour
{
    public float checkRadius = 3.0f;                         //������ ���� ����
    public Vector3 lastPostion;                             //�÷��̾��� ������ ��ġ
    public float moveThreshold = 0.1f;                      //�̵� ���� �Ӱ谪
    public ConstructibleBuilding currentNearbyBuilding;
    public BuildingCrafter currentBuildingCrafter;

    // Start is called before the first frame update
    void Start()
    {
        lastPostion = transform.position;                   //���� �� ���� ��ġ�� ������ ��ġ�� ����
        CheckForBuilding();                                    //�ʱ� �ǹ� üũ ���� 
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastPostion, transform.position) > moveThreshold)   //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        {
            CheckForBuilding();                                                    //�̵��� �ǹ� üũ
            lastPostion = transform.position;                                   //���� ��ġ�� ������ ��ġ�� ������Ʈ 
        }

        //����� �������� �ְ� EŰ�� ������ �� ������ ����
        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if(!currentNearbyBuilding.isConstructed)
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());
            }
            else if(currentBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding.buildingName} �� ���� �޴� ����");
                CraftingUIManager.instance?.ShowUi(currentBuildingCrafter);
            }
        }
    }

    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);       //���� ���� ���� ��� �ݶ��̴��� ã�ƿ�

        float closestDistance = float.MaxValue;                             //���� ����� �Ÿ��� �ʱⰪ
        ConstructibleBuilding closestBuilding = null;                                 //���� ����� �ǹ� �ʱⰪ
        BuildingCrafter closesCrafter = null;

        foreach (Collider collider in hitColliders)                         //�� �ݶ��̴��� �˻��Ͽ� ���� ������ �ǹ� ã��
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();            //�ǹ� ����
            if (building != null && building.canBuild && !building.isConstructed)        //�ǹ� �ְ� ���� �������� Ȯ��
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);     //�Ÿ� ���
                if (distance < closestDistance)                                                     //�� ����� �ǹ� �߰� �� ������Ʈ 
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closesCrafter = building.GetComponent<currentBuildingCrafter>();
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding)        //���� ����� �ǹ��� ����Ǿ��� �� �޼��� ǥ��
        {
            currentNearbyBuilding = closestBuilding;        //���� ����� �ǹ� ������Ʈ
            currnetBuildingCrafter = closesCrafter;
            if (currentNearbyBuilding != null)
            {
                if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstructed)
                {
                    if (FloatingTextManager.instance != null)
                    {
                        Vector3 textPostion = transform.position + Vector3.up * 0.5f;                   //������ ��ġ���� �ణ ���� �ؽ�Ʈ ����
                        FloatingTextManager.instance.Show(
                            $"[F] Ű�� {currentNearbyBuilding.buildingName} �Ǽ� (���� {currentNearbyBuilding.requiredTree} �� �ʿ�)"
                            , currentNearbyBuilding.transform.position + Vector3.up
                            );
                    }
                }
                

            }
        }
    }
}
