using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
   public delegate void OnDurabilityChanged(float durability, float prevDurability, float maxDurability);
    public event OnDurabilityChanged onDurabilityChanged = delegate {};

    public delegate void OnAnyDurabilityChanged(SwordController swordController, float durability, float prevDurability, float maxDurability);
    public static event OnAnyDurabilityChanged onAnyDurabilityChanged = delegate {};

    // [Range(0,1)] public float audioSpatialBlend = 0;

    public float restoreTime = 3;
    private bool _isRestoring = false;
    public bool isRestoring {
        get { return _isRestoring; }
    }
    public bool spawnInvincible = false;
    public bool restoreOnEnable = false;
    public float _maxDurability = 3;
    public float maxDurability {
        get { return _maxDurability; }
        set {
            if (value != _maxDurability) {
                float prevMax = _maxDurability;
                _maxDurability = value;
                float diff = _maxDurability - prevMax;
                if (durability < _maxDurability) durability += diff;
                else if (durability > _maxDurability) durability = maxDurability;
            }
        }
    }

    public float durabilityPct {
        get { return Mathf.Clamp01(durability / maxDurability); }
    }
    
    private float _durability = -1;
    public float durability {
        get {
            return _durability;
        }
        set {
            float prevDurability = _durability;
            _durability = value;
            if (_durability > maxDurability) {
                _durability = maxDurability;
            }
            
            
            if (prevDurability != _durability) {
                onDurabilityChanged(_durability, prevDurability, maxDurability);
                onAnyDurabilityChanged(this, _durability, prevDurability, maxDurability);
            }
        }
    }
    
    void Start() {
        durability = maxDurability;
    }

    void OnEnable () {
        if (restoreOnEnable) durability = maxDurability;
        if (spawnInvincible) StartCoroutine("Restore");
        else _isRestoring = false;
    }

    public void UsedSword() {
        if (!enabled) return;
        // if (allowRestore && restoreTime > 0.001f && gameObject.activeInHierarchy) StartCoroutine("Restore");
        durability -= 1.0f;

    }

    IEnumerator Restore () {
        _isRestoring = true;
        yield return new WaitForSeconds(restoreTime);
        _isRestoring = false;
    }
    
}
