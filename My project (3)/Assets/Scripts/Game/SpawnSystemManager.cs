using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 공중 몹에 대한 스폰시스템 제작하기
 * 일반양과 주기, 엘리트양 저장
 * 제작하면 주기적으로 랜덤한 위치에 생성하도록 만들기
 * (오브젝트 주변의 사각틀 영역)
 * 
 * 퍼블릭으로는 스폰시스템 추가하는 것만 만들기
 * 
 * 지상몹도 추가하고 싶다면 타입지정 or 메소드를 여러개로
 * 
 */

public class SpawnSystemManager : MonoBehaviour
{
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
        if (GameTimer > 0 && maxIdx == -1)
        {
            addSpawnSystem(4, 0, 4f);
        }
        else if (GameTimer > 10 && maxIdx == 0)
        {
            addSpawnSystem(2, 1, 5f);
        }
        else if (GameTimer > 20 && maxIdx == 1)
        {
            addSpawnSystem(6, 1, 6f);
        }
    }
}
