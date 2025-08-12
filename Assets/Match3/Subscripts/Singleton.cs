using UnityEngine;

namespace Match3.SubScripts
{
    /// <summary>
    /// Class Singleton generic có thể kế thừa cho bất kỳ MonoBehaviour nào
    /// </summary>
    /// <typeparam name="T">Kiểu của class kế thừa từ MonoBehaviour</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        // Instance tĩnh của singleton
        private static T _instance;

        // Khóa đồng bộ hóa để tránh race condition
        private static readonly object _lock = new object();

        // Cờ đánh dấu đã hủy instance
        private static bool _isQuitting = false;

        /// <summary>
        /// Truy cập instance của singleton
        /// </summary>
        public static T Instance
        {
            get
            {
                // Kiểm tra nếu ứng dụng đang thoát
                if (_isQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' đã bị hủy khi ứng dụng thoát.");
                    return null;
                }

                // Khóa để đảm bảo thread-safe
                lock (_lock)
                {
                    // Nếu instance chưa được khởi tạo
                    if (_instance == null)
                    {
                        // Tìm tất cả các instance trong scene
                        T[] instances = FindObjectsByType<T>(FindObjectsSortMode.None);

                        // Nếu có nhiều hơn 1 instance
                        if (instances.Length > 1)
                        {
                            Debug.LogError($"[Singleton] Có nhiều hơn một instance của '{typeof(T)}' trong scene!");
                            return instances[0];
                        }
                        // Nếu đã có một instance
                        else if (instances.Length == 1)
                        {
                            _instance = instances[0];
                            return _instance;
                        }
                        // Nếu chưa có instance nào
                        else
                        {
                            // Tạo GameObject mới và thêm component
                            GameObject singleton = new GameObject($"{typeof(T)} (Singleton)");
                            _instance = singleton.AddComponent<T>();

                            // Đảm bảo singleton không bị hủy khi chuyển scene
                            //DontDestroyOnLoad(singleton);

                            return _instance;
                        }
                    }

                    return _instance;
                }
            }
        }

        /// <summary>
        /// Khi MonoBehaviour bị hủy
        /// </summary>
        protected virtual void OnDestroy()
        {
            _isQuitting = true;
        }

        /// <summary>
        /// Khi ứng dụng thoát
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
        }
    }

    public abstract class SingletonNoLoadDestroy<T> : MonoBehaviour where T : Component
    {
        // Instance tĩnh của singleton
        private static T _instance;

        // Khóa đồng bộ hóa để tránh race condition
        private static readonly object _lock = new object();

        // Cờ đánh dấu đã hủy instance
        private static bool _isQuitting = false;

        /// <summary>
        /// Truy cập instance của singleton
        /// </summary>
        public static T Instance
        {
            get
            {
                // Kiểm tra nếu ứng dụng đang thoát
                if (_isQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' đã bị hủy khi ứng dụng thoát.");
                    return null;
                }

                // Khóa để đảm bảo thread-safe
                lock (_lock)
                {
                    // Nếu instance chưa được khởi tạo
                    if (_instance == null)
                    {
                        // Tìm tất cả các instance trong scene
                        T[] instances = Object.FindObjectsByType<T>(FindObjectsSortMode.None);

                        // Nếu có nhiều hơn 1 instance
                        if (instances.Length > 1)
                        {
                            Debug.LogError($"[Singleton] Có nhiều hơn một instance của '{typeof(T)}' trong scene!");
                            return instances[0];
                        }
                        // Nếu đã có một instance
                        else if (instances.Length == 1)
                        {
                            _instance = instances[0];
                            return _instance;
                        }
                        // Nếu chưa có instance nào
                        else
                        {
                            // Tạo GameObject mới và thêm component
                            GameObject singleton = new GameObject($"{typeof(T)} (Singleton)");
                            _instance = singleton.AddComponent<T>();

                            // Đảm bảo singleton không bị hủy khi chuyển scene
                            DontDestroyOnLoad(singleton);

                            return _instance;
                        }
                    }

                    return _instance;
                }
            }
        }

        /// <summary>
        /// Khi MonoBehaviour bị hủy
        /// </summary>
        protected virtual void OnDestroy()
        {
            _isQuitting = true;
        }

        /// <summary>
        /// Khi ứng dụng thoát
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
        }
    }
}