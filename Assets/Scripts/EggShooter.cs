using UnityEngine;


public class EggShooter : MonoBehaviour
{
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private Transform Shooter;
    [SerializeField] private float eggInterval = 0.5f;
    [SerializeField] private GameObject eggCamera;

    private float time = 0; //時間制御
    public float forceMultiplayer = 5f;//加える力

    private void Start()
    {
         
    }

    private void Update()
    {
        time += Time.deltaTime;//正確な時間
  
        if (Input.GetMouseButtonUp(0))//マウスを離した瞬間
        {
            EggFire();//したを呼び出す
        }
    }

    private void EggFire()
    {
        if (time >= eggInterval)
        {
            GameObject egg = Instantiate(eggPrefab, Shooter.position, Quaternion.identity);
            //               生成　　　　何を生成するか、どこに生成するか、角度などの指定
            egg.GetComponent<Rigidbody2D>().AddForce(Vector2.right * forceMultiplayer, ForceMode2D.Impulse);
            //　　　　　　　　　　　　　　力を加える（方向（1.0)　*　加える力、力を瞬間に加える）
            time = 0f;//タイマーリセット

        }
    }
}
