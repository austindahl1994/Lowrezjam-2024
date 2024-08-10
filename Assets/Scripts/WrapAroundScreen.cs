using UnityEngine;
using UnityEngine.Tilemaps;

public class WrapAroundScreen : MonoBehaviour
{
    private Rigidbody2D _rb;
    public LayerMask layerMask;
    public Tilemap tilemap;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Debug.Log(transform.position.x);
        //Debug.Log(oppositePosition.x);
        float rightBound = 3.62f;
        float leftBound = -3.62f;

            if (transform.position.x >= rightBound && _rb.velocity.x > 0)
            {
                if (!CheckForObject() && CheckForTerrain()) {
                    transform.position = new Vector2(leftBound, transform.position.y);
                }
            }
            else if (transform.position.x <= leftBound && _rb.velocity.x < 0)
            {
                if (!CheckForObject() && CheckForTerrain())
                {
                    transform.position = new Vector2(rightBound, transform.position.y);
                }
            }
        }
    }

    //returns true if there is nothing
    private bool CheckForTerrain()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(new Vector2(-transform.position.x, transform.position.y));
        return tilemap.GetTile(cellPosition) == null;
    }

    //returns true if there is something there
    private bool CheckForObject()
    {
        return MapManager.Instance.CheckVectorList(new Vector2(-transform.position.x, transform.position.y));
    }
}
