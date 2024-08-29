using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiCraftingContainer
{
    private UiCraftingController uiCraftingController;
    private VisualElement craftContainer;

    public UiCraftingContainer(UiCraftingController uiCraftingController)
    {
        this.uiCraftingController = uiCraftingController;

        craftContainer = uiCraftingController.Root.Q<VisualElement>("Craft-Container");
    }
}
