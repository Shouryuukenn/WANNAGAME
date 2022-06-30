using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    /// <summary>
    /// 射出するオブジェクト
    /// </summary>
    [SerializeField, Tooltip("射出するオブジェクトをここに割り当てる")]
    private GameObject ThrowingObject;

    /// <summary>
    /// 標的のオブジェクト
    /// </summary>
    [SerializeField, Tooltip("標的のオブジェクトをここに割り当てる")]
    private GameObject TargetObject;

    /// <summary>
    /// 射出角度
    /// </summary>
    [SerializeField, Range(0F, 90F), Tooltip("射出する角度")]
    private float ThrowingAngle;

    private Dictionary<int, Vector3> myDictionary = new Dictionary<int, Vector3>();

    int balls = 0;
    float kyori = 0;

    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 10.0f;        // 移動速度
    public int zanki;
    public int remain;
    private string objName;

    public GameObject GameOver;

    private void Start()
    {
        GameOver = GameObject.Find("GameOver");
    }

    private void Update()
    {
        remain = zanki - balls + 1;
        // マウス入力に応じて射出
        if (Input.GetMouseButtonDown(0))
        {

            if (balls <= zanki)
            {
                // スペースキーでボールを射出する
                ThrowingBall();
                balls += 1;
            }
            else {
            setcanvasonoff();
            }
        }

        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        velocity = Vector3.zero;
        if (this.transform.position.z <= 20)
        {
            if (Input.GetKey(KeyCode.W))
                velocity.z += 100;
        }
        if (this.transform.position.x >= -25)
        {
            if (Input.GetKey(KeyCode.A))
                velocity.x -= 100;
        }
        if (this.transform.position.z >= 0)
        {
            if (Input.GetKey(KeyCode.S))
                velocity.z -= 100;
        }
        if (this.transform.position.x <= 25)
        {
            if (Input.GetKey(KeyCode.D))
                velocity.x += 100;
        }
        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;



        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            // プレイヤーの位置(transform.position)の更新
            // 移動方向ベクトル(velocity)を足し込みます
            transform.position += velocity;
        }


    }





    /// <summary>
    /// ボールを射出する
    /// </summary>
    private void ThrowingBall()
    {

        // Torusオブジェクトの生成
        GameObject torus11 = Instantiate(ThrowingObject, this.transform.position, Quaternion.Euler(90f, 0f, 0f));

        // オブジェクトの名前の変更
        torus11.name = "torusClone" + balls;

        // 標的の座標
        Vector3 targetPosition = TargetObject.transform.position;

        // 射出角度
        float angle = ThrowingAngle;

        // 射出速度を算出
        Vector3 velocity = CalculateVelocity(this.transform.position, targetPosition, angle);

        // 射出
        Rigidbody rid = torus11.GetComponent<Rigidbody>();
        rid.AddForce(velocity * rid.mass, ForceMode.Impulse);

        // このオブジェクトの座標をディクショナリーに加える
        Vector3 hassya = this.transform.position;
        myDictionary.Add(balls, hassya);
        UnityEngine.Debug.Log(string.Join(",", myDictionary.Select(n => n.ToString())));
    }

    // スコアの追加とオブジェクトの削除
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Respawn"))
        {
            UnityEngine.Debug.Log(string.Join(",", myDictionary.Select(n => n.ToString())));
            objName = this.gameObject.name;
            UnityEngine.Debug.Log(balls);

        
            Vector3 value;
            bool hasValue = myDictionary.TryGetValue(balls, out value);
            if (hasValue)
            {
                UnityEngine.Debug.Log(value);
            }
            else
            {
                UnityEngine.Debug.Log("Key not present");
            }
        

            float distance = Vector3.Distance(this.transform.position, other.transform.position);
            // UnityEngine.Debug.Log(distance);
            kyori = Mathf.Ceil((2.2f-distance) * 10);
            
            FindObjectOfType<Score>().AddPoint(kyori);
            this.gameObject.SetActive(false);

        }
        if (other.CompareTag("Wind"))
        {
            Rigidbody AddRid = this.gameObject.GetComponent<Rigidbody>();
            // UnityEngine.Debug.Log("It's OK");
            AddRid.AddForce(0.0f, 200.0f, 0.0f);
        }
    }
    // WindBallに当たった際の処理
    void OnTriggerStay(Collider other)
    {

    }






    /// <summary>
    /// 標的に命中する射出速度の計算
    /// </summary>
    /// <param name="pointA">射出開始座標</param>
    /// <param name="pointB">標的の座標</param>
    /// <returns>射出速度</returns>
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // 垂直方向の距離y
        float y = pointA.y - pointB.y;

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            //UnityEngine.Debug.Log(speed);
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);

        }

    }

    private void setcanvasonoff() 
    {
        GameOver.GetComponent<CanvasActive>().GameOver();
    }



}