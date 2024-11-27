using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUIManager : MonoBehaviour
{
   public static CraftingUIManager instance { get; private set; }

    [Header("Ui References")]
    public GameObject craftingPanel;
    public TextMeshProUGUI buildingNameText;
    public Transform recipeContainer;
    public RecipeButton closeButton;
    public GameObject recipeButtonPrefabs;

    private BuildingCrafter currentCrafter;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        craftingPanel.SetActive(false);
    }

    private void RefreshRecipeList()
    {
        foreach(Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }

        if(currentCrafter != null & currentCrafter.recipes != null)
        {
            foreach(CraftingRecipe receipe in currentCrafter.recipes)
            {
                GameObject buttonObj = Instantiate(recipeButtonPrefabs, recipeContainer);
                RecipeButton recipeButton = buttonObj.GetComponent < RecipeButton>();
                recipeButton.Setup(recipe, currentCrafter);
            }
        }
    }

    public void ShowUi(BuildingCrafter crafter)
    {
        currentCrafter = crafter;
        craftingPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (crafter != null)
        {
            buildingNameText.text = crafter.GetComponent<ConstructibleBuilding>().buildingName;
            RefreshRecipeList();

        }

    }

    public void HideUI()
    {
        craftingPanel.SetActive(false);
        currentCrafter = null;
        Cursor.visible = false;
        Cursor.LockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        closeButton.onClick.AddListener(() => HideUI());
    }
}
