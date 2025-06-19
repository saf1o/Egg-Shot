using UnityEngine;


public class EggShooter : MonoBehaviour
{
    [SerializeField] public GameObject eggPrefab;
    [SerializeField] public GameObject friedEggPrefab;
    [SerializeField] private float eggInterval = 0.5f;

    private float time = 0;

    public float forceMultiplier = 5f;
    private Vector2 startPoint;

    private void Start()
    {
        
    }


    private void Update()
    {
        time += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))//マウスを押した瞬間
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))//マウスを離した瞬間
        {
            EggFire();
            time = 0f;
        }


    }

    private void EggFire()
    {
        if (time >= eggInterval)
        {
            Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = startPoint - endPoint;

            Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject egg = Instantiate(eggPrefab, spawnPos, Quaternion.identity);
            egg.GetComponent<Rigidbody2D>().AddForce(direction * forceMultiplier, ForceMode2D.Impulse);
        }
    }


}