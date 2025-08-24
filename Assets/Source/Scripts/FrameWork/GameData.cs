using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class GameData
{
    public bool gameover;

    public LevelComponent level;
    public PlayerComponent player;
    public CameraControllerComponent cameraController;
    public WeaponComponent currentWeapon;

    [Header("Dictionaries and Lists")]
    public Dictionary<Transform, EnemyComponent> enemies = new Dictionary<Transform, EnemyComponent>();
    public List<BulletComponent> bullets = new List<BulletComponent>();

    [Header("Systems")]
    public PoolSystem pool;
    public SaveLoadSystem saveLoad;

    public bool PointerOverUI()
    {
        if (EventSystem.current == null)
            return false;

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public Action Kill;
}
