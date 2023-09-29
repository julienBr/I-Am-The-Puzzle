using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private AppData _data;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _vitreCuve;

    private void Awake()
    {
        if (_data._lvl_1_succeeded || _data._lvl_2_succeeded || _data._lvl_3succeeded)
        {
            _player.position = _data.posPlayer;
        }
    }
}