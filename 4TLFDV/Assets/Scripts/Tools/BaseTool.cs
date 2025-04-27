using UnityEngine;

public abstract class BaseTool : ScriptableObject, ITool
{
    public string toolName;
    public Sprite toolIcon;

    public enum UPGRADETIER
    {
        ONE, TWO, THREE, FOUR, FIVE
    }
    public UPGRADETIER upgradeTier = UPGRADETIER.ONE;

    public abstract void UseTool(FarmTile targetTile);
}
