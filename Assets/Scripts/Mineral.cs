using System.Collections.Generic;
using Items;
using Items.Factories;
using Items.Runtime;
using LootSystem;
using UnityEngine;

public class Mineral : MonoBehaviour, ILootable
{
    [SerializeField] private float luckFactor = 1.0f;
    [SerializeField] private int timesToMining = 3;
    [SerializeField] private LootTable lootTable;
    private IPlayerMediator _mediator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _mediator = other.gameObject.GetComponent<IPlayerMediator>();
            _mediator.CanGetMinerals(true, this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _mediator = other.gameObject.GetComponent<IPlayerMediator>();
            _mediator.CanGetMinerals(false, null);
            _mediator = null;
        }
    }

    public void TryToDestroy()
    {
        timesToMining--;
        if (timesToMining <= 0)
        {
            _mediator?.CanGetMinerals(false, null);
            Destroy(gameObject);
        }
    }

    public List<LootItemInstance> GetLoot()
    {
        var lootItems = ServiceLocator.Instance.GetService<ILootFactory>()
            .GenerateLoot(lootTable, _mediator.GetLuckFactor());
        return lootItems;
    }
}