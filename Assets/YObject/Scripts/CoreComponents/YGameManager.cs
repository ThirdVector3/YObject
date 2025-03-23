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
using static UnityEngine.Rendering.DebugUI;

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

    private int[] takenGroups = new int[]
    {
        689,
        1000,
        1001,
        1002,
        1003,
        6000,
        6001,
        6002,
        6003,
        6004,
        6005,
        6006
    };

    private Dictionary<string, (int, bool)> variables = new Dictionary<string, (int, bool)>();
    private (int, float)[] memory = new (int, float)[10000];
    private List<int> pickedGroups = new List<int>();


    private List<YTrigger> beginTriggers = new List<YTrigger>();
    private List<YTrigger> tickTriggers = new List<YTrigger>();
    private List<YGDObject> initGDObjects = new List<YGDObject>();


    public string levelName = "Level";
    public string sampleLevelName = "SampleLevel";
    public LevelSavingType levelSavingType;
    public bool updateLevel;

    public void AddVariable(string name, int id, bool isFloat)
    {
        if (variables.ContainsKey(name))
        {
            throw new System.Exception("More than one variable with same name");
        }

        variables.Add(name, (id, isFloat));
    }
    public void RemoveVariable(string name)
    {
        if (!variables.ContainsKey(name))
        {
            throw new System.Exception("No variable with that name");
        }

        variables.Remove(name);
    }
    public void SetMemoryValue(int id, float value)
    {
        memory[id] = (memory[id].Item1, value);
    }
    public void SetMemoryValue(int id, int value)
    {
        memory[id] = (value, memory[id].Item2);
    }
    public void SetMemoryValueByName(string name, float value)
    {
        if (GetIsFloatByName(name))
        {
            memory[GetIdByName(name)] = (memory[GetIdByName(name)].Item1, value);
        }
    }
    public void SetMemoryValueByName(string name, int value)
    {
        if (!GetIsFloatByName(name))
        {
            memory[GetIdByName(name)] = (value, memory[GetIdByName(name)].Item1);
        }
    }
    public (int, float) GetMemoryValue(int id)
    {
        return memory[id];
    }
    public (int, float) GetMemoryValueByName(string name)
    {
        return GetMemoryValue(GetIdByName(name));
    }
    public int GetIdByName(string name)
    {
        return variables[name].Item1;
    }
    public bool GetIsFloatByName(string name)
    {
        return variables[name].Item2;
    }
    public int GetFreeIdInt()
    {
        List<int> allIds = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIds.Add(i);
        }
        foreach (var variable in variables.Values)
        {
            if (variable.Item2 == false)
            {
                allIds.Remove(variable.Item1);
            }
        }
        return allIds.Min();
    }
    public int GetFreeIdFloat()
    {
        List<int> allIds = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIds.Add(i);
        }
        foreach (var variable in variables.Values)
        {
            if (variable.Item2 == true)
            {
                allIds.Remove(variable.Item1);
            }
        }
        return allIds.Min();
    }
    public int GetFreeGroup()
    {
        List<int> allIds = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIds.Add(i);
        }
        foreach (var variable in takenGroups)
        {
            allIds.Add(variable);
        }
        foreach (var group in pickedGroups)
        {
            allIds.Remove(group);
        }
        return allIds.Min();
    }
    public void AddGroup(int group)
    {
        if (!pickedGroups.Contains(group))
            pickedGroups.Add(group);
    }
    public int GetFreeIdFloatAndGroup()
    {
        List<int> allIdsGroup = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIdsGroup.Add(i);
        }
        foreach (var variable in takenGroups)
        {
            allIdsGroup.Add(variable);
        }
        foreach (var group in pickedGroups)
        {
            allIdsGroup.Remove(group);
        }


        List<int> allIdsFloat = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIdsFloat.Add(i);
        }
        foreach (var variable in variables.Values)
        {
            if (variable.Item2 == true)
            {
                allIdsFloat.Remove(variable.Item1);
            }
        }


        while (allIdsGroup.Count > 0)
        {
            if (allIdsFloat.Contains(allIdsGroup.Min()))
                return allIdsGroup.Min();

            allIdsGroup.Remove(allIdsGroup.Min());
        }
        return 9999;
    }
    public int GetFreeIdIntAndGroup()
    {
        List<int> allIdsGroup = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIdsGroup.Add(i);
        }
        foreach (var variable in takenGroups)
        {
            allIdsGroup.Add(variable);
        }
        foreach (var group in pickedGroups)
        {
            allIdsGroup.Remove(group);
        }


        List<int> allIdsInt = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIdsInt.Add(i);
        }
        foreach (var variable in variables.Values)
        {
            if (variable.Item2 == false)
            {
                allIdsInt.Remove(variable.Item1);
            }
        }


        while (allIdsGroup.Count > 0)
        {
            if (allIdsInt.Contains(allIdsGroup.Min()))
                return allIdsGroup.Min();

            allIdsGroup.Remove(allIdsGroup.Min());
        }
        return 9999;
    }

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

        foreach (var gdObject in initGDObjects)
        {
            objs += gdObject.GetString(pos);
            pos.x += 2;
        }
        foreach (var trigger in beginTriggers)
        {
            objs += trigger.GetString(pos, new int[] { 1001 });
            pos.x += 2;
        }
        foreach (var trigger in tickTriggers)
        {
            objs += trigger.GetString(pos, new int[] { 1000 });
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
        SetMemoryValueByName("Input.P1Left", Input.GetKey(KeyCode.A) ? 1f : 0f);
        SetMemoryValueByName("Input.P1Right", Input.GetKey(KeyCode.D) ? 1f : 0f);
        SetMemoryValueByName("Input.P1Up", Input.GetKey(KeyCode.W) ? 1f : 0f);
        SetMemoryValueByName("Input.P2Left", Input.GetKey(KeyCode.LeftArrow) ? 1f : 0f);
        SetMemoryValueByName("Input.P2Right", Input.GetKey(KeyCode.RightArrow) ? 1f : 0f);
        SetMemoryValueByName("Input.P2Up", Input.GetKey(KeyCode.UpArrow) ? 1f : 0f);
        SetMemoryValueByName("Time.deltaTime", Time.fixedDeltaTime);
        SetMemoryValueByName("Time.time", Time.time);
    }

    private void InitAll()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        memory = new (int, float)[10000];
        variables = new Dictionary<string, (int, bool)>()
        {
            {"Camera.position.x", (1, true) },
            {"Camera.position.y", (2, true) },
            {"Camera.position.z", (3, true) },

            {"Camera.rotation.x", (4, true) },
            {"Camera.rotation.y", (5, true) },
            {"Camera.rotation.z", (11, true) },

            {"Camera.rotation.sin.x", (6, true) },
            {"Camera.rotation.sin.y", (7, true) },
            {"Camera.rotation.sin.z", (12, true) },

            {"Camera.rotation.cos.x", (8, true) },
            {"Camera.rotation.cos.y", (9, true) },
            {"Camera.rotation.cos.z", (13, true) },

            {"Camera.focalLen", (16, true) },

            {"PI", (10, true) },

            {"Input.P1Left", (17, true) },
            {"Input.P1Right", (18, true) },
            {"Input.P1Up", (19, true) },

            {"Input.P2Left", (20, true) },
            {"Input.P2Right", (21, true) },
            {"Input.P2Up", (22, true) },

            {"ZERO", (23, true) },

            {"Time.deltaTime", (14, true) },
            {"Time.time", (15, true) },
        };
        pickedGroups = new List<int>();



        initGDObjects.Clear();
        beginTriggers.Clear();
        tickTriggers.Clear();

        foreach (var yMono in GameObject.FindObjectsOfType<YMonoBehaviour>(true))
        {
            yMono.Uninit();
        }

        foreach (var yMono in GameObject.FindObjectsOfType<YMonoBehaviour>(true))
        {
            initGDObjects.AddRange(yMono.Init());
            beginTriggers.AddRange(yMono.Begin());
            tickTriggers.AddRange(yMono.Tick());
        }
    }
    private void BeginAll()
    {
        foreach (YTrigger trigger in beginTriggers)
        {
            trigger.Activate();
        }
    }
    private void TickAll()
    {
        foreach (YTrigger trigger in tickTriggers)
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