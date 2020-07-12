using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform spawnerPoint;
    [SerializeField] private Transform spawnPoint; 
    
    private List<Transform> spawnerPoints;
    private List<Transform> spawnPoints;

    #region singleton
    public static EnemySpawner instance { get; private set; }

    void Awake()
    {
        instance = this;
    }
    #endregion

    private int round = 1;
    private int currentQuantity = 0;
    private int extraPerRound = 1;
    private int enemyQuantityPerRound = 2;
    
    void Start()
    {
        spawnerPoints = spawnerPoint.GetComponentsInChildren<Transform>().ToList();
        spawnPoints = spawnPoint.GetComponentsInChildren<Transform>().ToList();
    }

    void Update()
    {
        if (currentQuantity == 0)
        {
            GameManager.instance.ShowRound(round);
            enemyQuantityPerRound += extraPerRound;
            StartCoroutine(Spawn());
            round++;
        }
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < enemyQuantityPerRound; i++)
        {
            Transform _spawnPoint = spawnerPoints[Random.Range(0, spawnerPoints.Count)];
            currentQuantity++;
            Enemy enemyCreated = Instantiate(enemyPrefab, _spawnPoint.position, Quaternion.identity);
            enemyCreated.Guy.onKill = () => currentQuantity--;
            int random = Random.Range(0, spawnPoints.Count - 1);
            enemyCreated.SetTarget(spawnPoints[random]);
            yield return new WaitForSeconds(1);
        }
    }
}