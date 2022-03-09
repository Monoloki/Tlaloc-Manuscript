using System;
using Cinemachine;
using UnityEngine;

namespace Camera {
    public class CameraZoom : MonoBehaviour {
        [SerializeField] [Range(0f, 10f)] private float defaultDistance = 6f;
        [SerializeField] [Range(0f, 10f)] private float maximumDistance = 6f;
        [SerializeField] [Range(0f, 10f)] private float minimumDistance = 1f;
        [SerializeField] [Range(0f, 10f)] private float smoothing       = 4f;
        [SerializeField] [Range(0f, 10f)] private float zoomSensitivity = 1f;

        private float _currentTargetDistance;

        private CinemachineFramingTransposer _framingTransposer;
        private CinemachineInputProvider     _inputProvider;

        private void Awake(){
            _framingTransposer = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineFramingTransposer>();
            _inputProvider = GetComponent<CinemachineInputProvider>();

            _currentTargetDistance = defaultDistance;
        }

        private void Update(){
            Zoom();
        }

        private void Zoom(){
            var zoomValue = _inputProvider.GetAxisValue(2) * zoomSensitivity;
            _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomValue, minimumDistance, maximumDistance);

            var currentDistance = _framingTransposer.m_CameraDistance;

            //Fixing float compare
            if (Math.Abs(currentDistance - _currentTargetDistance) == 0) return;

            var leapedZoomValue = Mathf.Lerp(currentDistance, _currentTargetDistance, smoothing * Time.deltaTime);

            _framingTransposer.m_CameraDistance = leapedZoomValue;
        }
    }
}
