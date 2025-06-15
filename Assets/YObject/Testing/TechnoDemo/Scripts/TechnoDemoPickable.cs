using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnoDemoPickable : YMonoBehaviour
{
    public YTransform playerHand;
    public Vector3 rotation;
    public override void Tick()
    {
        YVariable posX = new YFloat();
        YVariable posY = new YFloat();
        YVariable posZ = new YFloat();

        YVariable playerPosX = new YFloat();
        YVariable playerPosY = new YFloat();
        YVariable playerPosZ = new YFloat();

        GetComponent<YTransform>().GetPosition(posX, posY, posZ);
        playerHand.GetPosition(playerPosX, playerPosY, playerPosZ);

        posX -= playerPosX;
        posY -= playerPosY;
        posZ -= playerPosZ;

        YVariable distanceSquared = posX * posX + posY * posY + posZ * posZ;

        new ItemCompare(distanceSquared, new YFloat(3), ItemCompare.Operation.Less, MoveToPlayerHand(), new YTrigger[0]);
    }

    private YTrigger[] MoveToPlayerHand()
    {
        YGameManager.Instance.RecordPool();

        float t = 0.2f;
        YVariable posX = new YFloat();
        YVariable posY = new YFloat();
        YVariable posZ = new YFloat();

        YVariable playerPosX = new YFloat();
        YVariable playerPosY = new YFloat();
        YVariable playerPosZ = new YFloat();

        GetComponent<YTransform>().GetPosition(posX, posY, posZ);
        playerHand.GetPosition(playerPosX, playerPosY, playerPosZ);

        Lerp(posX, playerPosX, t);
        Lerp(posY, playerPosY, t);
        Lerp(posZ, playerPosZ, t);

        GetComponent<YTransform>().SetPosition(posX, posY, posZ);


        YVariable rotX = new YFloat();
        YVariable rotY = new YFloat();
        YVariable rotZ = new YFloat();

        playerHand.GetRotation(9999, 9998, 9997);
        GetComponent<YTransform>().GetRotation(rotX, rotY, rotZ);
        YVariable x = new YVariable(9999, true) + rotation.x;
        YVariable y = new YVariable(9998, true) + rotation.y;
        YVariable z = new YVariable(9997, true) + rotation.z;

        Lerp(rotX, x, t);
        Lerp(rotY, y, t);
        Lerp(rotZ, z, t);

        GetComponent<YTransform>().SetRotation(rotX, rotY, rotZ);


        return YGameManager.Instance.StopRecordPool();
    }

    private void Lerp(YVariable var1, YVariable var2, float t)
    {
        YVariable dist = new YFloat(0) + var2 - var1; 
        var1 = var1 + t * dist;
    }
}
