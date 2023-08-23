using UnityEngine.Events;

public static class GlobalEvents
{
    public static UnityAction<float> LifeChangeEvent;
    public static UnityAction PlayerDeathEvent;


    public static void LifeChangeInvoke(float value) => LifeChangeEvent?.Invoke(value);

    public static void PlayerDeathInvoke() => PlayerDeathEvent?.Invoke();
}
