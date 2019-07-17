using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{

    public GameObject point;        //화살의 머리 부분
    public GameObject end;          // 화살의 끝부분
    public GameObject ob_arrow;        // 화살 객체

    private GameObject debug_end;       // end객체의 위치를 파악하기 위한 디버그 Text 창

    bool arrow_ready = false;
    float distance;

    private float shootRate = 0.5f;     // 화살 지연 시간 설정
    private float nextShoot = 0.0f;      // 다음 화살 발사시간

    Transform point_transform;          // 화살 포인트의 트랜스폼 정보
    Vector3 new_position;               // 화살의 새로운 위치
    Vector3 point_position;             // 화살 포인트의 위치
    Quaternion point_quaternion;        // 화살 포인트의 각도
    Transform end_transform;            // 화살 끝의 트랜스폼 정보
    Vector3 end_position;               // 화살 끝의 위치 정보

    // Start is called before the first frame update
    void Start()
    {
        point = GameObject.Find("point");
        end = GameObject.Find("end");
        ob_arrow = GameObject.FindGameObjectWithTag("arrow");           // 화살.

        debug_end = GameObject.FindGameObjectWithTag("debug_text");     // 디버그용 텍스트 창
    }

    // Update is called once per frame
    void Update()
    {
        // Point의 위치정보를 얻고 그걸 바탕으로 화살의 위치를 변경시킨다. 
        // 또한 Point의 각도를 계산하여 화살의 각도를 같이 변경도 시킨다.
        point_transform = point.GetComponent<Transform>();        // point의 위치정보 얻기.
        point_position = point_transform.position;
        new_position = point_position + new Vector3(0, 0, 1);
        ob_arrow.GetComponent<Transform>().position = new_position;

        // 각도 계산
        point_quaternion = point.GetComponent<Transform>().rotation;
        ob_arrow.GetComponent<Transform>().rotation = point_quaternion;


        // End의 위치정보를 얻어내고 디버깅 정보를 모은다. (화살 발사를 위해서)
        end_transform = end.GetComponent<Transform>();
        end_position = end_transform.position;

        distance = PointToEndDistance(point_position, end_position);
        string debugText = string.Format("end - point : {0}", distance); // 위치정보를 뽑아내어 텍스트를 만들어낸다.

        DebugText(debugText);


        ShootArrow();
    }

    void ShootArrow()
    {
        //  화살 발사 
        if (!arrow_ready && distance > 3.2 && Time.time > nextShoot)
        {
            arrow_ready = true;
        }
        else if (arrow_ready && distance <= 1.7)
        {
            nextShoot = Time.time + shootRate;
            arrow_ready = false;
            GameObject arrow_clone;
            arrow_clone = Instantiate(ob_arrow, new_position, point_quaternion);
            //arrow_clone.GetComponent<Rigidbody>().velocity = transform.TransformVector(point_position.normalized * 10);
            arrow_clone.GetComponent<Rigidbody>().AddForce(point_position.normalized * 3000);
        }
    }

    private float PointToEndDistance(Vector3 point, Vector3 end)
    {
        float x = point.x - end.x;
        float y = point.y - end.y;
        float z = point.z - end.z;

        float total = x * x + y * y + z * z;
       // float total = x * x + y * y;
        float dis = Mathf.Sqrt(total);

        //float dis = point.y - end.y;
        return dis;
    }

    private void DebugText(string str)
    {
       
        debug_end.GetComponent<UnityEngine.UI.Text>().text = str;/// end 의 위치 텍스트 표시
    }
}
