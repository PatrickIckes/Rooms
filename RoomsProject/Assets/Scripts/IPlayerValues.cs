using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class CustomVector3
{
    internal float x;
    internal float y;
    internal float z;
    public CustomVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

[Serializable]
public class PlayerAttributes
{
  
    internal List<GameObject> Collectables;
    internal int health;
    internal int CurrentScene;
    //Used to track when items are picked up
    internal List<IInventoryItem> inventory; //Can't save this atm b/c serialization issues
    /// <summary>
    /// Note that Unity vector 3 is not serializable therefore cannot be saved accurately use custom vector3 to save
    /// </summary>
    internal CustomVector3 playerpostion;
    /// <summary>
    /// Set CustomVector3 by transfering unity vector3 Values into constructor
    /// </summary>
    /// <param name="pos"></param>
    public PlayerAttributes(int health,int CurrentScene, List<GameObject> Collectables/*, Inventory inventory*/, Vector3 position)
    {
        this.health = health;
        this.CurrentScene = CurrentScene;
        //this.inventory = inventory;
        SetPlayerPosition(position);
    }
    public void SetPlayerPosition(Vector3 pos)
    {
        playerpostion = new CustomVector3(pos.x, pos.y, pos.z);
    }

    /// <summary>
    /// get the custom vector3 and turn it into a unity vector3
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPlayerPosition()
    {
        return new Vector3(playerpostion.x, playerpostion.y, playerpostion.z);
    }
}
