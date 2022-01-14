using UnityEngine;

namespace Assets.PixelCrew.Utils {
    public class GizmosUtils {

        public static void DrawBounds(Bounds bounds, Color color) {
            var prevColor = Gizmos.color;
            Gizmos.color = color;
            Vector3 leftTop = new Vector3(bounds.min.x, bounds.max.y);
            Vector3 rightBottom = new Vector3(bounds.max.x, bounds.min.y);
            Gizmos.DrawLine(bounds.min, leftTop);
            Gizmos.DrawLine(leftTop, bounds.max);
            Gizmos.DrawLine(bounds.max, rightBottom);
            Gizmos.DrawLine(rightBottom, bounds.min);
            Gizmos.color = prevColor;
        }
    }
}