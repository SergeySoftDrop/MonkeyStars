using UnityEngine;

[CreateAssetMenu(fileName = "JoystickConfig", menuName = "Configs/JoystickConfig")]
public class JoystickConfig : ScriptableObject
{
    public float sensitivity = 1.0f;
    public float deadZone = 0.1f;
}
