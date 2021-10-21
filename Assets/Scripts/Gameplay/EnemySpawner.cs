using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.AI;
using Scripts.Managers;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 minSpawnZone;
    [SerializeField] private Vector2 maxSpawnZone;

    private System.IDisposable timer;

    private void OnEnable()
    {
        EventManager.OnEndGame += OnEndGame;
    }

    private void Start()
    {
        timer =  Observable.Timer(System.TimeSpan.FromSeconds(3f)).Repeat().TakeUntilDestroy(this).Subscribe(_ => Spawn());
    }

    private void OnDestroy()
    {
        EventManager.OnEndGame -= OnEndGame;
    }

    private void OnEndGame()
    {
        timer.Dispose();
    }


    private void Spawn()
    {
        Vector3 position = CalculatePointSpawn();
        
        EnemyController enemy = PoolManager.Spawn(enemyPrefab.name, position, Quaternion.identity) as EnemyController;
        enemy.SetTarget(GameManager.Player.transform);
    }

    private Vector3 CalculatePointSpawn()
    {
        float dot = 0;
        Vector3 position;
        Transform camera = Camera.main.transform;

        do
        {
            position = new Vector3(Random.Range(minSpawnZone.x, maxSpawnZone.x), 1f, Random.Range(minSpawnZone.y, maxSpawnZone.y));
            Vector3 direction = position - camera.position;

            dot = Vector3.Dot(camera.forward, position - camera.position);

            if (direction.magnitude < 6f)
                dot = -1;

            RaycastHit rayHit;
            if (direction.magnitude > 6f && Physics.Raycast(camera.position, direction, out rayHit, direction.magnitude))
                break;

        } while (dot > 0);


        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 5.0f, NavMesh.AllAreas))
        {
            position = new Vector3(hit.position.x, position.y, hit.position.z);
        }

        return position;
    }
}
