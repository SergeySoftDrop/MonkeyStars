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
    private bool isUsingSpace = false;

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
                isReloading = true;
                isHolding = false;
                holdTimer = 0f;
                onHoldEnd.Invoke();
            }
        }

        if(!isReloading && Input.GetKeyDown(KeyCode.Space))
        {
            if(!isHolding)
            {
                isUsingSpace = true;
                onHoldStart.Invoke();
                isHolding = true;
            }
        }

        if(isUsingSpace && Input.GetKeyUp(KeyCode.Space))
        {
            isUsingSpace = false;
            if(isHolding)
            {
                isHolding = false;
                isReloading = true;
                holdTimer = 0f;
                onHoldEnd.Invoke();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isReloading && !isUsingSpace)
        {
            onHoldStart.Invoke();
            isHolding = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!isReloading && !isUsingSpace)
        {
            isHolding = false;
            isReloading = true;
            holdTimer = 0.0f;
            onHoldEnd.Invoke();
        }
    }
}