using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float _startTime = 15;// ゲーム開始時の制御時間(秒)
    [SerializeField] public float _currentTime;// 残り時間(秒)
    public Text timerText;//UIのタイマー表示

    public bool _isCountStop;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _startTime;// 残り時間を初期化
        _isCountStop = true;
        Debug.Log($"[TimeManager] ゲーム開始。初期時間：{_startTime} 秒");
        
        UpdateTimerDisplay();// UI表示を更新
    }

    // Update is called once per frame
    void Update()
    {
        // 残り時間が０より大きい場合は時間を減らす
        if (_currentTime > 0)
        {
            if (_isCountStop)
            {
                _currentTime -= Time.deltaTime;// フレーム毎に経過時間を減算
                UpdateTimerDisplay();// UI表示を更新
                Debug.Log($"[TimeManager] 時間更新：残り {_currentTime:F2} 秒");
            }
        }
        else
        {
            // 時間切れの処理
            _currentTime = 0;// 0に固定
            UpdateTimerDisplay();
            Debug.Log("[TimeManager] タイムアップ！リザルトシーンへ移動");
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
        Debug.Log($"[TimeManager] タイマーUI更新：{minutes:00}:{seconds:00}");
    }
}