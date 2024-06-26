using UnityEngine;

namespace Marsminerwa
{
    public static class GizmosExtras
    {
        
        public static void Arrow(Vector2 from, Vector2 to, float headOffset = 0f, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Vector2 v = to - from;
            Vector2 direction = v - v.normalized * headOffset; 
            Gizmos.DrawRay(from, direction);
           
            Vector2 right = Quaternion.LookRotation(direction) * Quaternion.Euler(180+arrowHeadAngle, 0, 0) * new Vector3(0,0,1);
            Vector2 left = Quaternion.LookRotation(direction) * Quaternion.Euler(180-arrowHeadAngle, 0, 0) * new Vector3(0,0,1);
            
            Gizmos.DrawRay(from + direction, right * arrowHeadLength);
            Gizmos.DrawRay(from + direction, left * arrowHeadLength);
        }
     
        public static void Arrow(Vector2 from, Vector2 to, Color color, float headOffset = 0f, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = color;
            Arrow(from, to, headOffset, arrowHeadLength, arrowHeadAngle);   
            Gizmos.color = oldColor;
        }
    }
}