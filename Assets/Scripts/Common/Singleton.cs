using UnityEngine;

namespace KG.Common.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _one;
        public static T One
        {
            get
            {
                if (_one == null)
                {
                    _one = FindObjectOfType<T>();
                }

                if (_one == null)
                {
                    var instanceObj = new GameObject();
                    var component = instanceObj.AddComponent<T>();
                    _one = component;
                }

                return _one;
            }
        }

        protected virtual void Awake()
        {
            if (_one == null)
            {
                _one = this as T;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}