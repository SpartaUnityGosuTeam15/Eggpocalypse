using UnityEngine;

public class HasPosition : MonoBehaviour
{
    public Vector2 Position => new Vector2(transform.position.x, transform.position.z);
}
