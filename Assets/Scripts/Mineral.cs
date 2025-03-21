using UnityEngine;

public class Mineral : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var mediator = other.gameObject.GetComponent<IPlayerMediator>();
            mediator.CanGetMinerals(true, this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var mediator = other.gameObject.GetComponent<IPlayerMediator>();
            mediator.CanGetMinerals(false, null);
        }
    }
}