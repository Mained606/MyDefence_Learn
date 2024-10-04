using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MyDefence
{
    //Enemy 스폰을 관리하는 클래스 
    public class SpawnManager : MonoBehaviour
    {
        //필드
        #region Variable
        //enemy 프리팹
        public GameObject enemyPrefab;

        //Wave 데이터 셋팅
        public Wave[] waves;

        //스폰 위치(시작 위치)
        public Transform startPoint;

        //스폰 타이머
        public float spawnTimer = 5f;
        private float countdown = 0f;

        //웨이브 카운트
        private int waveCount = 0;

        //텍스트 UI
        public TextMeshProUGUI countdownText;
        //스폰중이면 true, 아니면 false
        bool isSpawn = false;       //

        //현재 맵상에서 살아있는 적의 수
        public static int enmeyAlive = 0;

        //GameManager 컴포넌트 참조
        public GameManager gameManager;

        //모든 웨이브가 스폰되었는지 확인하는 변수
        private bool allWavesSpawned = false;

        //노드 참조
        public Node node;
        private EnemyMove enemyMove;
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            enmeyAlive = 0;

            //초기화 - 시작시 대기 시간
            countdown = 2f;
            waveCount = 0;

            //시작지점 위치에  Enemy 1개를 생성
            //SpawnEnemy();
        }

        // Update is called once per frame
        void Update()
        {
            //맵상에 적이 살아 있으면
            if (enmeyAlive > 0)
                return;

            //모든 웨이브가 스폰되었고, 적이 없으면 게임 클리어 처리
            if (allWavesSpawned && enmeyAlive == 0)
            {
                gameManager.LevelClear();
                Debug.Log("Level Clear");
                this.enabled = false;
                return;
            }

            if (countdown <= 0f)
            {
                //타이머 명령
                StartCoroutine(SpawnWave());

                //초기화
                countdown = spawnTimer;
            }

            if (isSpawn == false)
            {
                countdown -= Time.deltaTime;
                countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            }
            //UI            
            countdownText.text = Mathf.Round(countdown).ToString();

        }

        //시작지점 위치에 Enemy를 생성
        private void SpawnEnemy(GameObject prefab)
        {
            // Instantiate(prefab, startPoint.position, Quaternion.identity);
            //웨이포인트 바라보며 생성
            Vector3 dir = (node.transform.position - startPoint.position).normalized;
            //노드가 없으면 정방향으로 이동
            if (dir == Vector3.zero)
            {
                dir = Vector3.forward;
            }
            //회전  
            Quaternion rotation = Quaternion.LookRotation(dir);
            //생성
            GameObject enemy = Instantiate(prefab, startPoint.position, rotation);
            //EnemyMove 컴포넌트 참조
            enemyMove = enemy.GetComponent<EnemyMove>();

            //노드 설정
            //노드의 자식 노드가 1개면 그 노드로 이동
            if (node.nextNodes.Length == 1)
            {
                enemyMove.SetTarget(node.nextNodes[0]);
            }
            //노드의 자식 노드가 2개 이상이면 랜덤으로 이동
            else
            {
                enemyMove.SetTarget(node.nextNodes[Random.Range(0, node.nextNodes.Length)]);
            }

            enmeyAlive++;
        }

        //웨이브 할때마다 1마리씩 추가 스폰  1 - 2 - 3 - 4 - 5..
        //Wave 데이터에 맞게 적 스폰
        IEnumerator SpawnWave()
        {
            isSpawn = true;
            //Debug.Log($"waveCount: {waveCount}");

            Wave wave = waves[waveCount];

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemyPrefab);
                yield return new WaitForSeconds(wave.delayTime);
            }

            waveCount++;
            PlayerStats.Rounds++;

            isSpawn = false;

            //마지막 웨이브 체크해서 스폰기능 비활성화
            if (waveCount >= waves.Length)
            {
                allWavesSpawned = true; //모든 웨이브 스폰 확인
            }
        }

        public void Restart()
        {
            countdown = 2f;
            waveCount = 0;
            enmeyAlive = 0;
            allWavesSpawned = false;
        }
    }
}

/*
유니티의 필드 초기화
1. 변수 선언시 값 초기화         spawnTimer = 5f;
2. public 을 선언후 인스펙터 창에서 값 초기화  spawnTimer = 3f;
3. Start() 함수에서 변수 값 초기화    spawnTimer = 4f;
spawnTimer : 5f -> 3f -> 4f
*/