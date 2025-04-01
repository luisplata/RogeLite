using UnityEngine;

public interface IPlayerMediator
{
    void Initialize(Joystick joystick);
    event System.Action<int> OnLevelUp;
    event System.Action OnDie;
    bool IsDead { get; }
    bool ICantGetMinerals { get; set; }
    void DisableControls();
    void CanGetMinerals(bool canGetMinerals, Mineral mineral);
    void CanMove(bool canMove);
    void GetMinerals();
    float GetTimeToMining();
    GameObject GetGameObject();
}