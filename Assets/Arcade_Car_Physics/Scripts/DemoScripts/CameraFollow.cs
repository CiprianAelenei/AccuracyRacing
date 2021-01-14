using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VehicleBehaviour.Utils {
	public class CameraFollow : MonoBehaviour {
		public WheelVehicle[] opponents;
		[SerializeField] bool follow = false;
		public bool Follow { get { return follow; } set { follow = value; } }
		[SerializeField] Transform target;
		[SerializeField] Transform[] targets;
		[SerializeField] Vector3 offset;
		[Range(0, 10)]
		[SerializeField] float lerpPositionMultiplier = 1f;
		[Range(0, 10)]		
		[SerializeField] float lerpRotationMultiplier = 1f;

		// Speedometer
		[SerializeField] Text speedometer;


		Rigidbody rb;
		Rigidbody target_rb;

		WheelVehicle vehicle;

		void Start () {
			rb = GetComponent<Rigidbody>();
		}

		
		public void SetTargetIndex(int i) {
			WheelVehicle v;

			foreach(Transform t in targets)
			{
				v = t != null ? t.GetComponent<WheelVehicle>() : null;
				if (v != null)
				{
					v.IsPlayer = false;
					v.Handbrake = true;
				}
			}

			target = targets[i % targets.Length];

			vehicle = target != null ? target.GetComponent<WheelVehicle>() : null;
			if (vehicle != null)
			{
				vehicle.IsPlayer = true;
				vehicle.Handbrake = false;
			}
			for (int j = 0; j < opponents.Length; j++)
				opponents[j].Handbrake = false;	
		}

		void FixedUpdate() {
			
			if (!follow || target == null) return;

			
			this.rb.velocity.Normalize();

			
			Quaternion curRot = transform.rotation;
			Vector3 tPos = target.position + target.TransformDirection(offset);

			
			transform.LookAt(target);

		
			if (tPos.y < target.position.y) {
				tPos.y = target.position.y;
			}

			
			transform.position = Vector3.Lerp(transform.position, tPos, Time.fixedDeltaTime * lerpPositionMultiplier);
			transform.rotation = Quaternion.Lerp(curRot, transform.rotation, Time.fixedDeltaTime * lerpRotationMultiplier);

			
			if (transform.position.y < 0.5f) {
				transform.position = new Vector3(transform.position.x , 0.5f, transform.position.z);
			}


            if (speedometer != null && vehicle != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Speed:");
                sb.Append(((int)(vehicle.Speed)).ToString());
                sb.Append(" km/h");

                speedometer.text = sb.ToString();
            }
            else if (speedometer.text != "")
            {
                speedometer.text = "";
            }

        }
    }
}
