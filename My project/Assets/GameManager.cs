using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject QuestionArea1;
    public GameObject QuestionArea2;
    public GameObject OperationArea;
    public GameObject ResultArea;

    public GameObject fractionPrefab;
    public GameObject opPrefab;
    public GameObject numPrefab;

    public Master ms;

    private void Awake()
    {
        // 問題マスタの読み込み
        ms = new Master();
    }

    private void Start()
    {
        StartCoroutine(Create(0));
    }

    /// <summary>
    /// 一行分の数式を作成する
    /// </summary>
    private void CreateQuestionArea(GameObject area,int listIndex)
    {
        foreach(Master.Formula ob in ms.questionFomulaList[listIndex])
        {
            // 順番に分数と演算子を生成する
            if (ob is Master.Frac)
            {
                fractionPrefab.transform.Find("denominator").GetComponent<Text>().text = ((Master.Frac)ob).deno;
                fractionPrefab.transform.Find("enumerator").GetComponent<Text>().text = ((Master.Frac)ob).enu;
                GameObject go = Instantiate(fractionPrefab, area.transform);
            }
            else
            {
                opPrefab.transform.Find("op").GetComponent<Text>().text = ((Master.Op)ob).op;
                GameObject  ggo = Instantiate(opPrefab, area.transform);
            }
        }   
    }

    private void CreateOperationArea(int listIndex)
    {
        // 分母
        foreach (Master.Formula ob in ms.bigFractionFomulaList[1])
        {
            // 順番に数と演算子を生成する
            if (ob is Master.Num)
            {
                numPrefab.transform.Find("Toggle/Text").GetComponent<Text>().text = ((Master.Num)ob).num;
                GameObject go1 = Instantiate(numPrefab, OperationArea.transform.Find("denominator"));
                go1.transform.Find("Toggle").GetComponent<Toggle>().group = OperationArea.transform.Find("denominator").GetComponent<ToggleGroup>();
            }
            else
            {
                opPrefab.transform.Find("op").GetComponent<Text>().text = ((Master.Op)ob).op;
                Instantiate(opPrefab, OperationArea.transform.Find("denominator"));
            }
        }

        // 分子
        foreach (Master.Formula ob in ms.bigFractionFomulaList[0])
        {
            // 順番に数と演算子を生成する
            if (ob is Master.Num)
            {
                numPrefab.transform.Find("Toggle/Text").GetComponent<Text>().text = ((Master.Num)ob).num;
                GameObject go2 = Instantiate(numPrefab, OperationArea.transform.Find("enumerator"));
                go2.transform.Find("Toggle").GetComponent<Toggle>().group = OperationArea.transform.Find("enumerator").GetComponent<ToggleGroup>();
            }
            else
            {
                opPrefab.transform.Find("op").GetComponent<Text>().text = ((Master.Op)ob).op;
                Instantiate(opPrefab, OperationArea.transform.Find("enumerator"));
            }
        }
    }



    /// <summary>
    /// 指定した問題を生成する
    /// </summary>
    private IEnumerator Create(int questionNum)
    {
        // 問題エリアの作成
        CreateQuestionArea(QuestionArea1,0);
        yield return new WaitForSeconds(1.0f);
        CreateQuestionArea(QuestionArea2,1);
        yield return new WaitForSeconds(1.0f);
        // 操作エリアの作成
        CreateOperationArea(0);

        // 計算結果表示エリアの作成
    }


    /// <summary>
    /// 次へボタンを押した時
    /// </summary>
    private void OnclickNextButton()
    {
        // ボタンなどの活性非活性をリセット

        // 次に出題する問題番号を取得
        int nextIndex = GetNextQuestionIndex();

        // 問題生成
        Create(nextIndex);
    }

    /// <summary>
    /// 最短ルートロジックにより次の問題番号を算出
    /// </summary>
    private int GetNextQuestionIndex()
    {
        return 1;
    }
}
