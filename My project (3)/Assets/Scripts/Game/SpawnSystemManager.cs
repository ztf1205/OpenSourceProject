using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 공중 몹에 대한 스폰시스템 제작하기
 * 일반양과 주기, 엘리트양 저장
 * 제작하면 주기적으로 랜덤한 위치에 생성하도록 만들기
 * (플레이어 주변의 사각틀 영역)
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


    public GameObject enemyNormal;
    public GameObject enemyElite;


    private GameObject player;
    private List<SpawnSystem> spawnSystems;
    private int maxIdx = -1;

    [SerializeField]
    private float width = 5f;//사각틀의 폭
    [SerializeField]
    private float offset = 5f;//사각틀의 중점으로부터의 오프셋


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
        player = GameObject.FindWithTag("Player");
        spawnSystems = new List<SpawnSystem>();
        addSpawnSystem(1, 1, 0.1f);
    }
    private void Update()
    {
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
        int dir;//상하좌우 방향 설정을 위한 변수
        float centerX = player.transform.position.x;
        float centerY = player.transform.position.y;


        //노말 스폰
        for (int i = 0; i < normalSpawn; i++)
        {
            dir = Random.Range(0, 8);
            GameObject enemy = Instantiate(enemyNormal);
            if (dir == 0)//하
            {
                enemy.transform.position = new Vector2(Random.Range(centerX - width - offset, centerX + width + offset),
                    Random.Range(centerY - offset, centerY - width - offset));
            }
            else if (dir % 3 == 0)//상
            {
                enemy.transform.position = new Vector2(Random.Range(centerX - offset, centerX + offset),
                    Random.Range(centerY + width + offset, centerY + offset));
            }
            else if (dir % 3 == 1)//좌
            {
                enemy.transform.position = new Vector2(Random.Range(centerX - width - offset, centerX - offset),
                    Random.Range(centerY + width + offset, centerY - offset));
            }
            else if (dir % 3 == 2)//우
            {
                enemy.transform.position = new Vector2(Random.Range(centerX + offset, centerX + width + offset),
                    Random.Range(centerY + width + offset, centerY - offset));
            }

        }

        //엘리트 스폰
        for (int i = 0; i < eliteSpawn; i++)
        {
            dir = Random.Range(0, 8);
            GameObject enemy = Instantiate(enemyElite);
            if (dir == 0)//하
            {
                enemy.transform.position = new Vector2(Random.Range(centerX - width - offset, centerX + width + offset),
                    Random.Range(centerY - offset, centerY - width - offset));
            }
            else if (dir % 3 == 0)//상
            {
                enemy.transform.position = new Vector2(Random.Range(centerX - offset, centerX + offset),
                    Random.Range(centerY + width + offset, centerY + offset));
            }
            else if (dir % 3 == 1)//좌
            {
                enemy.transform.position = new Vector2(Random.Range(centerX - width - offset, centerX - offset),
                    Random.Range(centerY + width + offset, centerY - offset));
            }
            else if (dir % 3 == 2)//우
            {
                enemy.transform.position = new Vector2(Random.Range(centerX + offset, centerX + width + offset),
                    Random.Range(centerY + width + offset, centerY - offset));
            }
        }
    }
}
