using UnityEngine;
using UnityEngine.Tilemaps;

public class WrapAroundScreen : MonoBehaviour
{
    private Rigidbody2D _rb;
    public LayerMask layerMask;
    public Tilemap tilemap;
    [SerializeField]
    private Door _door;

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
                if (CheckAll()) {
                    transform.position = new Vector2(leftBound, transform.position.y);
                }
            }
            else if (transform.position.x <= leftBound && _rb.velocity.x < 0)
            {
                if (CheckAll())
                {
                    transform.position = new Vector2(rightBound, transform.position.y);
                }
            }
        }
    //Want all true
    private bool CheckAll() {
        return ((!CheckForObject() && CheckForTerrain() && VelocityCheck()) || !PlayerManager.Instance.CanLowerHP);
    }

    //returns true if there is something there
    private bool CheckForTerrain()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(new Vector2(-transform.position.x, transform.position.y));
        //Debug.Log($"Checking tile position {cellPosition} from {-transform.position.x}, {transform.position.y + 1}, does it exist: {tilemap.GetTile(cellPosition) != null}");
        return tilemap.GetTile(cellPosition) == null;
    }

    //returns true if there is something there
    private bool CheckForObject()
    {
        return MapManager.Instance.CheckVectorList(new Vector2(-transform.position.x, transform.position.y));
    }

    //need to check for tiles above if rb.v.y > 0, it is below going up
    private bool VelocityCheck() {
        bool goingUp = PlayerManager.Instance.prb.velocity.y > 0;
        bool zeroed = (PlayerManager.Instance.player.GetComponent<Movement>().isGrounded);
        if (zeroed) {
            //Debug.Log("no vertical velocity");
            return true;
        } else if (goingUp)
        {
            //Debug.Log($"Checking going up at {tilemap.WorldToCell(new Vector2(-transform.position.x, transform.position.y + 1))} from {new Vector2(-transform.position.x, transform.position.y)}");
            Vector3Int cellPosition = tilemap.WorldToCell(new Vector2(-transform.position.x, transform.position.y - 1));
            return tilemap.GetTile(cellPosition) == null;
        }
        else if (!goingUp)
        {
            //Debug.Log($"Checking going down at {tilemap.WorldToCell(new Vector2(-transform.position.x, transform.position.y - 1))} from {new Vector2(-transform.position.x, transform.position.y)}");
            Vector3Int cellPosition = tilemap.WorldToCell(new Vector2(-transform.position.x, transform.position.y + 1));
            return tilemap.GetTile(cellPosition) == null;
        }
        else {
            return true;
        }
    }
}

