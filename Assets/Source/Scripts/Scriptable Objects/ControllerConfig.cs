using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/ControllerConfig")]
public sealed class ControllerConfig : ScriptableObject
{
    public KeyboardKeys[] keyboards;
}

[Serializable] public class KeyboardKeys
{
    public KeyCode up;
    public KeyCode Down;
    public KeyCode left;
    public KeyCode right;
}
