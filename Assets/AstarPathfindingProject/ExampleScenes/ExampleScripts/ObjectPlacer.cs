using UnityEngine;

namespace Pathfinding.Examples {
	/** Small sample script for placing obstacles */
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_object_placer.php")]
	public class ObjectPlacer : MonoBehaviour {
		/** GameObject to place.
		 * When using a Grid Graph you need to make sure the object's layer is included in the collision mask in the GridGraph settings.
		 */
		public GameObject go;
        public GameObject obstacle;
		/** Flush Graph Updates directly after placing. Slower, but updates are applied immidiately */
		public bool direct = false;

		/** Issue a graph update object after placement */
		public bool issueGUOs = true;

		/** Update is called once per frame */
		void Update () {
			if (Input.GetKeyDown("p")) {
				PlaceObject(go);
			}
            if (Input.GetKeyDown(KeyCode.O))
            {
                PlaceObject(obstacle);
            }

            if (Input.GetKeyDown("r")) {
				RemoveObject();
			}
		}

		public void PlaceObject (GameObject go) {
            //���ڵ���ֻ��������һ��
            if(go.tag==Tags.enemy )
            {
                if (GameObject.FindGameObjectWithTag(Tags.enemy) != null || AnimationExcuting.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") == false)
                {
                    return;
                }
            }
            else
            {
                if(GameObject.Find("Main Camera").GetComponent<AstarSmoothFollow2>().enabled==false)
                {
                    return;
                }
            }
            
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Figure out where the ground is
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				Vector3 p = hit.point;
				GameObject obj = GameObject.Instantiate(go, p, Quaternion.identity) as GameObject;
				if (issueGUOs) {
                    //ֻ�����ϰ���Ż����
                    if (obj.tag == Tags.ground)
                    {
                        Bounds b = obj.GetComponent<Collider>().bounds;
                        GraphUpdateObject guo = new GraphUpdateObject(b);
                        AstarPath.active.UpdateGraphs(guo);
                        if (direct)
                        {
                            AstarPath.active.FlushGraphUpdates();
                        }
                    }
				}
			}
		}

		public void RemoveObject () {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check what object is under the mouse cursor
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				// Ignore ground and triggers
				if (hit.collider.isTrigger || hit.transform.gameObject.name == "Room"||hit.collider.tag=="Untagged"||hit.collider.tag==Tags.wall) return;

				Bounds b = hit.collider.bounds;
				Destroy(hit.collider);
				Destroy(hit.collider.gameObject);

				//if (issueGUOs) {
				//	GraphUpdateObject guo = new GraphUpdateObject(b);
				//	AstarPath.active.UpdateGraphs(guo);
				//	if (direct) {
				//		AstarPath.active.FlushGraphUpdates();
				//	}
				//}
			}
		}
	}
}
