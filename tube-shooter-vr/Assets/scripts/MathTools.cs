using UnityEngine;
using System.Collections;

namespace MathTools
{
	public class CoordSystem
	{
		public struct Cylindrical
		{
			public Cylindrical (float rho, float theta, float z)
			{
				this.rho = rho;
				this.theta = theta;
				this.z = z;
			}
			
			public float rho;
			public float theta;
			public float z;
		}
		
		public struct CylindricalB
		{
			public CylindricalB (float rho, Vector2 shift, float theta, float z)
			{
				this.rho = rho;
				this.shift = shift;
				this.theta = theta;
				this.z = z;
			}
			
			public float rho;
			public Vector2 shift;
			public float theta;
			public float z;
		}
		
		public struct Spherical
		{
			public Spherical (float rho, float theta, float phi)
			{
				this.rho = rho;
				this.theta = theta;
				this.phi = phi;
			}
			
			public float rho;
			public float theta;
			public float phi;
		}
		
		//Tools
		public static Vector3 CylindricToCartesian (Cylindrical cylCoordinates)
		{
			return new Vector3 (cylCoordinates.rho * Mathf.Cos (cylCoordinates.theta), cylCoordinates.z, cylCoordinates.rho * Mathf.Sin (cylCoordinates.theta));
		}
		
		public static Vector3 CylindricToCartesianA (Cylindrical cylCoordinates)
		{
			return new Vector3(cylCoordinates.rho * Mathf.Cos(cylCoordinates.theta), cylCoordinates.rho * Mathf.Sin(cylCoordinates.theta), cylCoordinates.z);
		}
		
		public static Vector3 CylindricToCartesianB (CylindricalB cylCoordinates)
		{
			return new Vector3(cylCoordinates.rho * Mathf.Cos(cylCoordinates.theta) + cylCoordinates.shift.x, cylCoordinates.rho * Mathf.Sin(cylCoordinates.theta) + cylCoordinates.shift.y, cylCoordinates.z);
		}
		
		public static Cylindrical CartesianToCylindric (Vector3 position)
		{
			float rho = Mathf.Sqrt (position.x * position.x + position.y * position.y);
			float theta = 0;
			
			if (position.x == 0) {
				if (position.z == 0)
					theta = 0;
				else if (position.z > 0)
					theta = Mathf.PI / 2f;
				else
					theta = 3 * Mathf.PI / 2f;
			} else if (position.x > 0) {
				theta = Mathf.Atan (position.z / position.x);
			} else {
				theta = Mathf.PI + Mathf.Atan (position.z / position.x);
			}
			
			while (theta<0)
				theta += 2 * Mathf.PI;
			
			float z = position.y;
			
			return new Cylindrical (rho, theta, z);
		}
		
		public static Vector3 SphericalToCartesian (Spherical sphCoordinates)
		{
			return sphCoordinates.rho * new Vector3 (Mathf.Sin (sphCoordinates.phi) * Mathf.Cos (sphCoordinates.theta),
			                                         Mathf.Cos (sphCoordinates.phi),
			                                         Mathf.Sin (sphCoordinates.phi) * Mathf.Sin (sphCoordinates.theta));
		}
		
		public static Spherical CartesianToSpherical (Vector3 position)
		{
			float rho = position.magnitude;
			float theta = 0;
			if (position.x == 0) {
				if (position.z == 0)
					theta = 0;
				else if (position.z > 0)
					theta = Mathf.PI / 2f;
				else
					theta = 3 * Mathf.PI / 2f;
			} else if (position.x > 0) {
				theta = Mathf.Atan (position.z / position.x);
			} else {
				theta = Mathf.PI + Mathf.Atan (position.z / position.x);
			}
			
			while (theta<0)
				theta += 2 * Mathf.PI;
			
			float phi = Mathf.PI / 2f;
			if (position.y > 0)
				phi = Mathf.Atan (Mathf.Sqrt (position.x * position.x + position.z * position.z) / position.y);
			else if (position.y < 0)
				phi = Mathf.PI + Mathf.Atan (Mathf.Sqrt (position.x * position.x + position.z * position.z) / position.y);
			
			return new Spherical (rho, theta, phi);
		}
	}
	
	
	public class Funch
	{
		public static float sh (float t)
		{
			return (Mathf.Exp (t) - Mathf.Exp (-t)) * .5f;
		}
		
		public static float ch (float t)
		{
			return (Mathf.Exp (t) + Mathf.Exp (-t)) * .5f;
		}
		
		public static float th (float t)
		{
			return sh (t) / ch (t);
		}
		
		public static float sech (float t)
		{
			return 1f / ch (t);
		}
		
		public static float cosech (float t)
		{
			return 1f / sh (t);
		}
		
		public static float argsh (float t)
		{
			return Mathf.Log (t + Mathf.Sqrt (t * t + 1), 2f);
		}
		
		public static float argch (float t)
		{
			if (t >= 1)
				return Mathf.Log (t + Mathf.Sqrt (t * t - 1), 2f);
			else
				return float.NaN;
		}
		
		public static float argth (float t)
		{
			if (Mathf.Abs (t) < 1)
				return .5f * Mathf.Log ((1 + t) / (1 - t), 2f);
			else
				return float.NaN;
		}
		
		public static float argcoth (float t)
		{
			if (Mathf.Abs (t) > 1)
				return .5f * Mathf.Log ((1 + t) / (t - 1), 2f);
			else
				return float.NaN;
		}
	}
	
}
