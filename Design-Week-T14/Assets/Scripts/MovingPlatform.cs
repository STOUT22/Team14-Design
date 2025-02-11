using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Calling variables
    public Transform movingPlatform;
    public Transform startPosition;
    public Transform endPosition;

    int direction = 1;
    public float speed = 1.5f;

    private void Update()
    {
        Vector2 target = currentMovementTarget();

        movingPlatform.position = Vector2.Lerp(movingPlatform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)movingPlatform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return startPosition.position;
        }
        else
        {
             return endPosition.position;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the lines the platforms are moving too and from
        if (movingPlatform != null && startPosition != null && endPosition != null)
        {
            Gizmos.DrawLine(movingPlatform.transform.position, startPosition.position);
            Gizmos.DrawLine(movingPlatform.transform.position, endPosition.position);
        }
    }
}
