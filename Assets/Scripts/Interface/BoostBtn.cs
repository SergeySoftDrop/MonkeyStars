using Assets.Scripts.Conf.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BoostBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameConfig gameConfig;
    public UnityEvent onHoldStart;
    public UnityEvent onHoldEnd;

    private bool isHolding = false;
    private bool isReloading = false;

    private float holdTimer = 0.0f;
    private float reloadTimer = 0.0f;

    void Update()
    {
        if(isReloading)
        {
            reloadTimer += Time.deltaTime;
            if(reloadTimer >= 1 / gameConfig.PlayerBoostRateInSecond)
            {
                isReloading = false;
                reloadTimer = 0f;
            }
        }
        else if(isHolding)
        {
            holdTimer += Time.deltaTime;

            if(holdTimer >= gameConfig.PlayerBoostMaxDurationInSecond)
            {
                Debug.Log("Boos");
                isReloading = true;
                isHolding = false;
                holdTimer = 0f;
                onHoldEnd.Invoke();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isReloading)
        {
            onHoldStart.Invoke();
            isHolding = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!isReloading)
        {
            isHolding = false;
            isReloading = true;
            holdTimer = 0.0f;
            onHoldEnd.Invoke();
        }
    }
}
