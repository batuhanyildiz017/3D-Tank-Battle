using UnityEngine;

public class Cannon : MonoBehaviour {
    [SerializeField] private Projection _projection;

    private void Update() {
        HandleControls();
        _projection.SimulateTrajectory(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * _force);
    }

    #region Handle Controls

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private float _force = 20;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private ParticleSystem _launchParticles;


    private void HandleControls() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);

            spawned.Init(_ballSpawn.forward * _force, false);
            _launchParticles.Play();
            _source.PlayOneShot(_clip);
        }
    }

    #endregion
}