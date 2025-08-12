using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace UICustomPack
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        // Screens đang hiển thị
        private Dictionary<string, BaseUIScreen> activeScreens = new();
        private Dictionary<string, BaseUIPopup> activePopups = new();

        // Đã load cache prefab
        private Dictionary<string, GameObject> screenCache = new();
        private Dictionary<string, GameObject> popupCache = new();

        [Header("Parent nơi chứa UI")]
        public Transform screenRoot;
        public Transform popupRoot;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        #region SCREEN

        public void OpenScreen(string screenKey)
        {
            CloseAllScreens();

            LoadUI<BaseUIScreen>(screenKey, screenRoot, (screen) =>
            {
                screen.Show();
                activeScreens[screenKey] = screen;
            });
        }

        public void CloseAllScreens()
        {
            foreach (var screen in activeScreens.Values)
                screen.Hide();
            activeScreens.Clear();
        }

        #endregion

        #region POPUP

        public void OpenPopup(string popupKey)
        {
            LoadUI<BaseUIPopup>(popupKey, popupRoot, (popup) =>
            {
                popup.Show();
                activePopups[popupKey] = popup;
            });
        }

        public void ClosePopup(string popupKey)
        {
            if (activePopups.TryGetValue(popupKey, out var popup))
            {
                popup.Hide();
                activePopups.Remove(popupKey);
            }
        }

        #endregion

        #region CORE LOADER

        private void LoadUI<T>(string key, Transform parent, System.Action<T> callback) where T : MonoBehaviour
        {
            // Đã cache
            if (typeof(T) == typeof(BaseUIScreen) && screenCache.TryGetValue(key, out var cachedScreen))
            {
                GameObject go = Instantiate(cachedScreen, parent);
                callback(go.GetComponent<T>());
                return;
            }
            else if (typeof(T) == typeof(BaseUIPopup) && popupCache.TryGetValue(key, out var cachedPopup))
            {
                GameObject go = Instantiate(cachedPopup, parent);
                callback(go.GetComponent<T>());
                return;
            }

            // Chưa load → dùng Addressables
            Addressables.LoadAssetAsync<GameObject>(key).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject prefab = handle.Result;

                    if (typeof(T) == typeof(BaseUIScreen))
                        screenCache[key] = prefab;
                    else
                        popupCache[key] = prefab;

                    GameObject go = Instantiate(prefab, parent);
                    callback(go.GetComponent<T>());
                }
                else
                {
                    Debug.LogError($"Không thể load UI: {key}");
                }
            };
        }

        #endregion
    }
}

