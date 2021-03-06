using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystemManager : MonoBehaviour
{

    private int timeStat = -1;

    //주기적으로 스폰하는 스폰 시스템 추가
    public void addSpawnSystem(int normalSpawn, int eliteSpawn, float spawnCycle)
    {
        maxIdx++;
        spawnSystems.Add(new SpawnSystem(normalSpawn, eliteSpawn, spawnCycle));
    }


    public GameObject enemyAir;

    private List<SpawnSystem> spawnSystems;
    private int maxIdx = -1;

    private float width = 78f;//스폰지역 너비
    private float height = 38f;//스폰지역 높이

    private float GameTimer = 0f;//게임 시간 저장
    private float NormalCycleTimer = 0f;//노말 추가 싸이클 타이머
    private float EliteCycleTimer = 0f;//노말 추가 싸이클 타이머
    private float WaitingTimer = 0f; // 스폰시 대기 시간


    private class SpawnSystem
    {
        public int normalSpawn;
        public int eliteSpawn;
        public float spawnCycle;
        public float timer = 0f;

        public SpawnSystem(int normalSpawn, int eliteSpawn, float spawnCycle)
        {
            this.normalSpawn = normalSpawn;
            this.eliteSpawn = eliteSpawn;
            this.spawnCycle = spawnCycle;
        }

    }

    private void Awake()
    {
        spawnSystems = new List<SpawnSystem>();
    }

    private void Update()
    {
        //시간 갱신
        GameTimer += Time.deltaTime;
        NormalCycleTimer += Time.deltaTime;
        EliteCycleTimer += Time.deltaTime;
        WaitingTimer += Time.deltaTime;
        //시간에 따라서 스폰 추가
        SpawnTimeLine();
        //생성된 스폰시스템에 따라서 스폰
        for (int idx = 0; idx <= maxIdx; idx++)
        {
            spawnSystems[idx].timer += Time.deltaTime;
            if (spawnSystems[idx].timer >= spawnSystems[idx].spawnCycle)
            {
                Spawn(spawnSystems[idx].normalSpawn, spawnSystems[idx].eliteSpawn);
                spawnSystems[idx].timer = 0f;
            }
        }
    }

    private void Spawn(int normalSpawn, int eliteSpawn)
    {
        //노말 스폰
        for (int i = 0; i < normalSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyAir);
            enemy.transform.position = new Vector2(Random.Range(transform.position.x - width / 2, transform.position.x + width / 2),
                    Random.Range(transform.position.y - height / 2, transform.position.y + height / 2));

        }

        //엘리트 스폰
        for (int i = 0; i < eliteSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyAir);
            enemy.transform.position = new Vector2(Random.Range(transform.position.x - width / 2, transform.position.x + width / 2),
                    Random.Range(transform.position.y - height / 2, transform.position.y + height / 2));
            enemy.GetComponent<EnemyMove>().isElite = true;
        }
    }

    private void SpawnTimeLine()
    {
        //노말 싸이클
        if (NormalCycleTimer > 15)
        {
            addSpawnSystem(2, 0, 5f);
            NormalCycleTimer = 0;
        }

        //엘리트 싸이클
        if (EliteCycleTimer > 20)
        {
            addSpawnSystem(0, 1, 7f);
            EliteCycleTimer = 0;
        }


        //타임라인
        if (GameTimer > 0 && timeStat == -1)
        {
            addSpawnSystem(4, 0, 4f);
            timeStat++;
        }
        else if (GameTimer > 20 && timeStat == 0)
        {
            addSpawnSystem(2, 1, 5f);
            timeStat++;
        }
        else if (GameTimer > 40 && timeStat == 1)
        {
            addSpawnSystem(6, 1, 6f);
            timeStat++;
        }
        else if (GameTimer > 60 && timeStat == 2)
        {
            addSpawnSystem(2, 0, 2f);
            timeStat++;
        }
        else if (GameTimer > 80 && timeStat == 3)
        {
            addSpawnSystem(4, 1, 3f);
            timeStat++;
        }
        else if (GameTimer > 100 && timeStat == 4)
        {
            addSpawnSystem(4, 0, 3f);
            addSpawnSystem(0, 1, 5f);
            timeStat++;
        }
        else if (GameTimer > 120 && timeStat == 5)
        {
            addSpawnSystem(3, 1, 3f);
            addSpawnSystem(2, 0, 1f);
            addSpawnSystem(0, 1, 2f);
            timeStat++;
        }
        else if (GameTimer > 140 && timeStat == 6)
        {
            addSpawnSystem(6, 1, 1f);
            timeStat++;
        }
    }
}
