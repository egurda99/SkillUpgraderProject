// using Audio;
// using StarterAssets;
// using UnityEngine;
//
// namespace Example
// {
//     public sealed class FootstepController : MonoBehaviour
//     {
//         [SerializeField] private AudioClip[] _footstepSounds;
//         [SerializeField] private float _minTimeBetweenFootsteps = 0.3f;
//         [SerializeField] private float _maxTimeBetweenFootsteps = 0.6f;
//         [Range(0.0f, 2.0f)]
//         [SerializeField] private float _minPitch = 1.0f;
//         [Range(0.0f, 2.0f)]
//         [SerializeField] private float _maxPitch = 1.5f;
//
//         private float _timeSinceLastFootstep;
//         private Transform _character;
//
//         private void Start()
//         {
//              _character = FindFirstObjectByType<FirstPersonController>().transform;
//         }
//
//         private void Update()
//         {
//             if (Input.GetKey(KeyCode.W))
//             {
//                 if (Time.time - _timeSinceLastFootstep >= Random.Range(_minTimeBetweenFootsteps, _maxTimeBetweenFootsteps))
//                 {
//                     AudioClip footstepSound = _footstepSounds[Random.Range(0, _footstepSounds.Length)];
//                     AudioManager.Instance.PlaySoundOneShot(footstepSound, _character.position, pitch: Random.Range(_minPitch, _maxPitch));
//
//                     _timeSinceLastFootstep = Time.time;
//                 }
//             }
//         }
//     }
// }
