using UnityEngine;

public class EnemySpawnPointComponent : MonoBehaviour
{
    [SerializeField] Renderer renderer;

    public Vector3 position => transform.position;
    public Quaternion rotation => transform.rotation;

    public void Calculate()
    {
        renderer = GetComponent<Renderer>();
    }
    public bool IsVisible()
    {
        return renderer.isVisible;
    }
}
