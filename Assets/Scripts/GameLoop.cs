using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class GameLoop : MonoBehaviour, IGameLoop
{
    [SerializeField, InterfaceType(typeof(IInitializableCharacter))]
    private Object player;

    private IInitializableCharacter InitializableCharacterInstance => player as IInitializableCharacter;
    private IPlayerMediator PlayerMediatorInstance => InitializableCharacterInstance.GetMediator() as IPlayerMediator;
    
    // [SerializeField, InterfaceType(typeof(IEnemySpawn))]
    // private Object spawnEnemy;
    // private IEnemySpawn EnemySpawnInstance => spawnEnemy as IEnemySpawn;
    
    [SerializeField, InterfaceType(typeof(IEnemySpawn))]
    private Object[] spawnEnemies;
    public IEnemySpawn[] spawnEnemiesInstance => spawnEnemies.OfType<IEnemySpawn>().ToArray();

    [SerializeField] private UIDocument menuUi;
    [SerializeField] private GameObject endUi;

    private GameStateMachine _stateMachine;

    public bool StartGame { get; set; }
    public bool PlayerIsDead => PlayerMediatorInstance.IsDead;
    public bool WantExit { get; set; }

    public void DisablePlayerMovement()
    {
        PlayerMediatorInstance.DisableControls();
    }

    public void DisableSpawnEnemies()
    {
        foreach (var enemySpawn in spawnEnemiesInstance)
        {
            enemySpawn.DisableSpawn();
        }
        //EnemySpawnInstance.DisableSpawn();
    }

    public void HideMenuUI()
    {
        menuUi.enabled = false;
    }

    public void HideGameOverUi()
    {
        endUi.SetActive(false);
    }

    public void ShowGameOverUi()
    {
        endUi.SetActive(true);
    }

    private bool _whileTrue;

    private void Awake()
    {
        _stateMachine = new GameStateMachine();
        _stateMachine.AddInitialState(StateOfGame.INIT, new InitState(StateOfGame.START, this));
        var start = new StartGameState(StateOfGame.DIE, this);
        _stateMachine.AddState(StateOfGame.START, start);
        _stateMachine.AddState(StateOfGame.DIE, new MuerteState(StateOfGame.END, this));
        _stateMachine.AddState(StateOfGame.END, new EndGameState(StateOfGame.EXIT, this));
    }

    private void OnEnable()
    {
        _whileTrue = true;
        StartStateMachine();
    }

    private async Awaitable StartStateMachine()
    {
        var gameState = _stateMachine.GetInitialState();
        while (_whileTrue)
        {
            await gameState.Enter();
            await gameState.Doing();
            await gameState.Exit();
            var nextState = gameState.NextState();
            if (nextState == StateOfGame.EXIT)
            {
                break;
            }

            gameState = _stateMachine.GetState(nextState);
        }

        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        _whileTrue = false;
    }

    public void EnablePlayerMovement()
    {
        InitializableCharacterInstance.Initialize();
    }

    public void EnableSpawnEnemies()
    {
        foreach (var enemySpawn in spawnEnemiesInstance)
        {
            enemySpawn.StartSpawn();
        }
        //EnemySpawnInstance.StartSpawn();
    }
}

public interface IGameLoop
{
    void EnablePlayerMovement();
    void EnableSpawnEnemies();
    bool StartGame { get; }
    bool PlayerIsDead { get; }
    bool WantExit { get; set; }
    void DisablePlayerMovement();
    void DisableSpawnEnemies();
    void HideMenuUI();
    void HideGameOverUi();
    void ShowGameOverUi();
}