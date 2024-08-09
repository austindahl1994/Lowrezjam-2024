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
            if (CheckForTerrain() && CheckForObject()) {
                transform.position = new Vector2(leftBound, transform.position.y);
            }
        }
        else if (transform.position.x <= leftBound && _rb.velocity.x < 0)
        {
            if (CheckForTerrain() && CheckForObject())
            {
                transform.position = new Vector2(rightBound, transform.position.y);
            }
        }
    }

    private bool CheckForTerrain() {
        Vector3Int cellPosition = tilemap.WorldToCell(new Vector2(-transform.position.x, transform.position.y));
        return tilemap.GetTile(cellPosition) == null;
    }

    //check for monsters added to dictionary for their positions/widths
    private bool CheckForObject() {
        return true; 
    }

    //check to see if player is inside terrain
    private void CheckInTerrain(Vector2 currentPos) {
        
    }
    //If inside terrain or monster, can go down, if not able to (at bottom of map), then go next highest up
    private void ClosestTerrainExit(Vector2 currentPos) {
        
    }
}
