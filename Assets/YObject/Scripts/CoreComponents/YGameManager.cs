using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Structures;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class YGameManager : MonoBehaviour
{
    private static YGameManager _instance;
    public static YGameManager Instance
    {
        get
        {
            if ( _instance == null )
                _instance = FindAnyObjectByType<YGameManager>();
            return _instance;
        }
    }
    public YIDsManager IDsManager;





    private List<YTrigger> globalBeginTriggers = new List<YTrigger>();
    private List<YTrigger> globalTickTriggers = new List<YTrigger>();
    private List<YGDObject> globalInitGDObjects = new List<YGDObject>();
    private Dictionary<string, List<YTrigger>> groupsBeginTriggers = new Dictionary<string, List<YTrigger>>();
    private Dictionary<string, List<YTrigger>> groupsTickTriggers = new Dictionary<string, List<YTrigger>>();
    private Dictionary<string, List<YGDObject>> groupsInitGDObjects = new Dictionary<string, List<YGDObject>>();


    public string levelName = "Level";
    public string sampleLevelName = "SampleLevel";
    public LevelSavingType levelSavingType;
    public bool updateLevel;

    public enum LevelSavingType
    {
        Override = 0,
        UseSample = 1,
    }
    public void SaveLevel()
    {
        InitAll();

        string objs = "";

        Vector2 pos = new Vector2(100, 100);

        foreach (var gdObject in globalInitGDObjects)
        {
            objs += gdObject.GetString(pos);
            pos.x += 2;
        }
        foreach (var trigger in globalBeginTriggers)
        {
            trigger.AddGroup(1001);
            objs += trigger.GetString(pos);
            pos.x += 2;
        }
        foreach (var trigger in globalTickTriggers)
        {
            trigger.AddGroup(1000);
            objs += trigger.GetString(pos);
            pos.x += 2;
        }

        SaveLevelString(objs, levelName, updateLevel, null, 2010, levelSavingType, sampleLevelName, 2010);
    }
    public void CreateSampleLevel()
    {
        string objs = "";
        SaveLevelString(objs, sampleLevelName, false, new List<Gradient>(), 300);
    }




    private void Awake()
    {
        //ItemEdit itemEdit = new ItemEdit(1, ItemEdit.Operation.Add, 1, 1, ItemEdit.Operation.Add, true, true, true, 1, ItemEdit.Operation2.None, ItemEdit.Operation3.None);

        //Spawn spawn = new Spawn(123, false, 0.5f, new Dictionary<int, int> { { 100, 200 }, { 101, 201 } });

        //print(spawn.GetString(Vector2.zero, new int[] { 1,3 }));

        //PrintLevelStringAsync("t");



        InitAll();
        BeginAll();

        //foreach (var variable in variables)
        //{
        //    print(variable.Key);
        //    print(variable.Value.Item1);
        //}
    }
    private async void AAA()
    {
        var local = await LocalLevels.LoadFileAsync();
        string level = await PrintLevelStringAsync("newSample");

        File.WriteAllText(Application.dataPath + "/sampleLevel.txt", level);
    }
    private async Task<string> PrintLevelStringAsync(string levelName)
    {
        var local = await LocalLevels.LoadFileAsync();
        var levelInfo = local.GetLevel(levelName);
        print(Level.Decompress(levelInfo.LevelString));
        return Level.Decompress(levelInfo.LevelString);
    }

    private void FixedUpdate()
    {
        UpdateCoreVariables();
        TickAll();
    }


    private void UpdateCoreVariables()
    {
        IDsManager.SetMemoryValueByName("Input.P1Left", Input.GetKey(KeyCode.A) ? 1f : 0f);
        IDsManager.SetMemoryValueByName("Input.P1Right", Input.GetKey(KeyCode.D) ? 1f : 0f);
        IDsManager.SetMemoryValueByName("Input.P1Up", Input.GetKey(KeyCode.W) ? 1f : 0f);
        IDsManager.SetMemoryValueByName("Input.P2Left", Input.GetKey(KeyCode.LeftArrow) ? 1f : 0f);
        IDsManager.SetMemoryValueByName("Input.P2Right", Input.GetKey(KeyCode.RightArrow) ? 1f : 0f);
        IDsManager.SetMemoryValueByName("Input.P2Up", Input.GetKey(KeyCode.UpArrow) ? 1f : 0f);
        IDsManager.SetMemoryValueByName("Time.deltaTime", Time.fixedDeltaTime);
        IDsManager.SetMemoryValueByName("Time.time", Time.time);
    }

    private void InitAll()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        IDsManager = new YIDsManager();




        globalInitGDObjects.Clear();
        globalBeginTriggers.Clear();
        globalTickTriggers.Clear();
        groupsBeginTriggers.Clear();
        groupsTickTriggers.Clear();
        groupsInitGDObjects.Clear();


        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(true))
        {
            yMono.Uninit();
        }

        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(true))
        {
            globalInitGDObjects.AddRange(yMono.Init());
        }
        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(true))
        {
            globalBeginTriggers.AddRange(yMono.Begin());
            globalTickTriggers.AddRange(yMono.Tick());
        }
    }
    private void BeginAll()
    {
        foreach (YTrigger trigger in globalBeginTriggers)
        {
            trigger.Activate();
        }
    }
    private void TickAll()
    {
        foreach (YTrigger trigger in globalTickTriggers)
        {
            trigger.Activate();
        }
    }
    private async void SaveLevelString(string objs, string levelName, bool updateLevel, List<Gradient> colors, int lastgroup, YGameManager.LevelSavingType levelSavingType = YGameManager.LevelSavingType.Override, string sampleName = "", int startGroup = -1)
    {
        var local = await LocalLevels.LoadFileAsync();

        YStaticData.savingLevelSample = await File.ReadAllTextAsync(Application.dataPath + "/sampleLevel.txt");

        if (local.LevelExists(levelName) && !updateLevel)
        {
            print("Are you sure you want to update level file?");
            return;
        }
        if (!local.LevelExists(levelName) && updateLevel)
        {
            print("Are you sure you want to update not existing level?");
            return;
        }
        if (!local.LevelExists(levelName))
        {
            string leveltext = YStaticData.savingLevelSample;
            if (levelSavingType == YGameManager.LevelSavingType.UseSample)
            {
                var li = local.GetLevel(sampleName);
                leveltext = Level.Decompress(li.LevelString);
            }
            var levelString = leveltext + objs;
            Level savingLevel = new Level(levelString, false);
            int colorChannelID = -1;
            foreach (UnityEngine.Color color in Resources.Load<YProjectSettings>("YProjectSettings").colorChannels)
            {
                colorChannelID++;
                if (color == null || color == UnityEngine.Color.white || colorChannelID < 1)
                    continue;


                string col = "#" + color.ToHexString().ToLower().Substring(0, 6);
                savingLevel.AddColor(new GeometryDashAPI.Levels.Color(colorChannelID)
                {
                    Rgb = RgbColor.FromHex(col)
                });
            }
            //for (int i = 0; i < colors.Count; i++)
            //{
            //    string col = "#" + colors[i].color1.ToHexString().ToLower().Substring(0, 6);
            //    savingLevel.AddColor(new GeometryDashAPI.Levels.Color(2 * i + 3)
            //    {
            //        Rgb = RgbColor.FromHex(col)
            //    });
            //    col = "#" + colors[i].color2.ToHexString().ToLower().Substring(0, 6);
            //    savingLevel.AddColor(new GeometryDashAPI.Levels.Color(2 * i + 4)
            //    {
            //        Rgb = RgbColor.FromHex(col)
            //    });
            //}

            var levelInfo = LevelCreatorModel.CreateNew(levelName, "Yobj");
            levelInfo.SaveLevel(savingLevel);
            local.AddLevel(levelInfo);
        }
        else
        {
            string leveltext = YStaticData.savingLevelSample;
            if (levelSavingType == YGameManager.LevelSavingType.UseSample)
            {

                var li = local.GetLevel(sampleName);
                leveltext = Level.Decompress(li.LevelString);

            }
            var levelInfo = local.GetLevel(levelName);
            string level = leveltext + objs; //Level.Decompress(levelInfo.LevelString) + objs;
            Level savingLevel = new Level(level, false);

            int colorChannelID = -1;
            foreach (UnityEngine.Color color in Resources.Load<YProjectSettings>("YProjectSettings").colorChannels)
            {
                colorChannelID++;
                if (color == null || color == UnityEngine.Color.white || colorChannelID < 1)
                    continue;


                string col = "#" + color.ToHexString().ToLower().Substring(0, 6);
                savingLevel.AddColor(new GeometryDashAPI.Levels.Color(colorChannelID)
                {
                    Rgb = RgbColor.FromHex(col)
                });
            }

            //for (int i = 0; i < colors.Count; i++)
            //{
            //    string col = "#" + colors[i].color1.ToHexString().ToLower().Substring(0, 6);
            //    savingLevel.AddColor(new GeometryDashAPI.Levels.Color(2 * i + 3)
            //    {
            //        Rgb = RgbColor.FromHex(col) // orange
            //    });
            //    col = "#" + colors[i].color2.ToHexString().ToLower().Substring(0, 6);
            //    savingLevel.AddColor(new GeometryDashAPI.Levels.Color(2 * i + 4)
            //    {
            //        Rgb = RgbColor.FromHex(col) // orange
            //    });
            //}
            levelInfo.SaveLevel(savingLevel);
        }
        local.Save();
        print("Saved");
        print("Last Group: " + lastgroup + (startGroup != -1 ? " ,Groups Used: " + (lastgroup - startGroup) : ""));
    }
}