using Scriptableobject;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private BulletSO _bulletSo;
    
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private GameObject _poofPrefab;
    
    private bool _isGhost;
    private int hitCount;

    public void Init(Vector3 velocity, bool isGhost) {
        _isGhost = isGhost;
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision col) {
        //if (_isGhost) return;
        
        if (col.gameObject.CompareTag("Enemy")) {
            print("vurduifsiz");
            EnemyTank enemyTank = col.gameObject.GetComponent<EnemyTank>();
            if (enemyTank != null) {
                enemyTank.TakeDamage(100);
                print("vurdu");
            }
        }
        else if (col.gameObject.CompareTag("Player")) {
            PlayerBase player = col.gameObject.GetComponent<PlayerBase>();
            if (player != null) {
                player.TakeDamage(20);
            }
        }

        hitCount++;
        if(hitCount >=  _bulletSo.MaxHitCount)
            Destroy(gameObject);
        
        _source.clip = _clips[Random.Range(0, _clips.Length)];
        _source.Play();
    }
}