using UnityEngine;

[CreateAssetMenu(menuName = "Config/PlayerConfig")]
public sealed class PlayerConfig : ScriptableObject
{
    public float playerSpeed = 3;
    public float playerMaxHealth = 100;
    public float cameraLerpSpeed = 5f;
}

