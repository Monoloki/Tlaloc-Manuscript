using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedStatsPresenter : MonoBehaviour
{
    [SerializeField] private EquipedStatsModel model;
    [SerializeField] private EquipedStatsView view;

    private void Awake() {
        UpdateEquipedItemLogic();
    }

    public void UpdateEquipedItemLogic() {
        model.UpdateEquipedStats();
        view.UpdateStatsOnUI();
    }
}
