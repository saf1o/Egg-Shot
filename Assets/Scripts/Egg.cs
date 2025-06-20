using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] public GameObject friedEggPrefab;
    public GameObject _eggCamera;
    public Vector3 _cameraPosition;
    public float _count = 3;

    // Start is called before the first frame update
    void Start()
    {
        _eggCamera = GameObject.Find("Main Camera");
        _cameraPosition = _eggCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _eggCamera.transform.position = new Vector3(transform.position.x, transform.position.y,-10f);
        _count -= Time.deltaTime;

        Debug.Log(_count);
        if(_count < 0 )
        {
            _eggCamera.transform.position = _cameraPosition;
            Destroy(this.gameObject);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("frypan"))
        {
            Instantiate(friedEggPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
