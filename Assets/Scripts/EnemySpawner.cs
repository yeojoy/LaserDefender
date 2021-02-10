using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    
    IEnumerator Start() {
        do {

            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves() {
        for (int waveIndex = 0, maxWaves = waveConfigs.Count; waveIndex < maxWaves; waveIndex++) {
            var currentWaveConfig = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWaveConfig));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {

        for (int i = 0, maxEnemies = waveConfig.GetNumberOfEnemies(); i < maxEnemies; i++) {
            GameObject newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }
}
