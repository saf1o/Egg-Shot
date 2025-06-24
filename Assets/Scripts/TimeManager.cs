using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] public float _startTime = 15;// ゲーム開始時の制御時間(秒)
    [SerializeField] public float _currentTime;// 残り時間(秒)
    public Text timerText;//UIのタイマー表示
    
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _startTime;// 残り時間を初期化
        UpdateTimerDisplay();// UI表示を更新
    }

    // Update is called once per frame
    void Update()
    {
        // 残り時間が０より大きい場合は時間を減らす
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;// フレーム毎に経過時間を減算
            UpdateTimerDisplay();// UI表示を更新
        }
        else
        {
            // 時間切れの処理
            _currentTime = 0;// 0に固定
            UpdateTimerDisplay();
            SceneManager.LoadScene("Result");// リザルト画面に遷移
        }
    }
    
    // タイマー表示を分：秒形式に変換しUIを更新する
    private void UpdateTimerDisplay()
    {
        //時間がマイナスになったときに0以下は切り捨て
        float timeToShow = Mathf.Max(_currentTime, 0f);
        //FloorToIntは、小数点以下を切り捨て
        int minutes = Mathf.FloorToInt(timeToShow / 60);  
        int seconds = Mathf.FloorToInt(timeToShow % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}