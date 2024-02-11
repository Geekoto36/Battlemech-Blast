using UnityEngine;

public class PowerUps : MonoBehaviour
{
    void Update()
    {
        CheckCollider();
    }


    private float checkRaduis = .5f;

    void CheckCollider()
    {

        Collider2D collision = Physics2D.OverlapCircle(transform.position, checkRaduis);

        if (collision.name == "Tilemap_Bounds")
        {
            Destroy(gameObject);

        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRaduis);
    }

}
