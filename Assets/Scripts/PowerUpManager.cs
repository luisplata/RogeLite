using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public List<PowerUp> powerUps;
    private float nextPowerUpTime;
    private PlayerMediator mediator;

    public void Initialize(PlayerMediator mediator)
    {
        this.mediator = mediator;
    }


    public void ShowPowerUpOptions()
    {
        List<PowerUp> randomPowerUps = GetRandomPowerUps(1);
        //PowerUpUI.Instance.ShowOptions(randomPowerUps);
        mediator.OnPowerUpApplied(randomPowerUps[0]);
    }

    public void ApplyPowerUp(PowerUp powerUp)
    {
        mediator.OnPowerUpApplied(powerUp);
    }

    List<PowerUp> GetRandomPowerUps(int count)
    {
        List<PowerUp> shuffled = new List<PowerUp>(powerUps);
        //shuffled.Shuffle();
        return shuffled.GetRange(0, count);
    }
}