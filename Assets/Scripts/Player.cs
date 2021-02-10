using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    // configuration parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    private float xMin, xMax;
    private float yMin, yMax;

    private Coroutine firingCoroutine;
    // Start is called before the first frame update
    void Start() {
        SetUpMoveBoundaries();
        // Test to call coroutine method.
        // StartCoroutine(PrintAndWait());
    }


    // Update is called once per frame
    void Update() {
        Move();
        Fire();
    }


    /**
     * This is for test Coroutine
     */
    IEnumerator PrintAndWait() {
        Debug.Log("First message sent, boss");
        yield return new WaitForSeconds(3);
        Debug.Log("Second message sent, after 3 seconds.");
        yield return new WaitForSeconds(3);
        Debug.Log("Third message sent, after 6 seconds.");
    }

    private void Fire() {
        if (Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously() {
        while (true) {
            GameObject laser =
                    Instantiate(laserPrefab, transform.position, Quaternion.identity)
                    as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move() {
        /* The reason why we should use the Time.deltaTime is to equal the
         * game speed. But it's too slow to play, so use moveSpeed to get back
         * to normal.
         */
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);

    }

    private void SetUpMoveBoundaries() {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
