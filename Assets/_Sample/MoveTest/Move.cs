//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Move : MonoBehaviour
//{
//    public Node node;
//    public float speed = 10;
//    // Start is called before the first frame update
//    void Start()
//    {
//        sc ssc = GetComponent<sc>();
//    }

//    public void SetTarget(Node node)
//    {
//        this.node = node;
//    }

//    void Update()
//    {
//        if (node != null)
//        {
//            Vector3 dir = node.nextNode.transform.position - transform.position;
//            transform.Translate(dir.normalized * Time.deltaTime * speed);
//            if (Vector3.Distance(transform.position, node.transform.position) < 0.1f)
//            {
//                node = node.nextNode;
//            }
//        }

//    }
//}
