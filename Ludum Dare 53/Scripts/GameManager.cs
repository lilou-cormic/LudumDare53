using Godot;
using PurpleCable;
using System.Collections.Generic;

public partial class GameManager : Node2D
{
    private const int GridColCount = 20;

    private const int GridRowCount = 10;

    public static float MusicVolume = 0;

    public static float SfxVolume = 0;

    private static GameManager _instance;

    public static HQ HQ => HQ.Instance;

    private Warehouses _Warehouses;
    public static Warehouses Warehouses => _instance._Warehouses;

    private Destinations _Destinations;
    public static Destinations Destinations => _instance._Destinations;

    private AStar _aStar;

    public GameManager()
    {
        _instance = this;

        GameUI.TileSize = 100;
    }

    public override void _Ready()
    {
        _Warehouses = GetNode<Warehouses>("Warehouses");
        _Destinations = GetNode<Destinations>("Destinations");

        ScoreManager.ResetScore();

        CreateTileGrid();

        HQ.HireAgent();
        HQ.NewDelivery();
    }

    private void CreateTileGrid()
    {
        TileFactory tileFactory = GetNode<TileFactory>("TileFactory");

        List<List<AStarTile>> grid = new List<List<AStarTile>>();

        for (int colIndex = 0; colIndex < GridColCount; colIndex++)
        {
            List<AStarTile> row = new List<AStarTile>();

            for (int rowIndex = 0; rowIndex < GridRowCount; rowIndex++)
            {
                Tile tile = tileFactory.GetTile(colIndex, rowIndex);
                row.Add(tile.AStarTile);

                tile.SetIsWalkable(colIndex == 10 || (colIndex == 7 || colIndex == 14 || rowIndex == 5 || rowIndex == 3) && rowIndex <= 5);
            }

            grid.Add(row);
        }

        _aStar = new AStar(grid);
    }

    public static int GetDistance(Node2D start, Node2D end)
    {
        return _instance._aStar.FindPath(start.GlobalPosition, end.GlobalPosition)?.Count ?? -1;
    }

    public static Vector2? GetNextPosition(Node2D start, Node2D end)
    {
        return _instance._aStar.FindPath(start.GlobalPosition, end.GlobalPosition)?.Pop().Position * GameUI.TileSize;
    }

    public static void GameOver()
    {
        _instance.GetTree().ChangeSceneToFile(@"res://Scenes/GameOver.tscn");
    }
}