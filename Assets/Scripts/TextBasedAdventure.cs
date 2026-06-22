using UnityEngine;


public class TextBasedAdventure : MonoBehaviour
{
    [System.Serializable]
    public struct Room
    {
        public string Name;
        public TileType Type;
        public Enemy RoomEnemy;
    }

    public enum TileType
    {
        Invalid,
        Empty,
        Item,
        EnemyEncounter,
        Exit,
    }

    public enum DamageType
    {
        Invalid,
        Normal,
        Poison,
        Curse,
    }

    public struct StatusEffect
    {
        public bool IsActive;
        public DamageType Type;
        public int TurnsRemaining;
        public int DamagePerTurn;
    }

    public struct Enemy
    {
        public string Name;
        public int Damage;
        public DamageType DamageType;
    }

    private Room[,] dungeon = { 
                                {   new Room { Name = "Dark Cave",    Type = TileType.Empty } ,
                                    new Room { Name = "Mossy Tunnel", Type = TileType.Item  },
                                    new Room { Name = "Crystal Room", Type = TileType.Empty } 
                                },
                                
                                {   new Room { Name = "Bone Chamber", Type = TileType.EnemyEncounter, RoomEnemy = new Enemy { Name = "Goblin", Damage = 1, DamageType = DamageType.Normal } },
                                    new Room { Name = "Flooded Hall", Type = TileType.Empty },
                                    new Room { Name = "Iron Gate",    Type = TileType.Exit  } 
                                },

                                {   new Room { Name = "Goblin Den",   Type = TileType.Empty },
                                    new Room { Name = "Armory",       Type = TileType.EnemyEncounter, RoomEnemy = new Enemy { Name = "Orc", Damage = 2, DamageType = DamageType.Normal } },
                                    new Room { Name = "Throne Room",  Type = TileType.Item  } 
                                }
                              };

    private int playerRow = 0;
    private int playerCol = 0;

    public struct PlayerState
    {
        public int Health;
        public StatusEffect CurrentStatusEffect;
    }

    private PlayerState currentPlayerState;

    private int itemHealAmount = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPlayerState.Health = 10;
        OutputTileInformation();
    }

    // Update is called once per frame
    void Update()
    {
        bool wasKeyPressed = HandleMovementInput(out int newRow, out int newCol);
        if (!wasKeyPressed)
        {
            return;
        }
        SetPlayerPosition(newRow, newCol);
        OutputTileInformation();
    }

    private void OutputTileInformation()
    {
        Debug.Log("You are in: " + dungeon[playerRow, playerCol].Name);

        switch (dungeon[playerRow, playerCol].Type)
        {
            case TileType.Empty:
                Debug.Log("There is nothing here.");
                break;
            case TileType.EnemyEncounter:
                Debug.Log("Oooo a spooky ghost");
                EncounterEnemy();
                break;
            case TileType.Item:
                Debug.Log("You see a shiny object");
                ItemPickup();
                break;
            case TileType.Exit:
                Debug.Log("You see a way out");
                break;
            default:
                Debug.LogError("Invalid TileType");
                break;
        }
    }

    private void EncounterEnemy()
    {
        PlayerTakeDamage(dungeon[playerRow, playerCol].RoomEnemy.Damage);
    }
    
    private void ItemPickup()
    {
        PlayerHeal(itemHealAmount);
    }

    private void PlayerHeal(int heal)
    {
        currentPlayerState.Health += heal;
        Debug.Log("You get healed. Your health is now " + currentPlayerState.Health);
    }

    private void PlayerTakeDamage(int damage)
    {
        currentPlayerState.Health -= damage;
        Debug.Log("You get hit. Your health is now " + currentPlayerState.Health);
        if (currentPlayerState.Health <= 0)
        {
            currentPlayerState.Health = 0;
            Debug.Log("You are dead");
        }
    }

    /// <summary>
    /// Sets the player position to a new row and column position
    /// </summary>
    /// <param name="newRow"></param>
    /// <param name="newCol"></param>
    private void SetPlayerPosition(int newRow, int newCol)
    {
        if (CheckIfNewPositionInTileBounds(newRow, newCol))
        {
            playerRow = newRow;
            playerCol = newCol;
        }
        else
        {
            Debug.Log("Can't go that way");
        }
    }

    /// <summary>
    /// Determine if the new row and column position are within the bounds of the tiles
    /// </summary>
    /// <param name="newRow"></param>
    /// <param name="newCol"></param>
    /// <returns>True if it is within the bounds, false if not</returns>
    private bool CheckIfNewPositionInTileBounds(int newRow, int newCol)
    {
        return (newRow >= 0 && newRow < dungeon.GetLength(0)) && (newCol >= 0 && newCol < dungeon.GetLength(1));
    }

    /// <summary>
    /// Handles the player's input and sets potential new position in the tileNames array
    /// </summary>
    /// <param name="newRow">new row position</param>
    /// <param name="newCol">new column position</param>
    /// <returns>True if a movement input was pressed, false if not</returns>
    private bool HandleMovementInput(out int newRow, out int newCol)
    {
        bool hasPressedKey = true;
        newRow = playerRow;
        newCol = playerCol;
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("You pressed " + KeyCode.D);
            newCol++;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("You pressed " + KeyCode.A);
            newCol--;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("You pressed " + KeyCode.W);
            newRow--;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("You pressed " + KeyCode.S);
            newRow++;
        }
        else
        {
            hasPressedKey = false;
        }
        return hasPressedKey;
    }

}
