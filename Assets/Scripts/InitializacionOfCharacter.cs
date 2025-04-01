using Cinemachine;
using UnityEngine;

public class InitializacionOfCharacter : MonoBehaviour, IInitializableCharacter
{
    [SerializeField] private PlayerMediator prefab;
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameUiController gameUiController;
    [SerializeField] private CinemachineVirtualCameraBase virtualCamera;
    private IPlayerMediator playerMediator;

    public void Initialize()
    {
        playerMediator = Instantiate(prefab);
        playerMediator.Initialize(joystick);
        playerMediator.OnDie += PlayerMediatorOnOnDie;
        gameUiController.Initialize();
        virtualCamera.Follow = playerMediator.GetGameObject().transform;
        virtualCamera.LookAt = playerMediator.GetGameObject().transform;
    }

    private void PlayerMediatorOnOnDie()
    {
        playerMediator.OnDie -= PlayerMediatorOnOnDie;
        Destroy(playerMediator.GetGameObject());
        playerMediator = null;
    }

    public IPlayerMediator GetMediator()
    {
        return playerMediator;
    }
}

public interface IInitializableCharacter
{
    void Initialize();
    IPlayerMediator GetMediator();
}