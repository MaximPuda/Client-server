using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Slider _hp;
    [SerializeField] private TextMeshProUGUI _coins;

    private void Start()
    {
        GameManager.Instance.ActivePlayer.GetDamageEvent += OnLifeChange;
        GameManager.Instance.ActivePlayer.GetCoinEvent += OnGetCoin;
    }

    private void OnDestroy()
    {
        GameManager.Instance.ActivePlayer.GetDamageEvent -= OnLifeChange;
        GameManager.Instance.ActivePlayer.GetCoinEvent -= OnGetCoin;
    }

    private void OnLifeChange(float value)
    {
        _hp.value = value;
    }

    private void OnGetCoin(int value)
    {
        _coins.text = value.ToString();
    }
}
