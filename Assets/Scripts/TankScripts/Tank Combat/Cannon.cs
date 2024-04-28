using Scriptableobject;
using UnityEngine;

public class Cannon : MonoBehaviour {
    [SerializeField] private Projection _projection;
    [SerializeField] private TankSO _tankSo;

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private ParticleSystem _launchParticles;

    
    private void Update() 
    {
        _projection.SimulateTrajectory(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * _projection._maxPhysicsFrameIterations);
    }
    
    public void HandleControls() 
    {
	    
            var spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
            
            spawned.Init(_ballSpawn.forward * _tankSo.Force, false);
            _launchParticles.Play();
            _source.PlayOneShot(_clip);
	}
}