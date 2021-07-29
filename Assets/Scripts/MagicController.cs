using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    
    public delegate void OnManaChanged(float mana, float prevMana, float maxMana);
    public event OnManaChanged onManaChanged = delegate {};

    public delegate void OnAnyManaChanged(MagicController magicController, float mana, float prevMana, float maxMana);
    public static event OnAnyManaChanged onAnyManaChanged = delegate {};

    // [Range(0,1)] public float audioSpatialBlend = 0;

    public float restoreTime = 3;
    private bool _isRestoring = false;
    public bool isRestoring {
        get { return _isRestoring; }
    }
    public bool spawnInvincible = false;
    public bool restoreOnEnable = false;
    public float _maxMana = 3;
    public float maxMana {
        get { return _maxMana; }
        set {
            if (value != _maxMana) {
                float prevMax = _maxMana;
                _maxMana = value;
                float diff = _maxMana - prevMax;
                if (mana < _maxMana) mana += diff;
                else if (mana > _maxMana) mana = maxMana;
            }
        }
    }

    public float manaPct {
        get { return Mathf.Clamp01(mana / maxMana); }
    }
    
    private float _mana = -1;
    public float mana {
        get {
            return _mana;
        }
        set {
            float prevMana = _mana;
            _mana = value;
            if (_mana > maxMana) {
                _mana = maxMana;
            }

            // if (_mana <= 0 && prevMana > 0)
            // {
                

                
            // }
            
            
            if (prevMana != _mana) {
                onManaChanged(_mana, prevMana, maxMana);
                onAnyManaChanged(this, _mana, prevMana, maxMana);
            }
        }
    }

    
    public bool isDead {
        get { return mana <= 0; }
    }
    
    void Start() {
        mana = maxMana;
    }

    void OnEnable () {
        if (restoreOnEnable) mana = maxMana;
        if (spawnInvincible) StartCoroutine("Restore");
        else _isRestoring = false;
    }

    IEnumerator Restore () {
        _isRestoring = true;
        yield return new WaitForSeconds(restoreTime);
        _isRestoring = false;
    }
    
}
