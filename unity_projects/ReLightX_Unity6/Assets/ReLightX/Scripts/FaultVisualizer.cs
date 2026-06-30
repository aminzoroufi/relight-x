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
    public class FaultVisualizer : MonoBehaviour
    {
        public Renderer targetRenderer;
        public Color normalColor = Color.white;
        public Color faultColor = new Color(1f, 0.15f, 0.05f);
        public bool faultActive;

        private void Update()
        {
            if (targetRenderer == null) return;
            Color color = faultActive
                ? Color.Lerp(normalColor, faultColor, Mathf.Abs(Mathf.Sin(Time.time * 4f)))
                : normalColor;
            targetRenderer.material.color = color;
        }

        public void SetFault(bool active)
        {
            faultActive = active;
        }
    }
}
