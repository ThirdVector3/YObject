using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing2WallsControler : YMonoBehaviour
{
    // Привязать в инспекторе YTransform игрока
    public YTransform player;

    private YVariable playerX;
    private YVariable playerY;
    private YVariable playerZ;

    private YVariable _inputMode;

    public override void Init()
    {
        playerX = new YFloat();
        playerY = new YFloat();
        playerZ = new YFloat();

        _inputMode = new YFloat(1);
    }

    public override void Tick()
    {

        new YVector3(playerX, playerY, playerZ).SetValue(player.GetPosition());

        Move();

    }

    private void Move()
    {
        CamMove();
        YVariable move = new YFloat(2.5f) * new YVariable("Time.deltaTime");
        new Condition(new YVariable("Input.P1Up"), new YFloat(1), ItemCompare.Operation.Equals)
            .Then(() =>
            {
                player.TranslateLocal(0, 0, 0.1f);
        
            });
        


        //Смена режима управления
        new Condition(YInputService.Get().P2RightDown())
            .Then(() =>
            {
                ToggleInputMode();
            });
    }

    private void CamMove()
    {
        YMainCamera.Instance.yTransform.SetPosition(playerX, playerY, playerZ);

    }

    private void ToggleInputMode()
    {
        new Condition(_inputMode, new YFloat(1), ItemCompare.Operation.Equals)
        .Then(() =>
        {
            new DebugLog("1");
        })
        .Else(() =>
        {
            new DebugLog("2");
        });

    }
}