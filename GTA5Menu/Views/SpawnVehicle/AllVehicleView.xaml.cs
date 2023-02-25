﻿using GTA5Menu.Data;

using GTA5Core.RAGE;
using GTA5Core.RAGE.Vehicles;
using GTA5Core.Feature;
using GTA5Shared.Helper;

namespace GTA5Menu.Views.SpawnVehicle;

/// <summary>
/// AllVehicleView.xaml 的交互逻辑
/// </summary>
public partial class AllVehicleView : UserControl
{
    public AllVehicleView()
    {
        InitializeComponent();
        this.DataContext = this;
        SpawnVehicleWindow.WindowClosingEvent += SpawnVehicleWindow_WindowClosingEvent;

        // 载具分类列表
        foreach (var vClass in VehicleHash.VehicleClasses)
        {
            if (vClass.Name == "我的收藏")
                continue;

            ListBox_VehicleClasses.Items.Add(new IconMenu()
            {
                Icon = vClass.Icon,
                Title = vClass.Name
            });
        }
        ListBox_VehicleClasses.SelectedIndex = 0;
    }

    private void SpawnVehicleWindow_WindowClosingEvent()
    {

    }

    private void ListBox_VehicleTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        lock (this)
        {
            var index = ListBox_VehicleClasses.SelectedIndex;
            if (index == -1)
                return;

            ListBox_VehicleInfo.Items.Clear();

            Task.Run(() =>
            {
                foreach (var item in VehicleHash.VehicleClasses[index].VehicleInfos)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        if (index == ListBox_VehicleClasses.SelectedIndex)
                        {
                            ListBox_VehicleInfo.Items.Add(new ModelInfo()
                            {
                                Class = VehicleHash.VehicleClasses[index].Name,
                                Name = item.Name,
                                Value = item.Value,
                                Image = RAGEHelper.GetVehicleImage(item.Value),
                                Mod = item.Mod
                            });
                        }
                        else
                        {
                            return;
                        }
                    });
                }
            });

            ListBox_VehicleInfo.SelectedIndex = 0;
        }
    }

    private async void MenuItem_SpawnVehicleA_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (ListBox_VehicleInfo.SelectedItem is ModelInfo info)
        {
            await Vehicle2.SpawnVehicle(info.Value, -255.0f, 5, info.Mod);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的载具，操作取消");
        }
    }

    private async void MenuItem_SpawnVehicleB_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (ListBox_VehicleInfo.SelectedItem is ModelInfo info)
        {
            await Vehicle2.SpawnVehicle(info.Value, 0.0f, 5, info.Mod);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的载具，操作取消");
        }
    }

    private void MenuItem_AddMyFavorite_Click(object sender, RoutedEventArgs e)
    {
        AudioHelper.PlayClickSound();

        if (ListBox_VehicleInfo.SelectedItem is ModelInfo info)
        {
            MyVehicleView.ActionAddMyFavorite(info);
        }
        else
        {
            NotifierHelper.Show(NotifierType.Warning, "请选择正确的载具，操作取消");
        }
    }
}