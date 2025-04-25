# YObject
Unity to GD tool

## 🚀 Philosophy

The core principles behind YObject:

- **Almost the same approach to game creation as in Unity
- **Group objects "see" global objects but global objects doesn't "see" group objects


## 🛠️ Quick Start

...

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

...

### 📦 Group Loading / Unloading
- Load and unload groups of objects to exclude filling in all IDs and improve perfomance

```cs
YGameManager.Instance.GameobjectGroupsManager.SetCurrentGroup("2");
```

### 🔊 Audio System
audio support with volume and looping.

```cs
new SongTrigger(63, 0, 1, true, 0, 0, 0, 0);
```

### 🎨 Colors
Built-in support for changing colors in color channels.

```cs
new ColorTrigger(3, 1, Color.white)
```

### 🎮 Input System

```cs
YInput.GetP1Left(YMainCamera.Instance.Rotate(0,3f,0), new YTrigger[0])
```

### ⏱️ Delta Time & Time Control
