using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    // スコアを表示する
    public Text scoreText;
    // 残機を表示する
    public Text zankiText;
    // 必要なスコアを表示する
    public Text hitsuyouText;

    // スコア
    private float score;

    GameObject torus11;
    player script;

    // 残機
    private int zanki = 0;

    private int a;
    public int hitsuyouscore;

    void Start()
    {
        torus11 = GameObject.Find("torus11");
        script = torus11.GetComponent<player>();
        Initialize();
    }

    void Update()
    {
        // スコア・ハイスコアを表示する
        scoreText.text = score.ToString();
        zankiText.text = zanki.ToString();
        hitsuyouText.text = hitsuyouscore.ToString();
        if (score >= hitsuyouscore)
        {
            if (a == 0)
            {
                FindObjectOfType<GameManager>().NextStage();
            }
            a = 1;
        }


        // 
        zanki = script.remain;


    }

    // ゲーム開始前の状態に戻す
    private void Initialize()
    {
        // スコアを0に戻す
        score = 0;

        // ハイスコアを取得する。保存されてなければ0を取得する。
    }

    // ポイントの追加
    public void AddPoint(float point)
    {
        score = score + point;
    }

}