using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    private WaveConfig waveConfig;
    float moveSpeed;

    private List<Transform> waypoints;
    private int waypointIndex = 0;

    // Start is called before the first frame update
    void Start() {
        // Initailize();
    }

    // Update is called once per frame
    void Update() {
        Move(); 
    }

    public void SetWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;

        Initailize();
    }

    private void Initailize() {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
        moveSpeed = waveConfig.GetMoveSpeed();
    }

    private void Move() {
        if (waypoints != null && waypointIndex <= waypoints.Count - 1) {
            var targetPosiiton = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,
                targetPosiiton, movementThisFrame);

            if (transform.position == targetPosiiton) {
                waypointIndex++;
            }

        } else {
            Destroy(gameObject);
        }
    }
}
