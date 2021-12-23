using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Model.Data
{
    public interface ICanAddInInventory
    {
        bool AddInInventory(string id, int value);
    }
}