using System;
using UnityEngine;

///<summary>
///EggObjectにアタッチされるスクリプト
///カメラ追従・時間経過による処理・frypanとの衝突時の処理
///</summary>
public class EggObject : MonoBehaviour
{
    [SerializeField] public GameObject friedEggPrefab;  // 目玉焼き!
    public GameObject _eggCamera;  // MainCameraちゃん
    public Vector3 _cameraPosition;  // カメラのdefolt位置
    public float _count = 2f;  // Eggが出ている時間(秒)
    private ScoreManager _scoreManager;  // スコア管理(参照)
    private Gauge _gauge;  // ゲージUI(参照)    
    private bool isReturningCamera = false;  // カメラが元の位置に戻る
    private Action _onDestroy;
    public TimeManager timeManager;  // Timerを参照
    
    // 初期化処理
    void Start()
    {
        Debug.Log("EggObject Start");
        
        //カメラの初期位置を取得
        _eggCamera = GameObject.Find("Main Camera");
        
        // カメラの初期位置を保持
        _cameraPosition = _eggCamera.transform.position;
        
        //ScoreManagerとGaugeを取得
        Debug.Log("$\"Camera default position set to {_cameraPosition}");
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        _gauge = GameObject.Find("Gauge").GetComponent<Gauge>();
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    // 毎フレーム呼ばれる処理
    void Update()
    {
        //カメラがEggを追従している間
        if (!isReturningCamera)
        {
            //カメラが卵の位置に追従する
            _eggCamera.transform.position = Vector3.Lerp(
                _eggCamera.transform.position, new Vector3(transform.position.x, 0, -10f), 0.1f);
        }
        // カウントダウン
        _count -= Time.deltaTime;
        
        if(_count < 0 )
        {
            Debug.Log("Egg lifetime expired. Returning camera and destroying egg.");
            
            // フォロー中
            // フラグ切り替え
            isReturningCamera = true;
            
            // default位置に戻す
            _eggCamera.transform.position = _cameraPosition;
            if (_gauge != null)
            {
                _gauge._isCharging = true;
                Debug.Log("Gauge charging started.");
            }
            // ゲージチャージ開始
            _onDestroy?.Invoke();
            timeManager._isCountStop = true;
            ScoreManager.instance.isCountUp = false;
            Destroy(this.gameObject);
        }
        else
        {
            // 元の位置に戻す
            _eggCamera.transform.position = Vector3.Lerp(
                _eggCamera.transform.position,
                _cameraPosition,
                0.1f);
        }
    }

    public void RegisterOnDestroyAction(Action action)
    {
        _onDestroy = action;
        Debug.Log("OnDestroy action registered.");
    }
    // Frypanに当たった時
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("$\"Collision detected with: {collision.gameObject.name}");
        
        //frypanに接触したとき
        if (collision.gameObject.CompareTag("frypan"))// タグ
        {
            Debug.Log("Collided with frypan!");
            
            timeManager._isCountStop = true;
            // 卵を目玉焼きに変換
            var friedEgg = Instantiate(friedEggPrefab, transform.position, Quaternion.identity);
            friedEgg.gameObject.transform.SetParent(collision.transform);
            Debug.Log("Fried egg instantiated and parented.");
            
            // スコア加算
            if (ScoreManager.instance.isCountUp == true)
            {
                ScoreManager.instance.AddScore(2);
                Debug.Log("Score +2");
            }
            else
            {
                ScoreManager.instance.AddScore(1);
                Debug.Log("Score +1");
            }
            ScoreManager.instance.isCountUp = true;
            
            // カメラを初期位置に
            _eggCamera.transform.position =  _cameraPosition;
            Debug.Log("Camera returned to default position.");

            if (_gauge != null)
            {
                // ゲージシャージ開始
                _gauge._isCharging = true;
                Debug.Log("Gauge charging started.");
            }
            
            
            // 削除
            // 実行(?.はnullチェック)
            _onDestroy?.Invoke();         
            Destroy(gameObject);
            Debug.Log("EggObject destroyed.");
        }
    }
}
