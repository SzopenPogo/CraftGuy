using UnityEngine;

public class PlayerCrafting : Crafting
{
    public static PlayerCrafting Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
