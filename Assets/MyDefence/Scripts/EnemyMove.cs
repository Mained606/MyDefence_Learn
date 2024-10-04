using UnityEngine;

namespace MyDefence
{
    //Enemy의 이동을 관리하는 클래스
    public class EnemyMove : MonoBehaviour
    {
        //필드
        #region Variable
        [SerializeField] private float speed;   //이동 속도
        [SerializeField] private float startSpeed = 3f; //이동 시작 속도


        // private Transform target;   //이동할 목표지점        

        // private int wayPointIndex = 0;  //wayPoints 배열을 관리하는 인덱스

        //노드 인덱스
        public Node node;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //초기화
            speed = startSpeed;

            //첫번째 목표지점 셋팅
            // wayPointIndex = 0;
            // target = WayPoints.points[wayPointIndex];

        }

        private void Update()
        {
            Move();

            //속도 복원
            speed = startSpeed;
        }

        //이동
        public void Move()
        {

            // //이동 방향(dir)
            Vector3 dir = node.transform.position - this.transform.position;
            // //타겟을 바라보면서 이동(회전)
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir).normalized, Time.deltaTime * speed);
            //이동
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            //도착판정
            float distance = Vector3.Distance(transform.position, node.transform.position);
            if (distance < 0.2f)
            {
                SetNextTarget();
            }
        }
        //노드 설정 함수
        public void SetTarget(Node node)
        {
            this.node = node;
        }

        //다음 목표 지점 셋팅
        void SetNextTarget()
        {
            //노드가 없거나 노드의 자식 노드가 없으면 도착
            //IndexOutOfRange 방지
            if (node.nextNodes == null || node.nextNodes.Length == 0)
            {
                Arrive();
                return;
            }

            //노드의 자식 노드가 1개면 그 노드로 이동
            if (node.nextNodes.Length == 1)
            {
                this.node = node.nextNodes[0];
            }
            //노드의 자식 노드가 2개 이상이면 랜덤으로 이동
            else
            {
                this.node = node.nextNodes[Random.Range(0, node.nextNodes.Length)];
            }

        }

        //기존 목표지점 셋팅 방법
        // void SetNextTarget()
        // {
        //     if (wayPointIndex == WayPoints.points.Length - 1)
        //     {
        //         Arrive();
        //         return;
        //     }

        //     wayPointIndex++;
        //     target = WayPoints.points[wayPointIndex];
        // }


        //목표지점 도착 처리
        void Arrive()
        {
            //Debug.Log("종점 도착");
            PlayerStats.UseLives(1);

            //살아있는 적 카운팅
            SpawnManager.enmeyAlive--;

            //게임 오브젝트 kill
            Destroy(this.gameObject);
            //
            //Debug.Log("과금");
            //isPaid = true;
        }

        public void Slow(float rate)
        {
            speed = startSpeed * (1.0f - rate);
        }

        //public void SetTarget(Node node)
        //{
        //    this.nodes = node;
        //}

    }
}