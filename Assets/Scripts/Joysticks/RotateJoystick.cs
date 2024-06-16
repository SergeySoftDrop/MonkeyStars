using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class RotateJoystick : Joystick
{
    public JoystickConfig joystickConfig;

    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / background.sizeDelta.x);
            pos.y = 0;

            inputVector = new Vector2(pos.x * 2 * joystickConfig.sensitivity, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            handle.anchoredPosition = new Vector2(inputVector.x * (background.sizeDelta.x / 2), 0);
        }
    }

    public new float Vertical()
    {
        return 0;
    }
}
