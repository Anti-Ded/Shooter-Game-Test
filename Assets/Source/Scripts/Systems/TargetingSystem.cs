using UnityEngine;

public class TargetingSystem : GameSystem
{
    [SerializeField] LayerMask groundMask;

    public override void OnUpdate()
    {
        if(!game.player.isDead)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hit, 50, config.groundMask))
            {
                Vector3 pos = hit.point;
                pos.y = game.player.transform.position.y;
                game.player.Targeting(pos);
            }
        }
    }
}
