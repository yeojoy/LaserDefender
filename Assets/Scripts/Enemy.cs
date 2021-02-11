using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.9f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;

    private Vector2 velocity;

    // Start is called before the first frame update
    void Start() {
        velocity = new Vector2(0, -projectileSpeed);
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update() {
        CountDownAndShoot();
    }

    private void CountDownAndShoot() {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f) {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire() {
        Player player = FindObjectOfType<Player>();

        if (player != null) {

            GameObject laser = Instantiate(
                projectile,
                transform.position,
                Quaternion.identity) as GameObject;

            Vector2 velocity = player.transform.position - transform.position;

            laser.GetComponent<Rigidbody2D>().velocity = velocity;
            //laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(collision.gameObject.GetComponent<DamageDealer>());
    }

    private void ProcessHit(DamageDealer damageDealer) {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
