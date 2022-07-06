using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 最短ルートロジックを管理する
/// </summary>
public class RootLogicManager : MonoBehaviour
{
    /// <summary>
    /// 連続正解をカウントする
    /// </summary>
    public int bonus = 0;

    /// <summary>
    /// 問題の最大インデックス
    /// </summary>
    public readonly int MAX_QUESTION_INDEX = 29;

    /// <summary>
    /// 問題の最大取り組み数
    /// </summary>
    public readonly int MAX_QUESTION_COUNT = 5;

    /// <summary>
    /// 正解した時に進む固定値
    /// </summary>
    public readonly int CORRECT_STEP = 5;

    /// <summary>
    /// 基本の進む固定値
    /// </summary>
    public readonly int BASIC_STEP = 2;

    /// <summary>
    /// 現在の問題のインデックス
    /// </summary>
    public int nowIndex = 0;

    /// <summary>
    /// 現在の取り組み数
    /// </summary>
    public int questionTotalCount = 1;

    public GameObject list;
    public Toggle correctOrWrong;
    public Button answerButton;
    public Button nextButton;

    private void Start()
    {
        // 動作確認
        //for(int i = 0;i<=29;i++)
        //{
        //    Debug.Log($"{i+1}問目なら達成率は{GetCorrectRate(i)}");
        //}

        // 1問目を赤に
        list.transform.GetChild(0).GetComponent<Image>().color = Color.red;

        answerButton.onClick.AddListener(OnclickAnswerButton);
        nextButton.onClick.AddListener(OnclickNextButton);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nowIndex">今取り組んでいた問題のIndex</param>
    /// <param name="bonus">連続正解係数</param>
    /// <param name="isCorrectNow">今取り組んでいた問題が正解かどうか</param>
    /// <returns>次の問題のIndex</returns>
    public int SearchNextQuestion(int nowIndex,int bonus,bool isCorrectNow)
    {
        // 正解ならボーナスをつけて次のIndexを算出
        // 不正解なら単純に+2する
        int nextIndex = isCorrectNow ? nowIndex + CORRECT_STEP + (bonus - 1) * BASIC_STEP : nowIndex + BASIC_STEP;

        // L3問題で正解した場合はIndexをはみ出す場合があるので最終調整
        nextIndex = Mathf.Min(nextIndex, MAX_QUESTION_INDEX);
        Debug.Log($"次は{nextIndex+1}問目です");
        return nextIndex;
    }

    /// <summary>
    /// 正誤判定を行う
    /// </summary>
    /// <param name="choicedIndex">選択した選択肢のID</param>
    /// <param name="nowQuestion">正解のID</param>
    /// <returns>正誤結果</returns>
    public bool IsCorrect(int choicedIndex,Question nowQuestion)
    {
        return choicedIndex == nowQuestion.correctChoiceIndex;
    }

    /// <summary>
    /// 答え合わせをタップした時の一連の流れ
    /// </summary>
    public void OnclickAnswerButton()
    {
        //TODO
        // 選択されているトグルを取得する

        //FIXME デバッグ用
        bool isCorrect = correctOrWrong.isOn;

        // 正誤を判定
        //bool isCorrect = IsCorrect(1, questionList[0]);

        // 正解の場合、bonusをインクリメントし、不正解の場合、ボーナスをリセットする
        bonus = isCorrect ? bonus + 1 : 0;

        // 取り組み数をインクリメント
        questionTotalCount++;

        // 最終問題の場合は現在のインデックス位置から達成率を算出
        if (questionTotalCount == MAX_QUESTION_COUNT)
        {
            // 達成率を計算
            Debug.Log($"達成率は{GetCorrectRate(nowIndex)}");
        }

        // 次の問題のインデックスを指定
        nowIndex = SearchNextQuestion(nowIndex, bonus, isCorrect);

        answerButton.interactable = false;
        nextButton.interactable = true;
    }

    /// <summary>
    /// 最終問題のインデックスから達成率を算出する
    /// </summary>
    /// <param name="lastQuestionIndex">最終問題のインデックス</param>
    /// <returns>達成率</returns>
    public int GetCorrectRate(int lastQuestionIndex)
    {
        // インデックスと問題数の調整
        lastQuestionIndex += 1;
        int correctRate = 30;
        if(1 <= lastQuestionIndex && lastQuestionIndex <= 5)
        {
            correctRate = 30;
        }
        else if(6 <= lastQuestionIndex && lastQuestionIndex <= 10)
        {
            correctRate = 40;
        }
        else if (11 <= lastQuestionIndex && lastQuestionIndex <= 13)
        {
            correctRate = 50;
        }
        else if (14 <= lastQuestionIndex && lastQuestionIndex <= 17)
        {
            correctRate = 60;
        }
        else if (18 <= lastQuestionIndex && lastQuestionIndex <= 20)
        {
            correctRate = 70;
        }
        else if (21 <= lastQuestionIndex && lastQuestionIndex <= 25)
        {
            correctRate = 80;
        }
        else if (26 <= lastQuestionIndex && lastQuestionIndex <= 29)
        {
            correctRate = 90;
        }
        else if (lastQuestionIndex == 30)
        {
            correctRate = 100;
        }

        return correctRate;
    }

    /// <summary>
    /// 次へボタンをタップした時の一連の流れ
    /// </summary>
    public void OnclickNextButton()
    {
        // 次に取り組む問題を赤に
        list.transform.GetChild(nowIndex).GetComponent<Image>().color = Color.red;

        answerButton.interactable = true;
        nextButton.interactable = false;
    }
}

public class Question
{
    public int questionId;
    public List<string> choiceList;
    public int correctChoiceIndex;

    public Question(int questionId,List<string>choiceList,int correctChoiceIndex)
    {
        this.questionId = questionId;
        this.choiceList = choiceList;
        this.correctChoiceIndex = correctChoiceIndex;
    }
}
