using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpawnController : MonoBehaviour
{
    [SerializeField] private Rectangle rectanglePrefab;
    [SerializeField] private RectTransform parentRectangles;


    public void SpawnRectangle(Vector2 postion)
    {
        if (!IsSpawnRectangle(postion))
        {
            Rectangle rectangle = Instantiate(rectanglePrefab, postion, Quaternion.identity, parentRectangles);
        }
    }

    public bool IsSpawnRectangle(Vector2 position)
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(position, 112f, Vector2.zero);

        return raycastHit2D.collider ? true : false;
    }

    public bool IsRaycastHitRectangle(Vector2 position)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(position, Vector2.zero);

        return raycastHit2D.collider ? true : false;
    }

    public Rectangle GetRectangle(Vector2 position)
    {
        return Physics2D.Raycast(position, Vector2.zero).collider.GetComponent<Rectangle>();
    }
}
