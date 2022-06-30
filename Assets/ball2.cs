using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ball2 : MonoBehaviour
{
    private int SpX;
    private int SpZ;
    private int CalSp = 0;
    private int i = 0;
    private int t = 0;
    [SerializeField] private int maxSphere = 10;

    public GameObject GameOver1;

    // Start is called before the first frame update
    void Start()
    {
        // コルーチンの起動
        StartCoroutine(DelayCoroutine());
        GameOver1 = GameObject.Find("GameOver");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // コルーチン
    private IEnumerator DelayCoroutine()
    {
        while (CalSp < maxSphere)
        {
            yield return new WaitForSeconds(3);
            InsSphere();
            //Debug.Log("BBBBBBBBBBBBBBBB");
            CalSp += 1;
        }
        yield return new WaitForSeconds(15);
        setcanvasonoff();
    }


    [SerializeField, Tooltip("出現させるオブジェクトをここに割り当てる")]
    private GameObject PlacementObject;




    /// <summary>
    /// Sphereを配置
    /// </summary>
    private void InsSphere()
    {
        SpX = Random.Range(-15, 15);
        SpZ = Random.Range(20, 40);
        GameObject Sphere1 = Instantiate(PlacementObject, new Vector3(SpX, 4.0f, SpZ), Quaternion.identity);
        Sphere1.name = "SphereClone" + CalSp + "(2)";
        StartCoroutine(DelayThat());

    }

    private IEnumerator DelayThat()
    {
        yield return new WaitForSeconds(20);

        //GameObjectを全て取得
        var gameObjectList = Resources.FindObjectsOfTypeAll<GameObject>();

        //取得したGameObjectの中からSphereCloneを探す
        foreach (var go in gameObjectList)
        {
            if (go.name == "SphereClone" + i + "(2)")
            {
                Debug.Log(go + "を消しました。");
                GameObject CL = go;
                for (t = 1; t <= 10; t = t + 1)
                {
                    CL.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    yield return new WaitForSeconds(0.1f);
                }
                CL.gameObject.SetActive(false);
            }
        }
        i++;
    }

    private void setcanvasonoff()
    {
        GameOver1.GetComponent<CanvasActive>().GameOver();
    }
}
