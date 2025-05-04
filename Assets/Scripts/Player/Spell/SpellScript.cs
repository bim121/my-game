using UnityEngine;

public class SpellScript : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    [SerializeField] private float speed = 5f;  
    [SerializeField] private float maxRange = 50f;  
    [SerializeField] private int damage = 1; 

    private Vector3 moveDirection;

    private Vector3 startPosition;

    private float screenLeft, screenRight, screenTop, screenBottom;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        startPosition = transform.position;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 

        moveDirection = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void FixedUpdate()
    {
        transform.position += moveDirection * speed * Time.fixedDeltaTime;

        if (Vector3.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") return;
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(1);
        Destroy(gameObject);
    }
}
