using Godot;
using PurpleCable;

public partial class TileFactory : PrefabFactory<Tile>
{
	public Tile GetTile(int col, int row)
	{
		return CreateItem(new Vector2(col * GameUI.TileSize, row * GameUI.TileSize));
	}
}
