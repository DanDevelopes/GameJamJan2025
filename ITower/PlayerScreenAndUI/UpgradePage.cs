using Godot;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
using System.Collections.Generic;

public class UpgradePage : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    UpgradePage()
    {

    }
    RichTextLabel[] upgrades;
    Dictionary<int, StatModifiers.Upgrades> keyValuePairs;
    Button[] addButtons;
    RichTextLabel[] amounts;
    RichTextLabel pointsAvailable;
    Button[] subtractButtons;
    string[] upgradesNames;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        pointsAvailable = GetNode<RichTextLabel>("Panel/Points");
        StatModifiers.AddPoints(5);
        pointsAvailable.Text = $"Points: {StatModifiers.GetTotalPoints()}";
        upgradesNames = Enum.GetNames(typeof(StatModifiers.Upgrades));
        upgrades = new RichTextLabel[upgradesNames.Length];
        addButtons = new Button[upgradesNames.Length];
        subtractButtons = new Button[upgradesNames.Length];
        amounts = new RichTextLabel[upgradesNames.Length];
        for (int i = 0; i < upgradesNames.Length; i++)
        {
            Enum.TryParse(upgradesNames[i], out StatModifiers.Upgrades a);

            var upgradeText = GetNode<RichTextLabel>($"Panel/UpgradeBoxV/HBox{i + 1}/UpgradeName");
            upgradeText.Text = Enum.GetName(typeof(StatModifiers.Upgrades), a);
            upgrades[i] = upgradeText;
            addButtons[i] = GetNode<Button>($"Panel/UpgradeBoxV/HBox{i + 1}/More");
            amounts[i] = GetNode<RichTextLabel>($"Panel/UpgradeBoxV/HBox{i + 1}/Amount");
            subtractButtons[i] = GetNode<Button>($"Panel/UpgradeBoxV/HBox{i + 1}/Less");
            amounts[i].Text = StatModifiers.GetStatModifiers()[a].ToString();
            addButtons[i].Connect("pressed", this, nameof(AddToAmount), new Godot.Collections.Array { upgrades[i].Text, i });
            subtractButtons[i].Connect("pressed", this, nameof(SubtractToAmount), new Godot.Collections.Array { upgrades[i].Text, i });
        }
    }
    

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.

    private void AddToAmount(string upgradeName, int row) 
    {
        Enum.TryParse(upgradeName, out StatModifiers.Upgrades result);
        int.TryParse(amounts[row].Text, out int amount);
        if (amount <= 5)
        {
            StatModifiers.SubtractPoints(1);
            StatModifiers.UpgradeStat(result, amount + 1);
            amounts[row].Text = (amount + 1).ToString();
            pointsAvailable.Text = $"Points: {StatModifiers.GetTotalPoints()}";
        }
    }
    private void SubtractToAmount(string upgradeName, int row)
    {
        Enum.TryParse(upgradeName, out StatModifiers.Upgrades result);
        int.TryParse(amounts[row].Text, out int amount);
        if (amount > 0)
        {
            StatModifiers.AddPoints(1);
            StatModifiers.UpgradeStat(result, amount - 1);
            amounts[row].Text = (amount - 1).ToString();
            pointsAvailable.Text = $"Points: {StatModifiers.GetTotalPoints()}";
        }
    }
}
