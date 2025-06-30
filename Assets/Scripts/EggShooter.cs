using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
        _gauge._isCharging = true;// ゲージを動かす
    }
    private void Update()
    {
        time += Time.deltaTime;//正確な時間
  
        if (Input.GetMouseButtonUp(0))//マウスを離した瞬間
        {
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
        
        if (time >= eggInterval && canShot)// 発射間隔
        {
            // 投げる角度を線形補間
            float angledig = Mathf.Lerp(90f, 0f, _gauge._fillAmount);
            float angleRad = angledig * Mathf.Deg2Rad; // ラジアンに変換
            
            // 発射方向のベクトル計算
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            // Shooter位置に生成
            GameObject egg = Instantiate(eggPrefab, Shooter.position, Quaternion.identity);
            //               生成　　　　何を生成するか、どこに生成するか、角度などの指定
            // 力を加えて飛ばす
            egg.GetComponent<Rigidbody2D>().AddForce(direction * forceMultiplayer, ForceMode2D.Impulse);
            //　　　　　　　　　　　　　　力を加える（方向（1.0)　*　加える力、力を瞬間に加える）
            // カメラに卵をセット
            //Main Camera.GetComponent<CameraFollowEgg>().SetEgg(egg);
            var eggObj = egg.GetComponent<EggObject>();//EggObjectからもってきてる
            eggObj.RegisterOnDestroyAction(ActiveCanShot);// ActiveCanShot(メソッド)を登録している
            time = 0f;//タイマーリセット
            canShot = false;// 
        }
    }
    
    private void ActiveCanShot() => canShot = true;// 
}
