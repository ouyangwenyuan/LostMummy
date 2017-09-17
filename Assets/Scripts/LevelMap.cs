using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LevelMap
{
    public static int currentLevel;
    public static bool flagWin = false;
    public static bool flagPassedLevel = false;

    //state =0 : Nothing
    public const int xxxx = -1;//state = -1 : border xung quanh map
    public const int S_PLAYER = 1;//state =1 : player
    public const int S_MUMMY1 = 2;//state =2 : quai vat trang
    public const int S_MUMMY2 = 3;//state =3 : quai vat do

    public const int S_KEY = 4;//state =4 : chia khoa
    public const int S_GATE = 5;//state =5 : gate

    public const int S_DICH1 = 6;//state =6 : đích bên dưới
    public const int S_DICH2 = 7;//state =7 : đích bên trên cùng
    public const int S_DICH3 = 8;//state =8 : đích bên trái
    public const int S_DICH4 = 9;//state =9 : đích bên phải

    public const int W_NGANG = 10;//state =10 : tường ngang ben duoi
    public const int W_NGANG_T = 21;//state =21 : tường ngang ben tren

    public const int W_WALL2 = 11;//state =11 : tường dọc có góc
    public const int W_DOCT = 12;//state =12 : tường dọc ben trai
    public const int W_DOCP = 18;//state =18 : tường dọc ben phai

    public const int W_TRAI_D = 13;//state =13 : tưòng góc trái dưới
    public const int W_PHAI_D = 14;//state =14 : tưòng góc phải dưới
    public const int W_TRAI_U = 15;//state =15 : tưòng góc trái trên
    public const int W_PHAI_U = 16;//state =16 : tưòng góc phải trên
    public const int W_ALL_D = 17;//state =17 : tưòng tường 3 góc

    public const int S_TRAP = 20;//state =20 : trap in map
    public static List<int[,]> allMAP = new List<int[,]>();

    public static int[,] map0 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,  xxxx,},
        {   xxxx,         0,        0,        0,     W_DOCT,            0,    W_NGANG,  xxxx,},
        {   xxxx,         0,        0,    W_DOCT,   W_NGANG,     W_TRAI_D,        0,  xxxx,},
        {   xxxx,         0,        0,  W_NGANG,         0,            0,        0,  xxxx,},
        { S_DICH3,        0, W_TRAI_D,        0,         0,            0,    W_NGANG,  xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,  xxxx,},
        {   xxxx,         0,        0,        0,     W_DOCT,            0,        0,  xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,  xxxx,},
        {      3,         4,        1,        5,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map1 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,  xxxx,},
        {   xxxx,   W_NGANG, W_NGANG,         0,         0,            0,        0,  xxxx,},
        {   xxxx,         0,       0,         0,         0,     W_TRAI_U,        0,  xxxx,},
        {   xxxx,         0,    W_DOCT,    W_DOCT,  W_PHAI_U,            0,        0,  xxxx,},
        {   xxxx,         0,        0,        0,   W_ALL_D,            0,        0,  xxxx,},
        {   xxxx,         0,    W_DOCT,        0,     W_DOCT,      W_NGANG,  W_NGANG,  xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,  xxxx,},
        {   xxxx,   S_DICH1,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,  xxxx,},
        {      2,         2,        5,        5,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map2 = new int[,]
        {
        {   xxxx,      xxxx,  S_DICH2,     xxxx,      xxxx,         xxxx,     xxxx,  xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,  xxxx,},
        {   xxxx,         0,        0,        0,         0,      W_NGANG,        0,  xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,  xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,  xxxx,},
        {   xxxx,    W_NGANG,       0,        0,         0,            0,        0,  xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,  xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,  xxxx,},
        {      4,         3,        6,        6,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map3 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,  xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,  xxxx,},
        {   xxxx,  W_PHAI_U,        0,        0,         0,            0,        0,  xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,  xxxx,},
        {S_DICH3,         0,        0,        0,     W_DOCT,            0,    W_DOCT,  xxxx,},
        {   xxxx,         0,    W_DOCT,        0,  W_TRAI_D,      W_NGANG,        0,  xxxx,},
        {   xxxx,         0,        0,    W_DOCT,         0,            0,        0,  xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,  xxxx,},
        {      3,         5,        6,        2,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map4 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,      S_DICH2,     xxxx,   xxxx,},
        {   xxxx,         0,  W_NGANG,        0,         0,      W_NGANG,        0,   xxxx,},
        {   xxxx,         0,        0,        0,     W_DOCT,            0,  W_NGANG,   xxxx,},
        {   xxxx,   W_NGANG, W_PHAI_U,        0,     W_DOCT,            0,        0,   xxxx,},
        {   xxxx,         0,        0, W_PHAI_D,         0,     W_PHAI_U,        0,   xxxx,},
        {   xxxx,         0,        0,        0,   W_NGANG,      W_NGANG,    W_DOCT,   xxxx,},
        {   xxxx,         0,    W_DOCT,    W_DOCT,     W_DOCT,            0,        0,   xxxx,},
        {   xxxx,     xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      2,         1,        1,        3,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map5 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,  W_NGANG,   W_NGANG,            0,        0,   xxxx,},
        {   xxxx,         0,  W_NGANG, W_TRAI_D,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,    S_KEY,  W_PHAI_D,            0,        0,   S_DICH4,},
        {   xxxx,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0, W_PHAI_D,         0,       W_DOCP,   S_GATE,   xxxx,},
        {   xxxx,         0,        0,        0,         0,       W_DOCT,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      6,         1,        1,        2,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map6 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,   S_DICH2,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,        0,   W_TRAI_D,           0,        0,   xxxx,},
        {   xxxx,   W_NGANG,        0,        0,         0,            0, W_TRAI_D,   xxxx,},
        {   xxxx,         0, W_PHAI_U,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,         0,       S_GATE,        0,   xxxx,},
        {   xxxx,    W_DOCP,        0, W_TRAI_D,         0,        S_KEY,        0,   xxxx,},
        {   xxxx,         0,        0,   W_DOCT,         0,            0,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      5,         2,        6,        6,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map7 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,        0,  W_TRAI_D,            0,        0,   xxxx,},
        {S_DICH3,    W_DOCP,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,  W_ALL_D,         0,      W_NGANG,        0,   xxxx,},
        {   xxxx,         0,        0,        0,  W_PHAI_D,      W_NGANG,        0,   xxxx,},
        {   xxxx,    W_DOCP,        0,        0,  W_PHAI_D,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,         0,       W_DOCP,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      4,         6,        0,        0,         5,            4,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map8 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,    W_DOCP,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,    W_DOCP,            0,        0,   xxxx,},
        {   xxxx,         0,        0,  W_NGANG,   W_NGANG,            0,        0,   xxxx,},
        {S_DICH3,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      2,         5,        0,        0,         6,            6,        0,     0,}, //Toa do mummy va player
        };




    public static int[,] map12 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,    W_DOCP,            0, W_TRAI_D,   xxxx,},
        {   xxxx,        0,        0,         0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,         0,       W_DOCP,        0,   xxxx,},
        {S_DICH3,         0,        0,    W_NGANG,       0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      4,         6,        1,        5,         0,            0,        0,     0,}, //Toa do mummy va player
        };


    public static int[,] map13 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,  S_DICH2,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,  W_NGANG,        0,         0,       W_DOCP,        0,   xxxx,},
        {   xxxx,         0,  W_NGANG,  W_NGANG,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,  W_NGANG,  W_PHAI_D,     W_TRAI_U,        0,   xxxx,},
        {   xxxx,   W_NGANG,        0,        0,    W_DOCP,     W_PHAI_U,        0,   xxxx,},
        {   xxxx,   W_NGANG,   W_DOCP,        0,  W_PHAI_D,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      4,         6,        1,        5,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map9 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,        0,   W_NGANG,       W_DOCP,        0,   S_DICH4,},
        {   xxxx,    W_DOCP,        0,        0,   W_NGANG,            0,        0,   xxxx,},
        {   xxxx,         0,        0, W_TRAI_D,         0,            0,  W_NGANG,   xxxx,},
        {   xxxx,   W_NGANG,        0,  W_NGANG,         0,            0,        0,   xxxx,},
        {   xxxx,         0, W_PHAI_D,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      5,         1,        2,        6,         0,            0,        0,     0,}, //Toa do mummy va player
        };


    public static int[,] map10 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,       xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,  W_NGANG,        0,    W_NGANG,            0,  W_NGANG,   xxxx,},
        {   xxxx,         0,  W_NGANG,        0,          0,            0,        0,   xxxx,},
        {   xxxx,         0,        0, W_TRAI_D,          0,       W_DOCT,   W_DOCT,   xxxx,},
        {   xxxx,         0,   W_NGANG, W_NGANG,   W_TRAI_D,            0,   W_DOCT,   xxxx,},
        {   xxxx,         0,        0,        0,          0,            0,   W_DOCT,   xxxx,},
        {   xxxx,    W_DOCP,        0, W_PHAI_U,          0,     W_TRAI_U,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,       xxxx,         xxxx,  S_DICH1,   xxxx,},
        {      2,         1,        6,        6,          0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map11 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,       xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,  W_NGANG,  W_NGANG,          0,            0,  W_NGANG,   S_DICH4,},
        {   xxxx,         0,   W_DOCP,        0,          0,     W_TRAI_D,        0,   xxxx,},
        {   xxxx,         0,        0, W_TRAI_D,   W_TRAI_D,       W_DOCP,        0,   xxxx,},
        {   xxxx,  W_PHAI_U,   W_DOCP,        0,           0,            0,  W_NGANG,   xxxx,},
        {   xxxx,         0, W_PHAI_D,        0,          0,      W_PHAI_U,        0,   xxxx,},
        {   xxxx,         0,        0,        0,          0,            0,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,       xxxx,         xxxx,     xxxx,   xxxx,},
        {      3,         5,        1,        6,          0,            0,        0,     0,}, //Toa do mummy va player
        };


    public static int[,] map14 = new int[,]
        {
        {   xxxx,      xxxx,  S_DICH2,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,  W_NGANG, W_PHAI_D,         0,       W_DOCP,        0,   xxxx,},
        {   xxxx,         0,  W_NGANG,  W_NGANG,   W_ALL_D,       W_DOCP,        0,   xxxx,},
        {   xxxx,    W_DOCP,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,   W_NGANG, W_PHAI_D,        0,  W_TRAI_U,      W_NGANG,        0,   xxxx,},
        {   xxxx,         0,        0,  W_NGANG,  W_TRAI_U,            0,  W_NGANG,   xxxx,},
        {   xxxx,    S_TRAP,        0,        0,         0,       W_DOCT,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      1,         5,        1,        3,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map15 = new int[,]
        {
        {   xxxx,      xxxx,  S_DICH2,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,   W_DOCP,        0,   W_NGANG,            0,  W_NGANG,   xxxx,},
        {   xxxx,         0,        0, W_PHAI_D,         0,            0,        0,   xxxx,},
        {   xxxx,  W_PHAI_D,   W_DOCP,        0,    W_DOCP,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,    W_DOCP,      W_NGANG,        0,   xxxx,},
        {   xxxx,    S_TRAP, W_TRAI_U, W_PHAI_D,         0,            0,        0,   xxxx,},
        {   xxxx,         0,   W_DOCP,        0,         0,       W_DOCT,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      6,         2,        1,        5,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map16 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,   W_DOCP,    S_KEY,            0,         0,   xxxx,},
        {   xxxx,         0, W_PHAI_D,        0,         0,     W_TRAI_U,        0,   xxxx,},
        { S_DICH3,        0,        0,  W_NGANG,    W_DOCP,            0,        0,   xxxx,},
        {   xxxx,    S_GATE,   W_DOCT,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,   W_DOCT,        0,   W_NGANG,       W_DOCP,   S_TRAP,   xxxx,},
        {   xxxx,         0,        0,        0,         0,            0, W_NGANG_T,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      4,         6,        2,        1,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map17 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,   W_DOCP,    S_KEY,    W_DOCP,            0,        0,   xxxx,},
        {   xxxx,         0,   W_DOCP,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,   W_DOCP,        0,    W_DOCP,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,  W_PHAI_U,            0,        0,   xxxx,},
        {S_DICH3,         0,        0,   W_NGANG,         0,       W_DOCP,   S_GATE,   xxxx,},
        {xxxx,       W_DOCP,        0,        0,    W_DOCP,            0,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      2,         4,        3,        6,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map18 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,  W_NGANG,  W_NGANG,         0,            0,        0,   xxxx,},
        {   xxxx,         0,   S_GATE,        0,         0,            0,        0,   S_DICH4,},
        {   xxxx,         0,   W_DOCP,   W_DOCP,     S_KEY,            0,        0,   xxxx,},
        {   xxxx,  W_PHAI_D,        0,        0,  W_TRAI_U,       W_DOCT,        0,   xxxx,},
        {   xxxx,         0,        0,        0,         0,            0,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      5,         4,        1,        6,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map19 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,      S_DICH2,     xxxx,   xxxx,},
        {   xxxx,         0,        0,   W_DOCP,    W_DOCP,      W_DOCP,        0,   xxxx,},
        {   xxxx,         0,        0, W_TRAI_D,         0,           0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,         0,       W_DOCP,   S_TRAP,   xxxx,},
        {   xxxx,         0, W_PHAI_D,        0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,   W_DOCP,        0,    W_DOCP,            0,        0,   xxxx,},
        {   xxxx,         0, W_PHAI_U,   W_DOCP,         0,            0,        0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      1,         1,        0,        0,         6,            2,        0,     0,}, //Toa do mummy va player
        };


    public static int[,] map20 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,         0,        0,            0,        0,   xxxx,},
        {   xxxx,   W_NGANG,    W_DOCP, W_TRAI_D,    W_DOCT,        W_DOCP,        0,   xxxx,},
        {   xxxx,         0,    S_TRAP,   W_DOCT,   W_DOCP,            0,    W_NGANG,   xxxx,},
        {   xxxx,    W_DOCP,         0,   W_DOCT,    W_DOCT,            0,        0,   xxxx,},
        {   xxxx,         0,        0,        0,    W_DOCP,            0,        0,   xxxx,},
        {   xxxx,         0, W_PHAI_U,   W_DOCP,         0,            0,        0,   xxxx,},
        {   xxxx,      xxxx,  S_DICH1,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      3,         4,        0,        0,         6,            2,        0,     0,}, //Toa do mummy va player
        };


    public static int[,] map21 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,     W_DOCP,       0,         0,        0,            0,        0,   xxxx,},
        {   xxxx,         0, W_PHAI_D,         0,   S_TRAP,     W_TRAI_U,        0,   xxxx,},
        {   xxxx,         0,  W_NGANG,  W_PHAI_U,        0,            0,  W_NGANG,   xxxx,},
        {   xxxx,         0,        0,  W_PHAI_U,        0,            0,  W_NGANG,   xxxx,},
        {   xxxx,         0,        0,  W_TRAI_D,  W_NGANG,      W_NGANG,        0,   xxxx,},
        {   xxxx,         0,        0,         0,        0,            0,   W_DOCT,   xxxx,},
        {   xxxx,      xxxx,  S_DICH1,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {      4,         3,        5,        1,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map22 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,        0,         0,         0,            0,        0,   xxxx,},
        {   xxxx,         0, W_PHAI_U,         0,  W_TRAI_D,            0,        0,   xxxx,},
        {   xxxx,         0,  W_NGANG,   W_NGANG,        0,             0,        0,   xxxx,},
        {   xxxx,   W_NGANG,        0,         0,   S_TRAP,     W_TRAI_U,         0,   xxxx,},
        {   xxxx,         0,        0,         0,  W_PHAI_U,            0,  W_TRAI_U,   xxxx,},
        {   xxxx,         0,   W_DOCP,         0,   W_NGANG_T,            0,         0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     S_DICH1,   xxxx,},
        {      2,         6,        6,        4,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map23 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,     xxxx,   xxxx,},
        {   xxxx,         0,   W_NGANG,         0,         0,            0,        0,   xxxx,},
        {   xxxx,         0,  W_PHAI_D,  W_PHAI_U,         0,     W_TRAI_U,   W_NGANG,   xxxx,},
        {S_DICH3,         0,        0,         0,     S_TRAP,            0,   W_NGANG,   xxxx,},
        {   xxxx,         0,  W_PHAI_D,  W_PHAI_D,    S_GATE,      W_NGANG,         0,   xxxx,},
        {   xxxx,   W_NGANG,    S_KEY,     W_TRAI_D,  W_NGANG,     W_NGANG,          0,   xxxx,},
        {   xxxx,         0,        0,         0,         0,            0,         0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,       xxxx,   xxxx,},
        {      1,         2,        4,        4,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map24 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,        xxxx,         xxxx,       xxxx,   xxxx,},
        {   xxxx,         0,        0,         0,         0 ,      W_NGANG,          0,   xxxx,},
        {   xxxx,    W_DOCP,        0,         0,     W_DOCP,       S_GATE,    W_NGANG,   xxxx,},
        {   xxxx,     S_KEY, W_PHAI_D,   W_NGANG,          0,      W_NGANG,          0,   xxxx,},
        {   xxxx,   W_NGANG,  W_NGANG,  W_NGANG,    W_NGANG,         W_DOCT,      W_NGANG,   xxxx,},
        {   xxxx,         0,        0,    S_TRAP,           0,           0,          0,   xxxx,},
        {   xxxx,         0,    W_DOCP,         0,         0,            0,         0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,     xxxx,      xxxx,         xxxx,       S_DICH1,   xxxx,},
        {      4,         5,        6,        3,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map25 = new int[,]
        {
        {   xxxx,      xxxx,  S_DICH2,     xxxx,        xxxx,         xxxx,       xxxx,   xxxx,},
        {   xxxx,         0,   W_DOCT,    W_NGANG,         0,      W_NGANG,          0,   xxxx,},
        {   xxxx,   W_NGANG,   W_DOCP,         0,          0,            0,    W_NGANG,   xxxx,},
        {   xxxx,         0,   W_DOCP,         0,    W_NGANG,            0,          0,   xxxx,},
        {   xxxx,         0, W_PHAI_D,         0,   W_TRAI_D,     W_PHAI_D,          0,   xxxx,},
        {   xxxx,         0, W_TRAI_D,   W_NGANG,          0,     S_TRAP,      W_DOCT,   xxxx,},
        {   xxxx,         0,        0,         0,          0,    W_NGANG_T,         0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,      xxxx,       xxxx,         xxxx,       xxxx,   xxxx,},
        {      5,         1,        1,        4,         0,            0,        0,     0,}, //Toa do mummy va player
        };


    public static int[,] map26 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,        xxxx,         xxxx,       xxxx,   xxxx,},
        {   xxxx,         0, W_TRAI_D,         0,          0,            0,          0,   xxxx,},
        {   xxxx,   W_NGANG,  W_NGANG,         0,          0,            0,          0,   xxxx,},
        {   xxxx,         0,   W_DOCP,         0,   W_PHAI_D,            0,          0,   xxxx,},
        {   xxxx,  W_PHAI_U,        0,     W_DOCP,    W_DOCP,       W_DOCP,          0,   xxxx,},
        {   xxxx,    S_TRAP, W_TRAI_D,         0,    W_NGANG,      W_NGANG,          0,   xxxx,},
        {   xxxx,         0,        0,         0,          0,    W_NGANG_T,         0,   xxxx,},
        {   xxxx,   S_DICH1,     xxxx,      xxxx,       xxxx,         xxxx,       xxxx,   xxxx,},
        {      4,         2,        6,        5,         0,            0,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map27 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,        xxxx,         xxxx,       xxxx,   xxxx,},
        {   xxxx,    S_TRAP, W_PHAI_D,         0,      W_NGANG,    W_NGANG,          0,   xxxx,},
        {   xxxx,         0,  W_NGANG,    W_NGANG,    W_TRAI_D,          0,    W_TRAI_D,   xxxx,},
        {   xxxx,    W_DOCP,        0,    W_NGANG,    W_NGANG,           0,          0,   S_DICH4,},
        {   xxxx,         0,        0,     W_DOCP,          0,     W_NGANG,     W_NGANG,   xxxx,},
        {   xxxx,   W_NGANG, W_NGANG,   W_PHAI_U,           0,      S_TRAP,          0,   xxxx,},
        {   xxxx,         0,        0,         0,           0,           0,          0,   xxxx,},
        {   xxxx,      xxxx,     xxxx,      xxxx,       xxxx,         xxxx,       xxxx,   xxxx,},
        {      6,         3,        5,        6,         0,            0,        0,     0,}, //Toa do mummy va player
        };


    public static int[,] map28 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,        xxxx,         xxxx,       xxxx,   xxxx,},
        {   xxxx,         0,  W_NGANG,         0,      W_DOCP,           0,      S_TRAP,   xxxx,},
        {   xxxx,         0,        0,    W_DOCP,           0,          0,    W_TRAI_U,   xxxx,},
        {   xxxx,         0, W_TRAI_U,  W_PHAI_U,    W_PHAI_U,           0,          0,   xxxx,},
        {   xxxx,         0,   W_DOCT,         0,          0,     0,     0,   xxxx,},
        {   xxxx,         0,         0,   W_TRAI_D,           0,      S_TRAP,          0,   xxxx,},
        {   xxxx,    W_DOCP,        0,         0,           0,        W_NGANG_T,          0,   xxxx,},
        {   xxxx,      S_DICH1,     xxxx,      xxxx,       xxxx,         xxxx,       xxxx,   xxxx,},
        {      1,         1,        0,        0,         6,            1,        0,     0,}, //Toa do mummy va player
        };


    public static int[,] map29 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,        xxxx,         xxxx,          xxxx,   xxxx,},
        {   xxxx,         0,  W_TRAI_D,         0,           0,           0,       S_TRAP,   xxxx,},
        {   xxxx,         0,   W_NGANG,    W_DOCP,           0,   W_NGANG_T,    W_NGANG_T,   xxxx,},
        {   xxxx,         0,  W_NGANG,   W_PHAI_D,           0,    W_TRAI_D,            0,   xxxx,},
        {   xxxx,         0,        0,   W_TRAI_D,     W_NGANG,           0,      W_NGANG,   xxxx,},
        {   xxxx,   W_PHAI_D,    0,   W_DOCP,           0,     W_DOCP,          0,    S_DICH4,},
        {   xxxx,         0,    S_TRAP,         0,           0,           0,         0,  xxxx,},
        {   xxxx,      xxxx,     xxxx,      xxxx,       xxxx,         xxxx,          xxxx,   xxxx,},
        {      1,         2,        0,        0,         5,            6,        0,     0,}, //Toa do mummy va player
        };

    public static int[,] map30 = new int[,]
        {
        {   xxxx,      xxxx,     xxxx,     xxxx,        xxxx,         xxxx,          xxxx,   xxxx,},
        {   xxxx,    S_TRAP,        0,   W_NGANG,           0,      W_NGANG,       0,   xxxx,},
        {   xxxx,         0,        0,     S_KEY,      W_DOCP,     0,    W_NGANG,   S_DICH4,},
        {   xxxx,         0,  S_TRAP,   W_NGANG,           0,      0,            0,   xxxx,},
        {   xxxx,         0,  S_GATE,   W_DOCT,     0,             0,      W_DOCT,   xxxx,},
        {   xxxx,         0,       0,   W_DOCT,        W_DOCP,     0,          0,    xxxx,},
        {   xxxx,    W_DOCP,    0,       W_NGANG_T,           0,           0,         0,  xxxx,},
        {   xxxx,      xxxx,     xxxx,      xxxx,       xxxx,         xxxx,          xxxx,   xxxx,},
        {      4,         3,        1,        4,         0,            0,        0,     0,}, //Toa do mummy va player
        };


    public static void InitALLMAP()
    {
        if (allMAP.Count > 0) return;
        else
        {
            allMAP.Add(map0);
            allMAP.Add(map1);
            allMAP.Add(map2);
            allMAP.Add(map3);
            allMAP.Add(map4);
            allMAP.Add(map5);
            allMAP.Add(map6);
            allMAP.Add(map7);
            allMAP.Add(map9);
            allMAP.Add(map10);
            allMAP.Add(map11);
            allMAP.Add(map12);
            allMAP.Add(map13);
            allMAP.Add(map14);
            allMAP.Add(map15);
            allMAP.Add(map16);
            allMAP.Add(map17);
            allMAP.Add(map18);
            allMAP.Add(map19);
            allMAP.Add(map20);
            allMAP.Add(map21);
            allMAP.Add(map22);
            allMAP.Add(map23);
            allMAP.Add(map24);
            allMAP.Add(map25);
            allMAP.Add(map26);
            allMAP.Add(map27);
            allMAP.Add(map28);
            allMAP.Add(map29);
            allMAP.Add(map30);
        }
    }

}
