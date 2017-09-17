using UnityEngine;
using System.Collections;

public class scaleButton : MonoBehaviour
{
		bool tang;
		bool giam;
		Vector2 sc;
		// Use this for initialization
		void Start ()
		{
				sc = transform.localScale;
				tang = true;
				giam = false;
                float x=Random.Range (sc.x - 0.05f, sc.x + 0.05f);
				transform.localScale = new Vector3 (x, x, 1);
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (transform.localScale.x < (sc.x - 0.05f)) {
						tang = true;
						giam = false;
				}
				if (transform.localScale.x > (sc.x + 0.05f)) {
						tang = false;
						giam = true;
				}
				if (tang == true) {
						transform.localScale = new Vector3 (transform.localScale.x + 0.001f, transform.localScale.y + 0.001f, 1);
				}
				if (giam == true) {
						transform.localScale = new Vector3 (transform.localScale.x - 0.001f, transform.localScale.y - 0.001f, 1);
				}

		}
}
