using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item",menuName ="Items/Default")]
public class DefaultItem : Item {

    public override void Awake() {
        base.Awake();

        //type = ItemType.Default;
    }

}
