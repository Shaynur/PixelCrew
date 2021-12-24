using UnityEngine;

namespace Assets.PixelCrew.Components.Movement
{
    public class ParabolaMovement : MonoBehaviour
    {
        public static Vector2 ParabolaMove(Vector2 start, Vector2 end, float height, float t)
        {
            float f(float x) => -4 * height * x * x + 4 * height * x;
            var mid = Vector2.Lerp(start, end, t);
            return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
        }
    }
}