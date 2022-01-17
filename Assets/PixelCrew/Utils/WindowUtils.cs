using UnityEngine;

namespace Assets.PixelCrew.Utils {
    public static class WindowUtils{

        public static void CreateWindow(string resourcePath) {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvas = GameObject.Find("HudCanvas");
            Object.Instantiate(window, canvas.transform);
        }
    }
}