using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

 

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour {

 

  // タップ時に表示させるオブジェクト
  [SerializeField]

  private GameObject _spawnObject;

 

  private ARRaycastManager _raycastManager;
  private List<ARRaycastHit> _hitResults = new List<ARRaycastHit>();

  private bool _isPlaced = false;

 

  void Awake() {
    _raycastManager = GetComponent<ARRaycastManager>();
  }

 

  void Update() {
    // 一度もオブジェクトを表示してない状態で、画面タップされた場合
    if (!_isPlaced && Input.GetMouseButtonDown(0)) {
      if (_raycastManager.Raycast(Input.GetTouch(0).position, _hitResults)) {
        // タップされた場所にオブジェクトを生成
        Instantiate(_spawnObject, _hitResults[0].pose.position, Quaternion.identity);
        _isPlaced = true;
      }
    }
  }

}