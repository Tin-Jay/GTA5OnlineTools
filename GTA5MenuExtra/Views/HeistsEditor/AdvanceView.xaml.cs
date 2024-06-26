﻿using GTA5Core.Features;

namespace GTA5MenuExtra.Views.HeistsEditor;

/// <summary>
/// AdvanceView.xaml 的交互逻辑
/// </summary>
public partial class AdvanceView : UserControl
{
    public AdvanceView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 即时完成 fm_mission_controller
    /// 公寓抢劫 | 末日豪劫 | 名钻赌场豪劫
    /// </summary>
    public static void InstantFmMissionController()
    {
        if (Locals.LocalAddress("fm_mission_controller") == 0)
            return;

        if (Locals.ReadLocalAddress<int>("fm_mission_controller", 0x6510 / 8) == 0)
            return;

        Locals.WriteLocalAddress("fm_mission_controller", 0x3DDC0 / 8, 264666);
        Locals.WriteLocalAddress("fm_mission_controller", 0x26880 / 8, 12);
    }

    /// <summary>
    /// 即时完成 fm_mission_controller_2020
    /// 改装铺合约 | 佩里科岛 | 德瑞
    /// </summary>
    public static void InstantFmMissionController2020()
    {
        if (Locals.LocalAddress("fm_mission_controller_2020") == 0)
            return;

        if (Locals.ReadLocalAddress<int>("fm_mission_controller_2020", 0x24FF8 / 8) == 0)
            return;

        Locals.WriteLocalAddress("fm_mission_controller_2020", 0x3DDC0 / 8, 264666);
        Locals.WriteLocalAddress("fm_mission_controller_2020", 0x62338 / 8, 9);
    }

    /// <summary>
    /// 单人启动任务（这应该允许你能完整的玩末日将至）
    /// https://www.unknowncheats.me/forum/4007046-post4761.html
    /// </summary>
    public static void AloneLaunchHeist()
    {
        if (Locals.LocalAddress("fmmc_launcher") == 0)
            return;

        if (Locals.ReadLocalAddress<int>("fmmc_launcher", 19331 + 34) == 0)
            return;

        if (Locals.ReadLocalAddress<int>("fmmc_launcher", 19331 + 15) > 1)
        {
            Locals.WriteLocalAddress("fmmc_launcher", 19331 + 15, 1);
            Globals.Set_Global_Value(794744 + 4 + 1 + (Locals.ReadLocalAddress<int>("fmmc_launcher", 19331 + 34) * 89) + 69, 1);
        }

        Globals.Set_Global_Value(4718592 + 3255 + 1, 1);
        Globals.Set_Global_Value(4718592 + 176675 + 1, 0);
        Globals.Set_Global_Value(4718592 + 3252, 1);
        Globals.Set_Global_Value(4718592 + 3253, 1);
    }

    //////////////////////////////////////////////////////

    private void Button_InstantFmMissionController_Click(object sender, RoutedEventArgs e)
    {
        InstantFmMissionController();
    }

    private void Button_InstantFmMissionController2020_Click(object sender, RoutedEventArgs e)
    {
        InstantFmMissionController2020();
    }

    private void Button_AloneLaunchHeist_Click(object sender, RoutedEventArgs e)
    {
        AloneLaunchHeist();
    }
}
