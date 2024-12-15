using UnityEngine;

public class Camera2D : MonoBehaviour
{
	public Transform targetPlater;
		void Start()
		{
				
		}

		void Update()
		{
				transform.position = new Vector3(targetPlater.position.x + 6f, 0, -10);
		}
}
