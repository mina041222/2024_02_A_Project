using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats;                    //클래스 선언 

    //각각 아이템 개수를 저장하는 변수
    public int crystalCount = 0;                        //크리스탈 개수 
    public int plantCount = 0;                        //식물 개수 
    public int bushCount = 0;                        //수풀 개수 
    public int treeCount = 0;                        //나무 개수 

    //추가 아이템 변수 
    public int vegetableStewCount = 0;                          //야채 스튜 개수
    public int fruitSaledCount = 0;                             //과일 샐러드 개수
    public int repairKitCount = 0;                              //수리 키트 개수 

    public void Start()
    {
        survivalStats = GetComponent<SurvivalStats>();
    }
    public void UseItem(ItemType itemType)
    {
        if (GetItemCount(itemType) <= 0)                     //해당 아이템이 있는지 확인
        {
            return;
        }
        switch (itemType)
        {
            case ItemType.VeagetableStew:
                RemoveItem(ItemType.VeagetableStew, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[0].hungerRestoreAmount); //설정한 수치 동작
                break;
            case ItemType.FruitSalad:
                RemoveItem(ItemType.FruitSalad, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[1].hungerRestoreAmount); //설정한 수치 동작
                break;
            case ItemType.RepairKit:
                RemoveItem(ItemType.RepairKit, 1);
                survivalStats.EatFood(RecipeList.WorkbenchRecipes[0].repairAmount); //설정한 수치 동작
                break;
        }
    }


    //여러 아이템을 한꺼번에 획득
    public void AddItem(ItemType itemType, int amount)
    {
        //amount 망큼 여러번 AddItem 호출
        for (int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }


    //아이템을 추가하는 함수, 아이템 종류에 따라서 해당 아티메의 개수를 증가 시킴
    public void AddItem(ItemType itemType)
    {
        //아이템 종류에 따른 다른 동작 수행 
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;                 //크리스탈 개수 증가
                Debug.Log($"크리스탈 획득 ! 현재 개수 : {crystalCount}");             //현재 크리스탈 개수 출력
                break;
            case ItemType.Plant:
                plantCount++;                 //식물 개수 증가
                Debug.Log($"식물 획득 ! 현재 개수 : {plantCount}");             //현재 식물 개수 출력
                break;
            case ItemType.Bush:
                bushCount++;                 //수풀 개수 증가
                Debug.Log($"수풀 획득 ! 현재 개수 : {bushCount}");             //현재 수풀 개수 출력
                break;
            case ItemType.Tree:
                treeCount++;                 //나무 개수 증가
                Debug.Log($"나무 획득 ! 현재 개수 : {treeCount}");             //현재 나무 개수 출력
                break;

            case ItemType.VeagetableStew:
                vegetableStewCount++;                 //야채 스튜 개수 증가
                Debug.Log($"야채 스튜 획득 ! 현재 개수 : {vegetableStewCount}");             //현재 야채 스튜 개수 출력
                break;
            case ItemType.FruitSalad:
                fruitSaledCount++;                 //과일 샐러드 개수 증가
                Debug.Log($"과일 샐러드 획득 ! 현재 개수 : {fruitSaledCount}");             //현재 과일 샐러드 개수 출력
                break;
            case ItemType.RepairKit:
                repairKitCount++;                 //수리 키트 개수 증가
                Debug.Log($"수리 키트 획득 ! 현재 개수 : {repairKitCount}");             //현재 수리 키트 개수 출력
                break;
        }
    }

    public bool RemoveItem(ItemType itemType, int amount = 1)
    {
        //아이템 종류에 따른 다른 동작 수행 
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount)         //가지고 있는 개수가 충분한지 확인
                {
                    crystalCount -= amount;
                    Debug.Log($"크리스탈 {amount} 사용 ! 현재 개수 : {crystalCount}");             //현재 크리스탈 개수 출력
                    return true;
                }
                break;
            case ItemType.Plant:
                if (crystalCount >= amount)         //가지고 있는 개수가 충분한지 확인
                {
                    crystalCount -= amount;
                    Debug.Log($"식물 {amount} 사용  ! 현재 개수 : {plantCount}");             //현재 식물 개수 출력
                    return true;
                }
                break;
            case ItemType.Bush:

                if (bushCount >= amount)         //가지고 있는 개수가 충분한지 확인
                {
                    bushCount -= amount;
                    Debug.Log($"수풀 {amount} 사용 ! 현재 개수 : {bushCount}");             //현재 수풀 개수 출력
                    return true;
                }
                break;

            case ItemType.Tree:
                if (treeCount >= amount)         //가지고 있는 개수가 충분한지 확인
                {
                    treeCount -= amount;
                    Debug.Log($"나무 {amount} 사용 ! 현재 개수 : {treeCount}");             //현재 나무 개수 출력
                    return true;
                }
                break;

            case ItemType.VeagetableStew:
                if (vegetableStewCount >= amount)         //가지고 있는 개수가 충분한지 확인
                {
                    vegetableStewCount -= amount;
                    Debug.Log($"야채 스튜 {amount} 사용 ! 현재 개수 : {vegetableStewCount}");             //현재 야채 스튜 개수 출력
                    return true;
                }
                break;
            case ItemType.FruitSalad:
                if (fruitSaledCount >= amount)         //가지고 있는 개수가 충분한지 확인
                {
                    fruitSaledCount -= amount;
                    Debug.Log($"과일 샐러드 {amount} 사용 ! 현재 개수 : {fruitSaledCount}");             //현재 과일 샐러드 개수 출력
                    return true;
                }
                break;
            case ItemType.RepairKit:
                if (repairKitCount >= amount)         //가지고 있는 개수가 충분한지 확인
                {
                    repairKitCount -= amount;
                    Debug.Log($"수리 키트 {amount} 사용 ! 현재 개수 : {repairKitCount}");             //현재 수리 키트 개수 출력
                    return true;
                }
                break;
        }
        return false;
    }

    public int GetItemCount(ItemType itemType)      //특정 아이템의 현재 개수를 반환 하는 함수 
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
            case ItemType.Plant:
                return plantCount;
            case ItemType.Bush:
                return bushCount;
            case ItemType.Tree:
                return treeCount;

            case ItemType.VeagetableStew:
                return vegetableStewCount;
            case ItemType.FruitSalad:
                return fruitSaledCount;
            case ItemType.RepairKit:
                return repairKitCount;

            default:
                return 0;
        }
    }

    void Update()
    {
        //I 키를 눌렀을때 인벤토리 로그 내역을 보여줌
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("=======인벤토리========");
        Debug.Log($"크리스탈:{crystalCount}개");         //크리스탈 개수 출력 
        Debug.Log($"식물:{plantCount}개");         //식물 개수 출력 
        Debug.Log($"수풀:{bushCount}개");         //수풀 개수 출력 
        Debug.Log($"나무:{treeCount}개");         //나무 개수 출력 
        Debug.Log("=======================");
    }
}
