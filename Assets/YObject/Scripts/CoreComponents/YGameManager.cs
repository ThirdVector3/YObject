using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Enums;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.Structures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    public YGameobjectGroupsManager GameobjectGroupsManager;
    public YSoundManager SoundManager;


    private static List<YServiceBase> services;
    

    public List<YTrigger> globalPool;
    public List<YGDObject> globalGDObjectsPool;
    public bool transportingToGd = false;
    public bool gameobjectsAndServicesInitialization = false;



    public List<YTrigger> globalBeginTriggers = new List<YTrigger>();
    private List<YTrigger> globalTickTriggers = new List<YTrigger>();
    private List<YGDObject> globalInitGDObjects = new List<YGDObject>();
    public Dictionary<string, List<YTrigger>> groupsBeginTriggers = new Dictionary<string, List<YTrigger>>();
    private Dictionary<string, List<YTrigger>> groupsTickTriggers = new Dictionary<string, List<YTrigger>>();
    private Dictionary<string, List<YGDObject>> groupsInitGDObjects = new Dictionary<string, List<YGDObject>>();
    public Dictionary<string, int> groupsGroup = new Dictionary<string, int>();
    public Dictionary<string, int> groupsBeginGroup = new Dictionary<string, int>();
    public Dictionary<string, List<GameObject>> groupsGameobject = new Dictionary<string, List<GameObject>>();



    public string levelName = "Level";
    public string sampleLevelName = "SampleLevel";
    public LevelSavingType levelSavingType;
    public bool updateLevel;
    public int firstFreeID = 600;
    public string playerName = "YObject";

    public enum LevelSavingType
    {
        Override = 0,
        UseSample = 1,
    }

    public static T GetService<T>() where T : class
    {
        foreach (var service in services)
        {
            if (service is T)
                return service as T;
        }
        return null;
    }

    public void SaveLevel()
    {
        transportingToGd = true;
        InitAll();

        string objs = "";

        Vector2 pos = new Vector2(100, 100);

        foreach (var group in groupsGroup)
        {
            var toggle = new Toggle(group.Value, false);
            toggle.AddGroup(1001);
            objs += toggle.GetString(pos);
            pos.x += 2;
        }

        foreach (var gdObject in globalInitGDObjects)
        {
            if (gdObject.pos != Vector2.zero)
                objs += gdObject.GetString(gdObject.pos);
            else
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

        foreach (var gr in groupsGroup)
        {
            int groupOfGroup = gr.Value;
            int groupOfStart = groupsBeginGroup[gr.Key];

            foreach (var yGDObject in groupsInitGDObjects[gr.Key])
            {
                if (yGDObject.useGroupsGroup)
                    yGDObject.AddGroup(groupOfGroup, true);



                if (yGDObject.pos != Vector2.zero)
                    objs += yGDObject.GetString(yGDObject.pos);
                else
                    objs += yGDObject.GetString(pos);
                pos.x += 2;
            }

            foreach (var trigger in groupsBeginTriggers[gr.Key])
            {
                trigger.AddGroup(groupOfStart);
                trigger.AddGroup(groupOfGroup, true);
                objs += trigger.GetString(pos);
                pos.x += 2;
            }
            foreach (var trigger in groupsTickTriggers[gr.Key])
            {
                trigger.AddGroup(groupOfGroup, true);
                trigger.AddGroup(1000);
                objs += trigger.GetString(pos);
                pos.x += 2;
            }
        }

        SaveLevelString(objs, levelName, updateLevel, 2010, levelSavingType, sampleLevelName, firstFreeID);
    }
    public void CreateSampleLevel()
    {
        IDsManager = new YIDsManager(500);
        string objs = "";
        SaveLevelString(objs, sampleLevelName, false, 500);
    }




    private void Awake()
    {
        _instance = this;
        //ItemEdit itemEdit = new ItemEdit(1, ItemEdit.Operation.Add, 1, 1, ItemEdit.Operation.Add, true, true, true, 1, ItemEdit.Operation2.None, ItemEdit.Operation3.None);

        //Spawn spawn = new Spawn(123, false, 0.5f, new Dictionary<int, int> { { 100, 200 }, { 101, 201 } });

        //print(spawn.GetString(Vector2.zero, new int[] { 1,3 }));

        //PrintLevelStringAsync("t");


        transportingToGd = false;

        SoundManager = new YSoundManager();
        SoundManager.Init();

        InitAll();
        BeginAll();

        //foreach (var variable in variables)
        //{
        //    print(variable.Key);
        //    print(variable.Value.Item1);
        //}

    }
    private void OnApplicationQuit()
    {
        YColorManager.InitColors();
    }
    private async void ImportSampleLevel()
    {
        var local = await LocalLevels.LoadFileAsync();
        string level = await PrintLevelStringAsync("SampleLevelA");

        File.WriteAllText(Application.dataPath + "/sampleLevel.txt", level);
        print("imported");
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
        IDsManager.SetMemoryValueByName("PI", 3.1415926535f);
    }

    private int startRecordingPoolIndex = 0;
    public void RecordPool()
    {
        startRecordingPoolIndex = globalPool.Count;
    }
    public YTrigger[] StopRecordPool()
    {
        return globalPool.GetRange(startRecordingPoolIndex, globalPool.Count - startRecordingPoolIndex).ToArray();
    }

    private void InitAll()
    {
        _instance = this;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        IDsManager = new YIDsManager(firstFreeID);
        GameobjectGroupsManager = new YGameobjectGroupsManager();

        globalPool = new List<YTrigger>();
        globalGDObjectsPool = new List<YGDObject>();

        services = GetAllServices();


        globalInitGDObjects.Clear();
        globalBeginTriggers.Clear();
        globalTickTriggers.Clear();
        groupsBeginTriggers.Clear();
        groupsTickTriggers.Clear();
        groupsInitGDObjects.Clear();
        groupsGroup.Clear();
        groupsBeginGroup.Clear();
        groupsGameobject.Clear();


        IDsManager.SetCurrentGroupName(null);
        YColorManager.InitColors();

        YCoroutines.Init();

        InitServices();
        InitGameobjects();
    }

    private List<YServiceBase> GetAllServices()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(YServiceBase)) && !type.IsAbstract)
            .Select(type => Activator.CreateInstance(type) as YServiceBase)
            .ToList();
    }
    private void InitServices()
    {
        IDsManager.SetCurrentGroupName(null);
        foreach (var yService in services)
        {
            yService.Uninit();
        }
        gameobjectsAndServicesInitialization = true;
        foreach (var yService in services)
        {
            globalGDObjectsPool.Clear();

            yService.Init();
            foreach (YGDObject GDObject in globalGDObjectsPool)
            {
                if (GDObject.isFirstLevel)
                {
                    globalInitGDObjects.Add(GDObject);
                }
            }
        }
        gameobjectsAndServicesInitialization = false;
        foreach (var yService in services)
        {
            globalPool.Clear();
            yService.Begin();
            foreach (YTrigger trigger in globalPool)
            {
                if (trigger.isFirstLevel)
                {
                    globalBeginTriggers.Add(trigger);
                }
            }

            globalPool.Clear();
            yService.Tick();
            foreach (YTrigger trigger in globalPool)
            {
                if (trigger.isFirstLevel)
                {
                    globalTickTriggers.Add(trigger);
                }
            }
        }
    }
    private void InitGameobjects()
    {
        IDsManager.SetCurrentGroupName(null);
        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(false))
        {
            yMono.Uninit();
            if (yMono.TryGetComponent(out YGameobjectGroup yGameobjectGroup) && !groupsBeginTriggers.ContainsKey(yGameobjectGroup.GetName()))
            {
                groupsBeginTriggers.Add(yGameobjectGroup.GetName(), new List<YTrigger>());
                groupsTickTriggers.Add(yGameobjectGroup.GetName(), new List<YTrigger>());
                groupsInitGDObjects.Add(yGameobjectGroup.GetName(), new List<YGDObject>());
                groupsGameobject.Add(yGameobjectGroup.GetName(), new List<GameObject>());

                int gr = IDsManager.GetFreeGroup(null);
                IDsManager.AddGroup(gr, null);
                groupsGroup.Add(yGameobjectGroup.GetName(), gr);

                gr = IDsManager.GetFreeGroup(null);
                IDsManager.AddGroup(gr, null);
                groupsBeginGroup.Add(yGameobjectGroup.GetName(), gr);
            }

            if (yMono.TryGetComponent(out YGameobjectGroup yGameobjectGroup2))
            {
                if (!groupsGameobject[yGameobjectGroup2.GetName()].Contains(yMono.gameObject))
                {
                    groupsGameobject[yGameobjectGroup2.GetName()].Add(yMono.gameObject);
                }
            }
        }
        gameobjectsAndServicesInitialization = true;
        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(false))
        {
            if (yMono.GetComponent<YGameobjectGroup>() == null)
            {
                globalGDObjectsPool.Clear();

                yMono.Init();
                foreach (YGDObject GDObject in globalGDObjectsPool)
                {
                    if (GDObject.isFirstLevel)
                    {
                        globalInitGDObjects.Add(GDObject);
                    }
                }
            }
        }
        gameobjectsAndServicesInitialization = false;
        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(false))
        {
            if (yMono.GetComponent<YGameobjectGroup>() == null && (yMono is YTransform || yMono is YMainCamera))
            {
                globalPool.Clear();
                //var a =
                yMono.Begin();
                //if (a != null)
                //    globalBeginTriggers.AddRange(a);
                foreach (YTrigger trigger in globalPool)
                {
                    if (trigger.isFirstLevel)
                    {
                        globalBeginTriggers.Add(trigger);
                    }
                }

                globalPool.Clear();
                //var b = 
                yMono.Tick();
                //if (b != null)
                //    globalTickTriggers.AddRange(b);
                foreach (YTrigger trigger in globalPool)
                {
                    if (trigger.isFirstLevel)
                    {
                        globalTickTriggers.Add(trigger);
                    }
                }
            }
        }
        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(false))
        {
            if (yMono.GetComponent<YGameobjectGroup>() == null && !(yMono is YTransform || yMono is YMainCamera))
            {
                globalPool.Clear();
                //var a =
                yMono.Begin();
                //if (a != null)
                //    globalBeginTriggers.AddRange(a);
                foreach (YTrigger trigger in globalPool)
                {
                    if (trigger.isFirstLevel)
                    {
                        globalBeginTriggers.Add(trigger);
                    }
                }

                globalPool.Clear();
                //var b =
                yMono.Tick();
                //if (b != null)
                //    globalTickTriggers.AddRange(b);
                foreach (YTrigger trigger in globalPool)
                {
                    if (trigger.isFirstLevel)
                    {
                        globalTickTriggers.Add(trigger);
                    }
                }
            }
        }


        IDsManager.InitGroups(groupsGroup.Keys.ToArray());
        gameobjectsAndServicesInitialization = true;


        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(false))
        {
            if (yMono.TryGetComponent(out YGameobjectGroup yGameobjectGroup))
            {
                IDsManager.SetCurrentGroupName(yGameobjectGroup.GetName());

                globalGDObjectsPool.Clear();

                yMono.Init();
                foreach (YGDObject GDObject in globalGDObjectsPool)
                {
                    if (GDObject.isFirstLevel)
                    {
                        groupsInitGDObjects[yGameobjectGroup.GetName()].Add(GDObject);
                    }
                }
            }
        }
        gameobjectsAndServicesInitialization = false;
        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(false))
        {
            if (yMono.TryGetComponent(out YGameobjectGroup yGameobjectGroup) && (yMono is YTransform || yMono is YMainCamera))
            {
                IDsManager.SetCurrentGroupName(yGameobjectGroup.GetName());
                globalPool.Clear();
                //var a =
                yMono.Begin();
                //if (a != null)
                //    groupsBeginTriggers[yGameobjectGroup.GetName()].AddRange(a);
                foreach (YTrigger trigger in globalPool)
                {
                    if (trigger.isFirstLevel)
                    {
                        groupsBeginTriggers[yGameobjectGroup.GetName()].Add(trigger);
                    }
                }

                globalPool.Clear();
                //var b =
                yMono.Tick();
                //if (b != null)
                //    groupsTickTriggers[yGameobjectGroup.GetName()].AddRange(b);
                foreach (YTrigger trigger in globalPool)
                {
                    if (trigger.isFirstLevel)
                    {
                        groupsTickTriggers[yGameobjectGroup.GetName()].Add(trigger);
                    }
                }
            }
        }
        foreach (var yMono in FindObjectsOfType<YMonoBehaviour>(false))
        {
            if (yMono.TryGetComponent(out YGameobjectGroup yGameobjectGroup) && !(yMono is YTransform || yMono is YMainCamera))
            {
                IDsManager.SetCurrentGroupName(yGameobjectGroup.GetName());
                globalPool.Clear();
                //var a =
                yMono.Begin();
                //if (a != null)
                //    groupsBeginTriggers[yGameobjectGroup.GetName()].AddRange(a);
                foreach (YTrigger trigger in globalPool)
                {
                    if (trigger.isFirstLevel)
                    {
                        groupsBeginTriggers[yGameobjectGroup.GetName()].Add(trigger);
                    }
                }

                globalPool.Clear();
                //var b =
                yMono.Tick();
                //if (b != null)
                //    groupsTickTriggers[yGameobjectGroup.GetName()].AddRange(b);
                foreach (YTrigger trigger in globalPool)
                {
                    if (trigger.isFirstLevel)
                    {
                        groupsTickTriggers[yGameobjectGroup.GetName()].Add(trigger);
                    }
                }
            }
        }
    }

    private void BeginAll()
    {
        foreach (var group in groupsGameobject)
        {
            foreach (var go in group.Value)
            {
                go.SetActive(false);
            }
        }
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
        if (GameobjectGroupsManager.CurrentGroup != null)
        {
            if (groupsTickTriggers.ContainsKey(GameobjectGroupsManager.CurrentGroup))
            {
                foreach (YTrigger trigger in groupsTickTriggers[GameobjectGroupsManager.CurrentGroup])
                {
                    trigger.Activate();
                }
            }
        }
    }
    private async void SaveLevelString(string objs, string levelName, bool updateLevel, int lastgroup, LevelSavingType levelSavingType = LevelSavingType.Override, string sampleName = "", int startGroup = -1)
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
            if (levelSavingType == LevelSavingType.UseSample)
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

            var levelInfo = LevelCreatorModel.CreateNew(levelName, playerName);
            levelInfo.SaveLevel(savingLevel);
            local.AddLevel(levelInfo);
        }
        else
        {
            string leveltext = YStaticData.savingLevelSample;
            if (levelSavingType == LevelSavingType.UseSample)
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
            levelInfo.SaveLevel(savingLevel);
        }
        local.Save();
        print("Saved");
        print("Last Group: " + Instance.IDsManager.GetFreeGroup() + (startGroup != -1 ? " ,Groups Used: " + (Instance.IDsManager.GetFreeGroup() - startGroup) : ""));
    }
}