using Assets.Scripts.Conf.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootBtn : MonoBehaviour
{
    public GameConfig gameConfig;

    public UnityEngine.Events.UnityEvent onHold;
    
    private bool isHolding = false;
    private float holdTimer = 0.0f;

    void Update()
    {
        if(isHolding)
        {
            holdTimer += Time.deltaTime;
            if(holdTimer >= 1 / gameConfig.PlayerShootRateInSecond)
            {
                onHold.Invoke();
                holdTimer = 0.0f;
            }
        }
    }

    public void OnPointerDown()
    {
        onHold.Invoke();
        isHolding = true;
    }

    public void OnPointerUp()
    {
        isHolding = false;
        holdTimer = 0.0f;
    }
}
