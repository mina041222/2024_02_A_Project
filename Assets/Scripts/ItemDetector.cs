using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Crystal,
    Plant,
    Bush,
    Tree,
}
public class ItemDetector : MonoBehaviour
{

    public float checkRadius = 3.0f;
    public Vector3 lastPosition;
    public float moveThreshold;
    public CollectibleItem currentNearbyItem;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        CheckForItem();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(lastPosition, transform.position)>moveThreshold)
        {
            CheckForItem();
            lastPosition = transform.position;
        }

        if (currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());
        }
    }

    private  void CheckForItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;
        CollectibleItem closestItem = null;

        foreach(Collider collider in hitColliders)
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem> ();
            if (item != null && item.canCollect)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }
        if(closestItem != currentNearbyItem)
        {
            currentNearbyItem = closestItem;
            if(currentNearbyItem != null)
            {
                Debug.Log($"[E]Ű�� ���� {currentNearbyItem.itemName} ����");
            }
        }
    }

    private void OnDrawGizmos()                 //����Ƽ Sceneâ�� ���̴� Debug �׸� 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, checkRadius);
    }
}