using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private const string SOUNDMANAGER_VOLUME = "SoundManagerVolume";

    private int volume = 5;
    private void Awake()
    {
        Instance = this;
        LoadVolume();
    }
    private void Start()
    {
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
        CuttingCounter.Oncut += CuttingCounter_Oncut;
        KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
        KitchenObjectHolder.OnPickup += KitchenObjectHolder_OnPickup;
        TrashCounter.OnObjectTrash += TrashCounter_OnObjectTrash;
    }
    private void TrashCounter_OnObjectTrash(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.trash);
    }
    private void KitchenObjectHolder_OnPickup(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup);
    }
    private void KitchenObjectHolder_OnDrop(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectDrop);
    }
    private void CuttingCounter_Oncut(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.chop);
    }
    private void OrderManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail);
    }
    private void OrderManager_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess);
    }
    public void PlayStepSound(float volumeMutipler = 1f)
    {
        PlaySound(audioClipRefsSO.footstep, volumeMutipler);
    }
    private void PlaySound(AudioClip[] clips, float volumeMutipler = 1f)
    {
        PlaySound(clips, Camera.main.transform.position, volumeMutipler);
    }
    private void PlaySound(AudioClip[] clips, Vector3 position, float volumeMutipler = 1f)
    {
        if(volume==0)
        {
            return;
        }
        int index = Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[index], position, volumeMutipler*(volume/10.0f));
    }
    public void ChangeVolume()
    {
        volume++;
        if (volume > 10)
        {
            volume = 0;
        }
        SaveVolume();
    }
    public int GetVolume()
    {
        return volume;
    }
    private void SaveVolume()
    {
        PlayerPrefs.SetInt("SOUNDMANAGER_VOLUME", volume);
    }
    private void LoadVolume()
    {
        volume = PlayerPrefs.GetInt("SOUNDMANAGER_VOLUME",volume );
    }
}
