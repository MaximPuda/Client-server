using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private int _price = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.GetCoin(_price);
            Destroy(gameObject);
        }
    }
}
