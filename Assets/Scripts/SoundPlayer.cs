using UnityEngine;
using Paraphernalia.Components;

public class SoundPlayer : MonoBehaviour
{
    public void PlaySound (string sfx)
    {
        AudioManager.PlayVariedEffect(sfx);
    }
}
