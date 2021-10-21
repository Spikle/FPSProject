using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
    public static class EventManager
    {
        public static Func<Inputs> OnUpdateInputs;
        public static Action<int> OnUpdateWeaponAmmo;

        public static Action<ValueStats> OnSetHealthPlayer;

        public static Action<List<ItemInventory>> OnUpdateInventory;

        public static Action OnEndGame;
    }
}
