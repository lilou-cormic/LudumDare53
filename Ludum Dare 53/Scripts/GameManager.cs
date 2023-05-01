using Godot;
using PurpleCable;
using System.Collections.Generic;

public partial class GameManager : Node2D
{
    private const int GridColCount = 25;

    private const int GridRowCount = 17;

    public static float MusicVolume = 0;

    public static float SfxVolume = 0;

    private static GameManager _instance;

    public static HQ HQ => HQ.Instance;

    private Warehouses _Warehouses;
    public static Warehouses Warehouses => _instance._Warehouses;

    private Destinations _Destinations;
    public static Destinations Destinations => _instance._Destinations;

    private Paths _Paths;
    public static Paths Paths => _instance._Paths;

    private ProjectileFactory _ProjectileFactory;
    public static ProjectileFactory ProjectileFactory => _instance._ProjectileFactory;

    private DragonFactory _LeftDragonFactory;
    public static DragonFactory LeftDragonFactory => _instance._LeftDragonFactory;

    private DragonFactory _RightDragonFactory;
    public static DragonFactory RightDragonFactory => _instance._RightDragonFactory;

    private AStar _aStar;

    private double _deliveryTimer = 2f;

    private double _dragonTimer = 20f;

    public GameManager()
    {
        _instance = this;

        GameUI.TileSize = 64;
    }

    public override void _Ready()
    {
        _Warehouses = GetNode<Warehouses>("Warehouses");
        _Destinations = GetNode<Destinations>("Destinations");
        _Paths = GetNode<Paths>("Paths");
        _ProjectileFactory = GetNode<ProjectileFactory>("ProjectileFactory");
        _LeftDragonFactory = GetNode<DragonFactory>("LeftDragonFactory");
        _RightDragonFactory = GetNode<DragonFactory>("RightDragonFactory");

        ScoreManager.ResetScore();

        CreateTileGrid();

        HQ.HireAgent();
        HQ.NewDelivery();
    }

    public override void _PhysicsProcess(double delta)
    {
        ProcessDelivery(delta);

        ProcessDragon(delta);
    }

    private void ProcessDelivery(double delta)
    {
        _deliveryTimer -= delta;

        if (_deliveryTimer <= 0)
        {
            HQ.NewDelivery();

            _deliveryTimer = 2f;
        }
    }

    private void ProcessDragon(double delta)
    {
        _dragonTimer -= delta;

        if (_dragonTimer <= 0)
        {
            if (GD.Randf() > 0.5)
                LeftDragonFactory.SendOffDragon();
            else
                RightDragonFactory.SendOffDragon();

            _dragonTimer = 20f;
        }
    }

    private void CreateTileGrid()
    {
        TileFactory tileFactory = GetNode<TileFactory>("TileFactory");

        List<List<AStarTile>> grid = new List<List<AStarTile>>();

        for (int colIndex = 0; colIndex < GridColCount; colIndex++)
        {
            List<AStarTile> column = new List<AStarTile>();

            for (int rowIndex = 0; rowIndex < GridRowCount; rowIndex++)
            {
                Tile tile = tileFactory.GetTile(colIndex, rowIndex);
                column.Add(tile.AStarTile);
            }

            grid.Add(column);
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
        ScoreManager.SetHighScore();

        _instance.GetTree().ChangeSceneToFile(@"res://Scenes/GameOver.tscn");
    }
}
