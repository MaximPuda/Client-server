using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController ActivePlayer;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }
}
