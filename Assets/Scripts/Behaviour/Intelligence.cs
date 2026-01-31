using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemies { Freddy, Bonnie, Chica, Foxy }

public class Intelligence : MonoBehaviour
{
    [SerializeField] private int[] _custom;

    [SerializeField]
    private static Dictionary<CAM, CAM[]> _bonniePathes = new Dictionary<CAM, CAM[]>()
    {
        [CAM._1A] = new CAM[2] { CAM._5, CAM._1B },
        [CAM._5] = new CAM[2] { CAM._1B, CAM._2A },
        [CAM._1B] = new CAM[2] { CAM._5, CAM._2A },
        [CAM._2A] = new CAM[2] { CAM._3, CAM._2B },
        [CAM._3] = new CAM[3] { CAM._2B, CAM._2A, CAM.DOOR },
        [CAM._2B] = new CAM[2] { CAM._3, CAM.DOOR }
    };

    [SerializeField]
    private static Dictionary<CAM, CAM[]> _chicaPathes = new Dictionary<CAM, CAM[]>()
    {
        [CAM._1A] = new CAM[1] { CAM._1B },
        [CAM._1B] = new CAM[2] { CAM._7, CAM._4A },
        [CAM._7] = new CAM[2] { CAM._6, CAM._4A },
        [CAM._4A] = new CAM[2] { CAM._1B, CAM._4B },
        [CAM._6] = new CAM[2] { CAM._7, CAM._4A },
        [CAM._4B] = new CAM[2] { CAM._4A, CAM.DOOR }
    };

    [SerializeField]
    private static Dictionary<CAM, CAM[]> _freddyPathes = new Dictionary<CAM, CAM[]>()
    {
        [CAM._1A] = new CAM[1] { CAM._1B },
        [CAM._1B] = new CAM[1] { CAM._7 },
        [CAM._7] = new CAM[1] { CAM._6 },
        [CAM._6] = new CAM[1] { CAM._4A },
        [CAM._4A] = new CAM[1] { CAM.DOOR }
    };

    public static Dictionary<CAM, CAM[]> GetPath(Enemies enemy)
    {
        switch (enemy)
        {
            case Enemies.Freddy:
                return _freddyPathes;
            case Enemies.Bonnie:
                return _bonniePathes;
            case Enemies.Chica:
                return _chicaPathes;
            default:
                return null;
        }
    }

    private static Dictionary<Enemies, int> _intelligence = new Dictionary<Enemies, int>()
    {
        { Enemies.Freddy, 0 },
        { Enemies.Bonnie, 0 },
        { Enemies.Chica, 0 },
        { Enemies.Foxy, 0 }
    };

    public static int GetIntelligence( Enemies enemies ) => _intelligence[ enemies ];

    public static Intelligence Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        int night = PlayerPrefs.GetInt("Night");

        switch (night)
        {
            case 1:
                _intelligence[Enemies.Bonnie] = 3;
                _intelligence[Enemies.Chica] = 2;
                _intelligence[Enemies.Foxy] = 2;
                break;
            case 2:
                _intelligence[Enemies.Bonnie] = 6;
                _intelligence[Enemies.Chica] = 3;
                _intelligence[Enemies.Foxy] = 3;
                break;
            case 3:
                _intelligence[Enemies.Freddy] = 1;
                _intelligence[Enemies.Bonnie] = 3;
                _intelligence[Enemies.Chica] = 7;
                _intelligence[Enemies.Foxy] = 4;
                break;
            case 4:
                _intelligence[Enemies.Freddy] = Random.Range(1, 3);
                _intelligence[Enemies.Bonnie] = 5;
                _intelligence[Enemies.Chica] = 6;
                _intelligence[Enemies.Foxy] = 8;
                break;
            case 5:
                _intelligence[Enemies.Freddy] = 3;
                _intelligence[Enemies.Bonnie] = 8;
                _intelligence[Enemies.Chica] = 9;
                _intelligence[Enemies.Foxy] = 7;
                break;
            case 6:
                _intelligence[Enemies.Freddy] = 4;
                _intelligence[Enemies.Bonnie] = 13;
                _intelligence[Enemies.Chica] = 14;
                _intelligence[Enemies.Foxy] = 8;
                break;
            default:
                if (_custom is not null)
                {
                    for (int i = 0; i < _custom.Length; ++i)
                    {
                        _intelligence[(Enemies)i] = _custom[i];
                        Debug.Log("Using custom intelligence: ");
                        foreach (int j in _custom) Debug.Log(j + " ");
                    }
                }
                else Debug.LogError("Error during intelligence seting");
                break;
        }
    }
}
