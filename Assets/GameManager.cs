using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [System.NonSerialized]
    public int currentStageNum = 0; //現在のステージ番号（0始まり）

    [SerializeField]
    string[] stageName; //ステージ名

    void Start()
    {
        //シーンを切り替えてもこのゲームオブジェクトを削除しないようにする
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {

    }

    //次のステージに進む処理
    public void NextStage()
    {
        currentStageNum += 1;

        //コルーチンを実行
        StartCoroutine(WaitForLoadScene());
    }

    //メニュー画面に戻る処理
    public void BackToMenu()
    {
        currentStageNum = 0;

        //コルーチンを実行
        StartCoroutine(WaitForLoadScene());
    }

    //シーンの読み込みと待機を行うコルーチン
    IEnumerator WaitForLoadScene()
    {
        //シーンを非同期で読込し、読み込まれるまで待機する
        yield return SceneManager.LoadSceneAsync(stageName[currentStageNum]);
    }


}