using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineSmoother 
{
	public static Vector3[] SmoothLine(Vector3[] inputPointsPosition, float segmentSize)
	{
		AnimationCurve curveX = new AnimationCurve();
		AnimationCurve curveY = new AnimationCurve();
		AnimationCurve curveZ = new AnimationCurve();

		Keyframe[] keysX = new Keyframe[inputPointsPosition.Length];
		Keyframe[] keysY = new Keyframe[inputPointsPosition.Length];
		Keyframe[] keysZ = new Keyframe[inputPointsPosition.Length];

		for( int i = 0; i < inputPointsPosition.Length; i++ )
		{
			keysX[i] = new Keyframe(i, inputPointsPosition[i].x);
			keysY[i] = new Keyframe(i, inputPointsPosition[i].y);
			keysZ[i] = new Keyframe(i, inputPointsPosition[i].z);
		}

		curveX.keys = keysX;
		curveY.keys = keysY;
		curveZ.keys = keysZ;

		for (int i = 0; i < inputPointsPosition.Length; i++)
		{
			curveX.SmoothTangents(i, 0);
			curveY.SmoothTangents(i, 0);
			curveZ.SmoothTangents(i, 0);
		}

		List<Vector3> lineSegments = new List<Vector3>();

		for (int i = 0; i < inputPointsPosition.Length; i++)
		{
			lineSegments.Add(inputPointsPosition[i]);

			if (i+1 < inputPointsPosition.Length)
			{
				float distanceToNext = Vector3.Distance(inputPointsPosition[i], inputPointsPosition[i+1]);
				int segments = (int)(distanceToNext / segmentSize);

				for (int s = 1; s < segments; s++)
				{
					float time = ((float)s/(float)segments) + (float)i;

					Vector3 newSegment = new Vector3( curveX.Evaluate(time), curveY.Evaluate(time), curveZ.Evaluate(time) );

					lineSegments.Add( newSegment );
				}
			}
		}

		return lineSegments.ToArray();
	}

}
