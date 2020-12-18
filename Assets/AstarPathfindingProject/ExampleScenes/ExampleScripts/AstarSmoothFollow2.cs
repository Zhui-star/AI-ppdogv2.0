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
            //�Ƿ��������߼�⣬ֻ�е����˳��ӽǲ��������߼��
            if (!isbanLineCast)
            {
                LineCast();
            }
            if (staticOffset)
            { 
                //����������ӣ�������Inspector������ò���
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
                //����target
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
            //�����Ǽ������ߵķ��򣬴����Ƿ��䷽�������߻�����
            Vector3 aim = target.position;
            //�õ�����
            Vector3 ve = (target.position - transform.position).normalized;
            float an = transform.eulerAngles.y;
            aim -= an * ve;
            //�ڳ�����ͼ�п��Կ�����������
            Debug.DrawLine(target.position, aim, Color.red);
            //���ǳ����������������
            RaycastHit hit;
            //��������
            if (Physics.Linecast(target.position, aim, out hit))
            {
                string name = hit.collider.gameObject.tag;
                if (name == Tags.wall)
                {    //�ж�Ŀ���ɫ��������Ƕ�z����X��ĶԱ�
                    if (Vector3.Angle(new Vector3(0, 0, target.position.z), new Vector3(0, 0, hit.transform.position.z)) > Vector3.Angle(new Vector3(0, 0, target.position.x), new Vector3(0, 0, hit.transform.position.x)))
                    {   //����ڵ����Z��С��Ŀ��z��,z������
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
                        //ͬz��任ԭ������
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
        //ƽ������Ŀ��
        protected void CameraSmoothFollow()
        {   //���Ŀ��y����ת����
            float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.position.y +height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;
            //��ֵ������ĽǶ���targetһֱ��y��
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping* Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, rotationDamping * Time.deltaTime);
            //���Ƕ�תתΪ��Ԫ��
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            Vector3 newPos = target.position;

            //��Ŀ��λ�ý�����z�����ת
            newPos -= currentRotation * Vector3.forward *5;
            newPos = new Vector3(newPos.x, currentHeight, newPos.z);
          
            //����λ����Ϣ��ֵ������Ŀ��
            transform.position = newPos;
            transform.LookAt(target);

        }
        void OnDisable()
        {
            dogRender.enabled = true;
        }
    }
}
