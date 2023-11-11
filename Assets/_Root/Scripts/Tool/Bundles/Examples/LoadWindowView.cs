using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private AssetReference _loadedSprite;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _spawnAssetButton;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();
        private AsyncOperationHandle<Sprite> _spriteHandle;


        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _spawnAssetButton.onClick.AddListener(SpawnPrefab);
            _addBackgroundButton.onClick.AddListener(AddBackground);
            _removeBackgroundButton.onClick.AddListener(RemoveBackground);
            _removeBackgroundButton.interactable = false;
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _spawnAssetButton.onClick.RemoveAllListeners();
            _addBackgroundButton.onClick.RemoveAllListeners();
            _removeBackgroundButton.onClick.RemoveAllListeners();

            DespawnPrefabs();
            RemoveBackground();
        }

        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetAssetBundles());
        }

        private void SpawnPrefab()
        {
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(_spawningButtonPrefab, _spawnedButtonsContainer);

            _addressablePrefabs.Add(addressablePrefab);
        }

        [ContextMenu(nameof(DespawnPrefabs))]
        private void DespawnPrefabs()
        {
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }

        private async void AddBackground()
        {
            _spriteHandle = Addressables.LoadAssetAsync<Sprite>(_loadedSprite);
            await _spriteHandle.Task;
            if (_spriteHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Sprite sprite = _spriteHandle.Result;
                _backgroundImage.sprite = sprite;
                _backgroundImage.color = Color.white;
            }
            _addBackgroundButton.interactable = false;
            _removeBackgroundButton.interactable = true;
        }

        private void RemoveBackground()
        {
            _backgroundImage.sprite = null;
            _backgroundImage.color = new Color();
            if (_spriteHandle.IsValid())
                Addressables.Release<Sprite>(_spriteHandle);
        }
    }
}
