using UnityEngine;

namespace Pathfinding.Examples
{
    /** Smooth Camera Following.
	 * \author http://wiki.unity3d.com/index.php/SmoothFollow2
	 */
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_astar_smooth_follow2.php")]
    public class AstarSmoothFollow2 : MonoBehaviour
    {
        public Transform target;
        public float distance = 3.0f;
        public float height = 3.0f;
        public float damping = 5.0f;
        public bool smoothRotation = true;
        public bool followBehind = true;
        public float rotationDamping = 10.0f;
        public bool staticOffset = false;
        public float horzintal = 0;
        public bool isbanLineCast = false;
        public Renderer dogRender;
        private bool isFirstView = false;
        void LateUpdate() 
        {
            Vector3 wantedPosition = Vector3.zero;
            //是否允许射线检测，只有第三人称视角才允许射线检测
            if (!isbanLineCast)
            {
                LineCast();
            }
            if (staticOffset)
            { 
                //利用向量相加，方便在Inspector面板配置参数
                wantedPosition = target.position + new Vector3(horzintal, height, distance);
            }
            else {
                if (followBehind)
                    wantedPosition = target.TransformPoint(0, height, -distance);
                else
                    wantedPosition = target.TransformPoint(0, height, distance);
            }
            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

            if (smoothRotation&&isFirstView==false)
            {
                //望向target
                Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }
     

            if (isFirstView)
            {
                CameraSmoothFollow();
            }
        }
        void LineCast()
        {
            //这里是计算射线的方向，从主角发射方向是射线机方向
            Vector3 aim = target.position;
            //得到方向
            Vector3 ve = (target.position - transform.position).normalized;
            float an = transform.eulerAngles.y;
            aim -= an * ve;
            //在场景视图中可以看到这条射线
            Debug.DrawLine(target.position, aim, Color.red);
            //主角朝着这个方向发射射线
            RaycastHit hit;
            //发射射线
            if (Physics.Linecast(target.position, aim, out hit))
            {
                string name = hit.collider.gameObject.tag;
                if (name == Tags.wall)
                {    //判断目标角色与检测物体角度z轴与X轴的对比
                    if (Vector3.Angle(new Vector3(0, 0, target.position.z), new Vector3(0, 0, hit.transform.position.z)) > Vector3.Angle(new Vector3(0, 0, target.position.x), new Vector3(0, 0, hit.transform.position.x)))
                    {   //如果遮挡物的Z轴小于目标z轴,z轴增加
                        if (target.position.z >= hit.transform.position.z)
                        {

                            distance += Time.deltaTime * 40;
                        }
                        else if (target.position.z < hit.transform.position.z)
                        {

                            distance -= Time.deltaTime * 40;

                        }
                    }
                    else {
                        //同z轴变换原理相似
                        if (target.position.x >= hit.transform.position.x)
                        {

                            horzintal += Time.deltaTime * 40;

                        }
                        else
                        {

                            horzintal -= Time.deltaTime * 40;

                        }
                    }
                }

            }
        }
        public void ThirdView()
        {
            TransformState.instance.PlayButtonClip();
            isbanLineCast = false;
            height = 40.0f;
            dogRender.enabled = true;
            isFirstView = false;
        }
        public void FirstView()
        {
            TransformState.instance.PlayButtonClip();
            isFirstView = true;
            isbanLineCast = true;
            height = 0;
            distance = 0;
            dogRender.enabled = false;
        }
        public void StatelliteView()
        {
          TransformState.instance.PlayButtonClip();
            isbanLineCast = true;
           height = 600.0f;
            dogRender.enabled = true;
            isFirstView = false;
        }
        //平滑跟随目标
        protected void CameraSmoothFollow()
        {   //获得目标y轴旋转度数
            float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.position.y +height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;
            //差值将相机的角度与target一直在y轴
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping* Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, rotationDamping * Time.deltaTime);
            //将角度转转为四元数
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            Vector3 newPos = target.position;

            //将目标位置进行再z轴的旋转
            newPos -= currentRotation * Vector3.forward *5;
            newPos = new Vector3(newPos.x, currentHeight, newPos.z);
          
            //进行位置信息赋值并望向目标
            transform.position = newPos;
            transform.LookAt(target);

        }
        void OnDisable()
        {
            dogRender.enabled = true;
        }
    }
}
