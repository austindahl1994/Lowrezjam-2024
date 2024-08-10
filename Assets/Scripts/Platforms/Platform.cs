using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] DimensionsSO dim;

    protected virtual void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        MapManager.Instance.AddToVectorList(transform.position, dim.x, dim.y);
    }
}
