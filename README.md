# ![](imgs/YObjectLogomini.png)  YObject
Unity to GD tool

## 🚀 Philosophy

The core principles behind YObject:

- Almost the same approach to game creation as in Unity
- Group objects "see" global objects but global objects doesn't "see" group objects
- Gameobjects can't be parents of other gameobjects


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
public class MyComponent : YMonoBehaviour
{
    public override void Begin()
    {
        YCoroutines.StartCoroutine(yCoroutine);

        new ColorTrigger(1000, 1, Color.white);

        new SongTrigger(63, 0, 1, true, 0, 0, 0, 0);
    }
    private Coroutine yCoroutine;
    public override YGDObject[] Init()
    {
        YIDsManager.Instance.AddVariable(gameObject.GetInstanceID() + ".variable", YIDsManager.Instance.GetFreeIdFloat(), true);


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

        yCoroutine = YCoroutines.GetCoroutine(triggers.ToArray());
        return new YGDObject[]{ yCoroutine };
    }

    public override void Tick()
    {
        // free fly camera
        
        YMainCamera.Instance.GetSin(9999,9998,9997);
        YMainCamera.Instance.GetCos(9996,9995,9994);
        new ItemEdit(9998, true, ItemEdit.Operation.Divide, 30);
        new ItemEdit(9995, true, ItemEdit.Operation.Divide, -30);
        new ItemEdit(9999, true, ItemEdit.Operation.Divide, 30);
        new ItemEdit(9998, true, ItemEdit.Operation.Multiply, -1, 9996, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(9995, true, ItemEdit.Operation.Multiply, -1, 9996, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(9993, true, ItemEdit.Operation.Equals, -1, 9995, true, 0, true, ItemEdit.Operation.Add);
        YInput.GetP1Left(YMainCamera.Instance.Rotate(0,3f,0), new YTrigger[0]);
        YInput.GetP1Right(YMainCamera.Instance.Rotate(0,-3f,0), new YTrigger[0]);
        YInput.GetP2Left(YMainCamera.Instance.Rotate(3f, 0, 0), new YTrigger[0]);
        YInput.GetP2Right(YMainCamera.Instance.Rotate(-3f, 0, 0), new YTrigger[0]);
        YInput.GetP1Up(YMainCamera.Instance.Translate(9998, 9999, 9995), new YTrigger[0]);
        YInput.GetP2Up(YMainCamera.Instance.Translate(9993, 23, 9998), new YTrigger[0]);
    }
}
```

#### To export game to GD level you need to click "Create Level" button in the YGameManager
!!! geometry dash must be closed and after export you can open it !!!

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

    public override void Begin()
    {

    }

    public override YGDObject[] Init()
    {
        return null;
    }

    public override void Tick()
    {
        GetComponent<YTransform>().SetState(1);
        GetComponent<YTransform>().Rotate(1f,1f,0);
    }
}
```

### 🎯 Main Operations

<!-- - Add variable
    ```cs
    YIDsManager.Instance.AddVariable(gameObject.name + ".component.vector.x", YIDsManager.Instance.GetFreeIdFloat(), true);
    YIDsManager.Instance.AddVariable(gameObject.name + ".component.vector.y", YIDsManager.Instance.GetFreeIdFloat(), true);
    YIDsManager.Instance.AddVariable(gameObject.name + ".component.vector.z", YIDsManager.Instance.GetFreeIdFloat(), true);
    ```
    -->
- Get free ids, groups or gradients
    ```cs
    YIDsManager.Instance.GetFreeIdFloat();

    YIDsManager.Instance.GetFreeIdInt();

    YIDsManager.Instance.GetFreeGroup();
    
    YIDsManager.Instance.GetFreeGradient();
    ```
- Add (Take place in memory) ids, groups or gradients
    ```cs
    YIDsManager.Instance.AddVariable("varName", id, true);

    YIDsManager.Instance.AddVariable("varName", id, false);

    YIDsManager.Instance.AddGroup(group);

    YIDsManager.Instance.AddGradient(gradient);
    ```

### 🧩 Main Triggers For Code Logic
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
    public override void Begin()
    {
        YCoroutines.StartCoroutine(yCoroutine);
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

        yCoroutine = YCoroutines.GetCoroutine(triggers.ToArray());

        return new YGDObject[]
        {
            yCoroutine
        };
    }
```

### ✎ Level of Detail (LOD)
Reduce mesh detail based on camera distance.

![](imgs/2.png)

### ✎ Triangle layer

You can set layer (layer means triangles with one layer will be activated on one layer) and layer parent (layer parent means this triangle will be main triangle for layer calculations) of triangle

![](imgs/6.png)

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

You can set color channels and corrector colors in triangle object (child of gameobject)

![](imgs/6.png)

You can change color in color channel
```cs
new ColorTrigger(3, 1, Color.white)
```

And you can change color channels in the YProjectSettings in Assets/YObject/Resources/

![](imgs/5.png)

### 🎮 Input System

```cs
YInput.GetP1Left(YMainCamera.Instance.Rotate(0,3f,0), new YTrigger[0])
```

### 🎲 Random
```cs
new RandomTrigger(50, GetComponent<YTransform>().Translate(0.05f, 0, 0), GetComponent<YTransform>().Translate(-0.05f, 0, 0))
```

### ⏱️ Delta Time & Time Control
- Use variable Time.time to get Game time
- Use variable Time.deltaTime to get Delta time

```cs
GetComponent<YTransform>().Rotate(23, YIDsManager.Instance.GetIdByName("Time.time"), 23);
// 23 - zero variable
```

## How to use
- download unity (version >= 2021.3.38f1)
- add this files as project (Add project button)
- open project
- create game of your dreams

## Community

- discord server: https://discord.gg/FzWNBcQA57
- telegram channel: https://t.me/yaylab
- youtube channel: https://www.youtube.com/@YaYmsc
(here you can find tutorials)


## Special thanks
- made with help from Nemo (https://www.youtube.com/@Nemo_2510) 