using UnityEngine;

public class EggShooter : MonoBehaviour
{
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private Transform Shooter;// Eggを生成する位置
    [SerializeField] private float eggInterval = 3f;// 連続秒間
    [SerializeField] private GameObject eggCamera;
    [SerializeField] private Gauge _gauge;
    private float time = 10; //時間制御
    public float forceMultiplayer = 10f;//飛ばす強さ
    public bool canShot = true;
    private bool isCharging = true;
    public GameObject _eggCamera;
    public TimeManager timeManager;
    
    private void Start()
    {
        Debug.Log("EggShooter Start");
        _gauge._isCharging = true;// ゲージを動かす
    }
    private void Update()
    {
        time += Time.deltaTime;//正確な時間
  
        if (Input.GetMouseButtonUp(0))//マウスを離した瞬間
        {
            Debug.Log("Mouse button released.");
            _gauge._isCharging = false;// ゲージ更新を止める
            timeManager._isCountStop = false;
            EggFire();//したを呼び出す
        }

        if (isCharging)
        {
            time += Time.deltaTime;// 正確な時間
            //UpdateFillAmount();
        }
    }
    
    // 発射する
    private void EggFire()
    {
        _eggCamera = GameObject.Find("Main Camera");
        
        // 発射間隔
        if (time >= eggInterval && canShot)
        {
            Debug.Log("Firing egg...");

            // 投げる角度を線形補間
            float angledig = Mathf.Lerp(90f, 0f, _gauge._fillAmount);
            
            // ラジアンに変換
            float angleRad = angledig * Mathf.Deg2Rad;
            
            // 発射方向のベクトル計算
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            // Shooter位置に生成
            GameObject egg = Instantiate(eggPrefab, Shooter.position, Quaternion.identity);
            
            // 力を加えて飛ばす
            Debug.Log($"Egg instantiated at {Shooter.position} with angle {angledig}° (fillAmount: {_gauge._fillAmount})");
            egg.GetComponent<Rigidbody2D>().AddForce(direction * forceMultiplayer, ForceMode2D.Impulse);
            
            // カメラに卵をセット
            Debug.Log("Force applied to egg: " + (direction * forceMultiplayer));
            
            //Main Camera.GetComponent<CameraFollowEgg>().SetEgg(egg);
            //EggObjectからもってきてる
            var eggObj = egg.GetComponent<EggObject>();
            
            // ActiveCanShot(メソッド)を登録している
            eggObj.RegisterOnDestroyAction(ActiveCanShot);
            Debug.Log("OnDestroy action registered.");
            
            //タイマーリセット
            time = 0f;
            canShot = false;
        }
        else
        {
            Debug.Log("Cannot fire yet. Time: " + time + ", canShot: " + canShot);
        }
    }

    private void ActiveCanShot()
    {
        canShot = true;
        Debug.Log("canShot is now TRUE");

        if (_gauge != null)
        {
            _gauge._isCharging = true;
            Debug.Log("Gauge charging restarted");
        }
        else
        {
            Debug.Log("_gauge is NULL inActiveCanShot!");
        }
    }
}
