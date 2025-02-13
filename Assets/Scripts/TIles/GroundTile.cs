using UnityEngine;

public class GroundTile : Tile
{
    public Color secondaryColor;

    override public void init(bool isPrimary)
    {
        _renderer.color = isPrimary ? color : secondaryColor;
    }

}
