using Bellseboss;
using UnityEngine;

public class Mineral : MonoBehaviour, ILootSource
{
    [SerializeField] private LootTable lootTable;
    [SerializeField] private float luckFactor = 1.0f;
    [SerializeField] private int timesToMining = 3;
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

    public Item[] GetLoot()
    {
        return ServiceLocator.Instance.GetService<ILootFactory>().GenerateLoot(lootTable, luckFactor).ToArray();
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
}