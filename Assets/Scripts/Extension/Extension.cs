using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Extension
{
    public static class Extension
    {
        /// <summary>
        /// 根据名字查找所有层级的子物体
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform FindAllChildren(this Transform trans, string name)
        {
            Transform[] transformArry = trans.GetComponentsInChildren<Transform>();//包含trans
            foreach (var item in transformArry)
            {
                if (item.gameObject.name == name)
                    return item;
            }
            
            Debug.LogError("Can't find " + name + " in " + trans.name);
            
            return null;
        }
        
        /// <summary>
        /// 传入两个 Transform，输出 targetTransform 与 currentTransform.forward 的逆时针夹角（360 度），忽略高度差
        /// </summary>
        /// <param name="currentTransform"></param>
        /// <param name="targetTransform"></param>
        /// <returns></returns>
        public static float GetAngle(this Transform currentTransform, Transform targetTransform)
        {
            var a = (targetTransform.position - currentTransform.position).normalized;
            var b = currentTransform.forward;

            a.y = 0;
            a.Normalize();

            b.y = 0;
            b.Normalize();
            
            var angle = Vector3.Angle(a,b);
            var sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(a,b)));
            var signedAngle = angle * sign;
            return (signedAngle <= 0) ? 360 + signedAngle : signedAngle;
        }
        
        /// <summary>
        /// 查找字典里的值，不存在则报错
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <typeparam name="Tkey"></typeparam>
        /// <typeparam name="Tvalue"></typeparam>
        /// <returns></returns>
        public static Tvalue GetValue<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dic, Tkey key)
        {
            if (!dic.ContainsKey(key))
            {
                Debug.LogError("Can't find " + key.ToString() + " in " + dic.ToString());
            }
            
            return dic[key];
        }

        /// <summary>
        /// 修改动画速度
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Animator SetSpeed(this Animator animator, float num)
        {
            animator.speed = num;
            return animator;
        }
        
        /// <summary>
        /// 重置动画速度
        /// </summary>
        /// <param name="animator"></param>
        /// <returns></returns>
        public static Animator ResetSpeed(this Animator animator)
        {
            animator.speed = 1f;
            return animator;
        }
        
        /// <summary>
        /// 开始旋转
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public static NavMeshAgent StartRotation(this NavMeshAgent agent)
        {
            agent.updateRotation = true;
            return agent;
        }
        
        /// <summary>
        /// 停止旋转
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public static NavMeshAgent StopRotation(this NavMeshAgent agent)
        {
            agent.updateRotation = false;
            return agent;
        }
        
        /// <summary>
        /// 切换摄像机至此摄像机
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static CinemachineVirtualCameraBase IncreasePriority(this CinemachineVirtualCameraBase camera)
        {
            camera.Priority = 100;
            return camera;
        }
        
        /// <summary>
        /// 重置摄像机优先级
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static CinemachineVirtualCameraBase ResetPriority(this CinemachineVirtualCameraBase camera)
        {
            camera.Priority = -1;
            return camera;
        }
        
        /// <summary>
        /// 检查碰撞体层级
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static bool CompareLayer(this GameObject gameObject, int layer)
        {
            return gameObject.layer == layer;
        }

        /// <summary>
        /// 按照大写字母与数字插入空格
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string InsertSpace(this string s)
        {
            var newString = s.Replace(" ", "").Replace("\t", "");
            newString = Regex.Replace(newString, @"(?<!^)(?=([A-Z]|\d))", " $0");
            return newString;
        }
    }
}
