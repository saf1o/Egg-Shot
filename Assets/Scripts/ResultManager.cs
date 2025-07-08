using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text[] _resultText;// ランキングUI

    // Start is called before the first frame update
    void Start()
    {
        // スコアをManagerから取得
        int _currentScore = ScoreManager.instance.Score;
        Debug.Log($"[ResultManager] 現在のスコア: {_currentScore}");

        // 保存されたスコアを取得
        List<int> scores = LoadScores();
        Debug.Log($"[ResultManager] 保存済みスコア: {string.Join(", ", scores)}");

        // 0点を除外してから追加（＝初期状態でも順位おかしくならない）
        scores = scores.Where(s => s > 0).ToList();

        // 新しいスコアを追加
        scores.Add(_currentScore);
        Debug.Log($"[ResultManager] 0点除外後 + 現在のスコア追加: {string.Join(", ", scores)}");

        // スコアを降順でソート（高い順）
        scores = scores.OrderByDescending(s => s).ToList();
        Debug.Log($"[ResultManager] 降順ソート済み: {string.Join(", ", scores)}");

        // 上位5件だけ保存
        scores = scores.Take(5).ToList();

        // 保存
        SaveScores(scores);
        Debug.Log($"[ResultManager] 降順ソート済み: {string.Join(", ", scores)}");

        // 表示
        for (int i = 0; i < _resultText.Length; i++)
        {
            if (i < scores.Count)
            {
                _resultText[i].text = $"{i + 1} : {scores[i].ToString("D8")}";
                Debug.Log($"[ResultManager] ランキング表示 {i + 1}位: {scores[i]}");
            }
            else
            {
                _resultText[i].text = $"{i + 1} : --------";
                Debug.Log($"[ResultManager] ランキング表示 {i + 1}位: データなし");
            }
        }
    }

    // Update is called once per frame
    
    // 保存されたスコアを取得
    List<int> LoadScores()
    {
        List<int> scores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            scores.Add(PlayerPrefs.GetInt($"HighScore{i}", 0));
        }
        return scores;
    }
    
    // スコアを保存
    void SaveScores(List<int> scores)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt($"HighScore{i}", scores[i]);
        }
        PlayerPrefs.Save();// 保存を確定
        Debug.Log("[ResultManager] PlayerPrefs にスコアを保存しました");
    }
    
    // スタート画面に戻る
    public void OnClickReturnToStart()
    {
        Debug.Log("[ResultManager] タイトルへ戻るボタンが押されました");
        SceneManager.LoadScene("Title");
    }
    // ゲームもう一度実行
    public void OnClickRetryGame()
    {
        Debug.Log("[ResultManager] リトライボタンが押されました");
        SceneManager.LoadScene("InGame");
    }
}
