using UnityEngine;

namespace Code.Game.Card
{
    public static class CardInHandPositionHandler
    {
        public static Vector3[] GetCardInHandPositions(Transform pivot, int count, float radius)
        {
            var result = new Vector3[count];
            count++;
            var currentAngle = 180f;
            var step = currentAngle / count;
            var posX = float.PositiveInfinity;
            var posY = float.PositiveInfinity;

            for (var i = 0; i < count; ++i)
            {
                if (i > 0)
                {
                    posX = pivot.position.x + Mathf.Cos(currentAngle * Mathf.Deg2Rad) * radius;
                    posY = pivot.position.y + Mathf.Sin(currentAngle * Mathf.Deg2Rad) * radius;

                    result[i - 1] = new Vector3(posX, posY, default);
                }

                currentAngle -= step;
            }

            return result;
        }
    }
}
