﻿using GTA5Menu.Data;

using GTA5Core.GTA;
using GTA5Core.GTA.Weapons;
using GTA5Core.Features;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.OnlineWeapon;

/// <summary>
/// SpawnWeaponView.xaml 的交互逻辑
/// </summary>
public partial class SpawnWeaponView : UserControl
{
    public SpawnWeaponView()
    {
        InitializeComponent();
        GTA5MenuWindow.WindowClosingEvent += ExternalMenuWindow_WindowClosingEvent;

        // 武器列表
        foreach (var wClass in WeaponHash.WeaponClasses)
        {
            ListBox_WeaponClasses.Items.Add(new IconMenu()
            {
                Icon = wClass.Icon,
                Title = wClass.Name
            });
        }
        ListBox_WeaponClasses.SelectedIndex = 0;
    }

    private void ExternalMenuWindow_WindowClosingEvent()
    {

    }

    /////////////////////////////////////////////////////

    private void ListBox_WeaponClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        lock (this)
        {
            var index = ListBox_WeaponClasses.SelectedIndex;
            if (index == -1)
                return;

            ListBox_WeaponInfos.Items.Clear();

            foreach (var item in WeaponHash.WeaponClasses[index].WeaponInfos)
            {
                if (index != ListBox_WeaponClasses.SelectedIndex)
                    return;

                this.Dispatcher.BeginInvoke(DispatcherPriority.Background, () =>
                {
                    ListBox_WeaponInfos.Items.Add(new ModelInfo()
                    {
                        Name = item.Name,
                        Value = item.Value,
                        Image = RAGEHelper.GetWeaponImage(item.Value)
                    });
                });
            }
        }
    }

    private async void SpawnWeapon()
    {
        AudioHelper.PlayClickSound();

        if (ListBox_WeaponInfos.SelectedItem is ModelInfo info)
        {
            await Hacks.CreateAmbientPickup($"pickup_{info.Value}");
        }
    }

    private void ListBox_WeaponInfos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        SpawnWeapon();
    }

    private void Button_SpawnWeapon_Click(object sender, RoutedEventArgs e)
    {
        SpawnWeapon();
    }
}
