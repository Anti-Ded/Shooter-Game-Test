using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarFrame : MonoBehaviour
{
    [SerializeField] Image healthFillerImage;
    [SerializeField] Text healthText;
    GameData game;
    EnemyComponent enemy;
    public void Prestart(GameData game, EnemyComponent enemy)
    {
        this.game = game;
        this.enemy = enemy;
    }
    public void LateUpd()
    {
        if (enemy.GetCurrentHealth() == enemy.enemyData.health || enemy.isDead)
            transform.localScale = Vector3.zero;
        else
        {
            transform.localScale = Vector3.one;

            healthText.text = enemy.GetCurrentHealth().ToString("F0");
            healthFillerImage.fillAmount = enemy.GetCurrentHealth() / enemy.enemyData.health;

            transform.position = game.cameraController.camera.WorldToScreenPoint(enemy.transform.position);
        }
    }
}
