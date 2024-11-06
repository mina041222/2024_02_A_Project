using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{

    public ItemType itemType;
    public string itemName;
    public float respawnTime = 30.0f;
    public bool canCollect = true;

    public void CollectItem(PlayerInventory inventory)
    {
        if (!canCollect) return;

        inventory.AddItem(itemType);
        Debug.Log($"{itemName}  수집 완료");
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        canCollect = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;

        yield return new WaitForSeconds(respawnTime);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<MeshCollider>().enabled = true;
        canCollect = true;
    }
}
