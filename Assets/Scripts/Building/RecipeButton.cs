using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeButton : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI recipeName;
    public TextMeshProUGUI materialsText;
    public Button craftingButton;

    private CraftingRecipe recipe;
    private BuildingCrafter crafter;
    private PlayerInventory playerInventory;

    public void Setup(CraftingRecipe recipe, BuildingDetecter crafter)
    {
        this.recipe = recipe;
        this.crafter = crafter;
        playerInventory = FindObjectOfType<PlayerInventory>();

        recipeName.Text = recipe.itemName;
        UpdateMaterialsText();

        craftingButton.onClick.AddListener(OnCraftButtonlicked);
    }
    private void UpdateMaterialsText()
    {
        string materials = "필요 재료 : \n";
        for(int i = 0; i <recipe.requiredItems.Length; i++)
        {
            ItemType item = recipe.requiredItmes[i];
            int required = recipe.requiredAmounts[i];
            int has = playerInventory.GetItemCount(item);
            materials += $"{item} : {has}/{required} \n";
        }
        materialsText.text = materials;
    }

    private void OnCraftButtonlicked()
    {
        crafter.TryCraft(recipe, playerInventory);
        UpdateMaterialsText();
    }
}
