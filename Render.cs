using System;
using UnityEngine;

namespace TestUnityInternal
{
	public class Render : MonoBehaviour
	{
		public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

		public static Color Color
		{
			get { return GUI.color; }
			set { GUI.color = value; }
		}

		public static void DrawBox(Vector2 position, Vector2 size, Color color, bool centered = true)
		{
			Color = color;
			DrawBox(position, size, centered);
		}
		public static void DrawBox(Vector2 position, Vector2 size, bool centered = true)
		{
			var upperLeft = centered ? position - size / 2f : position;
			GUI.DrawTexture(new Rect(position, size), Texture2D.whiteTexture, ScaleMode.StretchToFill);
		}

		public static void DrawString(Vector2 position, string label, Color color, bool centered = true)
		{
			Color = color;
			DrawString(position, label, centered);
		}
		public static void DrawString(Vector2 position, string label, bool centered = true)
		{
			var content = new GUIContent(label);
			var size = StringStyle.CalcSize(content);
			var upperLeft = centered ? position - size / 2f : position;
			GUI.Label(new Rect(upperLeft, size), content);
		}

		public static Texture2D lineTex;
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
		{
			Matrix4x4 matrix = GUI.matrix;
			if (!lineTex)
				lineTex = new Texture2D(1, 1);

			Color color2 = GUI.color;
			GUI.color = color;
			float num = Vector3.Angle(pointB - pointA, Vector2.right);

			if (pointA.y > pointB.y)
				num = -num;

			GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
			GUIUtility.RotateAroundPivot(num, pointA);
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), lineTex);
			GUI.matrix = matrix;
			GUI.color = color2;
		}

		public static void DrawCircle(Vector2 center, float radius, Color color, int segments = 360, float thickness = 1f)
		{
			float angleStep = 360f / segments;
			Vector2 prevPoint = center + new Vector2(radius, 0);

			for (int i = 1; i <= segments; i++)
			{
				float angle = angleStep * i * Mathf.Deg2Rad;
				Vector2 nextPoint = center + new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
				DrawLine(prevPoint, nextPoint, color, thickness);
				prevPoint = nextPoint;
			}
		}
		public static void DrawCenteredLines(float lineLength, Color lineColor, float lineWidth)
		{
			// Calculate the center of the screen
			Vector2 centerOfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);

			// Define the length of the lines
			float halfLineLength = lineLength / 2f;

			// Draw the first line horizontally
			Vector2 startPoint1 = centerOfScreen + new Vector2(-halfLineLength, 0);
			Vector2 endPoint1 = centerOfScreen + new Vector2(halfLineLength, 0);
			DrawLine(startPoint1, endPoint1, lineColor, lineWidth);

			// Draw the second line vertically
			Vector2 startPoint2 = centerOfScreen + new Vector2(0, -halfLineLength);
			Vector2 endPoint2 = centerOfScreen + new Vector2(0, halfLineLength);
			DrawLine(startPoint2, endPoint2, lineColor, lineWidth);
		}
		public static void DrawBox(float x, float y, float w, float h, Color color, float thickness)
		{
			DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
			DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
			DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
			DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
		}

		public static void DrawBoxOutline(Vector2 Point, float width, float height, Color color, float thickness)
		{
			DrawLine(Point, new Vector2(Point.x + width, Point.y), color, thickness);
			DrawLine(Point, new Vector2(Point.x, Point.y + height), color, thickness);
			DrawLine(new Vector2(Point.x + width, Point.y + height), new Vector2(Point.x + width, Point.y), color, thickness);
			DrawLine(new Vector2(Point.x + width, Point.y + height), new Vector2(Point.x, Point.y + height), color, thickness);
		}
	}
}
