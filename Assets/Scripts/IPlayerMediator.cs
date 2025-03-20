public interface IPlayerMediator
{
    void Initialize();
    event System.Action<int> OnLevelUp;
    event System.Action OnDie;
    bool IsDead { get; }
    void DisableControls();
}