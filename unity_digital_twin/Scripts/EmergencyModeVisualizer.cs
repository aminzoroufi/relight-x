// ReLight-X source file.
//
// Project: ReLight-X
// Developer: Amin Zoroufi
// Role: AI Researcher / XR Developer
// Location: Dubai, UAE
// Contact: aminn.zoroufi@gmail.com
// Usage: part of the ReLight-X digital twin, adaptive-lighting simulation, board testing, or validation toolchain.

using UnityEngine;

namespace ReLightX
{
    public class EmergencyModeVisualizer : MonoBehaviour
    {
        public Light beaconLight;
        public float pulseSpeed = 5f;
        public bool emergencyActive;

        private void Update()
        {
            if (beaconLight == null) return;
            beaconLight.enabled = emergencyActive;
            if (emergencyActive)
            {
                beaconLight.intensity = 2f + Mathf.Abs(Mathf.Sin(Time.time * pulseSpeed)) * 7f;
            }
        }

        public void SetEmergency(bool active)
        {
            emergencyActive = active;
        }
    }
}
