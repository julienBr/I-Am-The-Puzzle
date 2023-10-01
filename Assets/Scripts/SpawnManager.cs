using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private AppData _data;
    
    [Header("Player")]
    [SerializeField] private Transform _player;
    
    [Header("Cuve")]
    [SerializeField] private GameObject _vitreCuve;
    [SerializeField] private RuntimeAnimatorController _newAnimator;
    [SerializeField] private GameObject _soundAlarm;

    private void Awake()
    {
        if (_data._lvl_1_succeeded || _data._lvl_2_succeeded || _data._lvl_3succeeded)
        {
            _player.position = _data.posPlayer;
            _soundAlarm.SetActive(false);
            _vitreCuve.GetComponent<Animator>().runtimeAnimatorController = _newAnimator;
        }
    }
}