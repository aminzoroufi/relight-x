/*
 Project: ReLight-X
 Developer: Amin Zoroufi
 Role: AI Researcher / XR Developer
 Location: Dubai, UAE
 Contact: aminn.zoroufi@gmail.com
 Usage: Unity editor utility for exporting clean README screenshots from the digital twin scene.
*/

using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ReLightX.EditorTools
{
    public static class ReLightXReadmeScreenshotExporter
    {
        private const string ScenePath = "Assets/ReLightX/Scenes/ReLightXHighway.unity";

        [MenuItem("ReLight-X/Export README Screenshots")]
        public static void CaptureReadmeScreenshots()
        {
            if (!File.Exists(ScenePath))
            {
                ReLightXUnity6ProjectBuilder.BuildScene();
            }

            EditorSceneManager.OpenScene(ScenePath);
            string outputFolder = Path.GetFullPath(Path.Combine(Application.dataPath, "../../..", "docs/images"));
            Directory.CreateDirectory(outputFolder);
            PrepareLightingPreview();

            Capture(
                outputFolder,
                "unity-night-highway.png",
                new Vector3(250f, 38f, -62f),
                new Vector3(410f, 0.8f, 1.5f),
                49f
            );
            Capture(
                outputFolder,
                "unity-road-lighting-wave.png",
                new Vector3(130f, 18f, -31f),
                new Vector3(230f, 1.0f, 3f),
                56f
            );

            AssetDatabase.Refresh();
            Debug.Log("ReLight-X README screenshots exported to docs/images.");
        }

        private static void PrepareLightingPreview()
        {
            LuminaireController[] luminaires = Object.FindObjectsOfType<LuminaireController>();
            foreach (LuminaireController luminaire in luminaires)
            {
                luminaire.ForceBrightness(0.30f);
                if (TryPoleNumber(luminaire.poleId, out int poleNumber) && poleNumber >= 7 && poleNumber <= 26)
                {
                    luminaire.ForceBrightness(luminaire.direction == "A" ? 1.0f : 0.72f);
                }
            }
        }

        private static bool TryPoleNumber(string poleId, out int number)
        {
            number = 0;
            if (string.IsNullOrEmpty(poleId) || poleId.Length < 2)
            {
                return false;
            }

            return int.TryParse(poleId.Substring(1), out number);
        }

        private static void Capture(string outputFolder, string fileName, Vector3 cameraPosition, Vector3 lookAt, float fieldOfView)
        {
            GameObject cameraObject = new GameObject($"README Capture Camera {fileName}");
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.002f, 0.004f, 0.007f);
            camera.fieldOfView = fieldOfView;
            camera.nearClipPlane = 0.3f;
            camera.farClipPlane = 2600f;
            camera.transform.position = cameraPosition;
            camera.transform.rotation = Quaternion.LookRotation(lookAt - cameraPosition, Vector3.up);

            RenderTexture renderTexture = new RenderTexture(1600, 900, 24, RenderTextureFormat.ARGB32)
            {
                antiAliasing = 4
            };
            Texture2D image = new Texture2D(1600, 900, TextureFormat.RGB24, false);
            RenderTexture previous = RenderTexture.active;
            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;
            image.ReadPixels(new Rect(0, 0, 1600, 900), 0, 0);
            image.Apply();

            string path = Path.Combine(outputFolder, fileName);
            File.WriteAllBytes(path, image.EncodeToPNG());

            camera.targetTexture = null;
            RenderTexture.active = previous;
            Object.DestroyImmediate(image);
            Object.DestroyImmediate(renderTexture);
            Object.DestroyImmediate(cameraObject);
        }
    }
}
