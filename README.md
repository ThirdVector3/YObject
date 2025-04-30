# ![](imgs/yobjectLogomini.png) YObject
Unity to GD tool

## 🚀 Philosophy

The core principles behind YObject:

- Almost the same approach to game creation as in Unity
- Group objects "see" global objects but global objects doesn't "see" group objects


## 🛠️ Quick Start
For a quick start you need to have YGameManager and Camera with YMainCamera component on scene.

![](imgs/3.png)

Let's get started with adding simple cube on scene. You can create object and put YMeshRenderer on it. Then you can put mesh on this object by selecting mesh and pressing "Create Mesh".

![](imgs/1.png)

Then you can create YMonoBehaviour (NOT MonoBehaviour) class (it will be your component). Implement there Init, Begin and Tick methods.
- In Init you can create variables, objects, coroutines etc.
- In Begin you can create triggers that will activate at start of game (or when group is loaded if gameobject has a group)
- In Tick you can create triggers that will activate every tick
```cs
public class FlyCamera : YMonoBehaviour
{
    public override YTrigger[] Begin()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        triggers.Add(YCoroutines.StartCoroutine(yCoroutine));

        triggers.Add(new ColorTrigger(1000, 1, Color.white));

        triggers.Add(new SongTrigger(63, 0, 1, true, 0, 0, 0, 0));

        return triggers.ToArray();
    }
    private Coroutine yCoroutine;
    public override YGDObject[] Init()
    {
        YIDsManager.Instance.AddVariable(gameObject.name + ".variable", YIDsManager.Instance.GetFreeIdFloat(), true);


        List<YTrigger> triggers = new List<YTrigger>();

        GetComponent<YTransform>().Init();

        triggers.Add(new YWaitForSeconds(3));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(0f, 0, 0));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(1f, 1, 1));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(2f, 2, 2));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(3f, 3, 3));

        yCoroutine = YCoroutines.GetCoroutine(new Vector2(300, 300), triggers.ToArray());
        return new YGDObject[]{ yCoroutine };
    }

    public override YTrigger[] Tick()
    {
        List<YTrigger> triggers = new List<YTrigger>();
        triggers.AddRange(YMainCamera.Instance.GetSin(9999,9998,9997));
        triggers.AddRange(YMainCamera.Instance.GetCos(9996,9995,9994));
        triggers.Add(new ItemEdit(9998, true, ItemEdit.Operation.Divide, 30));
        triggers.Add(new ItemEdit(9995, true, ItemEdit.Operation.Divide, -30));
        triggers.Add(new ItemEdit(9999, true, ItemEdit.Operation.Divide, 30));
        triggers.Add(new ItemEdit(9998, true, ItemEdit.Operation.Multiply, -1, 9996, true, 0, true, ItemEdit.Operation.Add));
        triggers.Add(new ItemEdit(9995, true, ItemEdit.Operation.Multiply, -1, 9996, true, 0, true, ItemEdit.Operation.Add));
        triggers.Add(new ItemEdit(9993, true, ItemEdit.Operation.Equals, -1, 9995, true, 0, true, ItemEdit.Operation.Add));
        triggers.Add(YInput.GetP1Left(YMainCamera.Instance.Rotate(0,3f,0), new YTrigger[0]));
        triggers.Add(YInput.GetP1Right(YMainCamera.Instance.Rotate(0,-3f,0), new YTrigger[0]));
        triggers.Add(YInput.GetP2Left(YMainCamera.Instance.Rotate(3f, 0, 0), new YTrigger[0]));
        triggers.Add(YInput.GetP2Right(YMainCamera.Instance.Rotate(-3f, 0, 0), new YTrigger[0]));
        triggers.Add(YInput.GetP1Up(YMainCamera.Instance.Translate(9998, 9999, 9995), new YTrigger[0]));
        triggers.Add(YInput.GetP2Up(YMainCamera.Instance.Translate(9993, 23, 9998), new YTrigger[0]));
        return triggers.ToArray();
    }
}
```

#### To export game to GD level you need to click "Create Level" button in the YGameManager


## 🧩 Core Concepts & Features

### 🧱 GameObjects
- Every item in the game world is a GameObject.
- Objects are organized into groups or located in global world.
- Groups can be loaded/unloaded.

### 🧬 Component System
- Each GameObject holds a set of modular components.
- Components define behavior and data.
```cs
public class TestComponent : YMonoBehaviour
{

    public override YTrigger[] Begin()
    {
        return null;
    }

    public override YGDObject[] Init()
    {
        return null;
    }

    public override YTrigger[] Tick()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        triggers.AddRange(GetComponent<YTransform>().SetState(1));
        triggers.AddRange(GetComponent<YTransform>().Rotate(1f,1f,0));

        return triggers.ToArray();
    }
}

```

### Main Triggers For Code Logic
- ItemEdit
    ```cs
    new ItemEdit(9999, true, ItemEdit.Operation.Equals, 10);
    ```
- ItemCompare
    ```cs
    new ItemCompare(9999, 0, true, true, 1, 1, ItemCompare.Operation.Equals, triggersTrue, triggersFalse);
    ```

### 🧭 Built-in Components
- YTransform: Position, Rotation, Scale
    ```cs
    GetComponent<YTransform>().Translate(1f,5f,3f);
    ```
- YMeshRenderer: Renders 3D models
- YMainCamera: Main camera data
    ```cs
    YMainCamera.Instance.Translate(1f,5f,3f);
    ```

### 🔁 Coroutines

```cs
    public override YTrigger[] Begin()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        triggers.Add(YCoroutines.StartCoroutine(yCoroutine));

        return triggers.ToArray();
    }

    private Coroutine yCoroutine;
    public override YGDObject[] Init()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        GetComponent<YTransform>().Init();

        triggers.Add(new YWaitForSeconds(3));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(0f, 0, 0));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(1f, 1, 1));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(2f, 2, 2));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(3f, 3, 3));

        yCoroutine = YCoroutines.GetCoroutine(new Vector2(300, 300), triggers.ToArray());

        return new YGDObject[]
        {
            yCoroutine
        };
    }
```

### ✎ Level of Detail (LOD)
Reduce mesh detail based on camera distance.
![](imgs/2.png)

### 📦 Group Loading / Unloading
To add group to an object you need to add YGameobjectGroup component to it
![](imgs/4.png)

- Load and unload groups of objects to exclude filling in all IDs and improve perfomance

```cs
YGameobjectGroupsManager.Instance.SetCurrentGroup("2");
```


### 🔊 Audio System
Audio support with volume and looping.

```cs
new SongTrigger(63, 0, 1, true, 0, 0, 0, 0);
```
And you can add songs in the YProjectSettings in Assets/YObject/Resources/
![](imgs/5.png)

### 🎨 Colors
Built-in support for changing colors in color channels.

```cs
new ColorTrigger(3, 1, Color.white)
```

And you can change color channels in the YProjectSettings in Assets/YObject/Resources/
![](imgs/5.png)

### 🎮 Input System

```cs
YInput.GetP1Left(YMainCamera.Instance.Rotate(0,3f,0), new YTrigger[0])
```

### ⏱️ Delta Time & Time Control
- Use variable Time.time to get Game time
- Use variable Time.deltaTime to get Delta time

```cs
GetComponent<YTransform>().Rotate(23, YGameManager.Instance.IDsManager.GetIdByName("Time.time"), 23);
```
