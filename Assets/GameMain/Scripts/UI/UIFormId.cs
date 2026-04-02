//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace StarForce
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId : short
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,

        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// 设置。
        /// </summary>
        SettingForm = 101,

        /// <summary>
        /// 关于。
        /// </summary>
        AboutForm = 102,

        MainForm = 1000, // 主界面
        
        RoleForm=1006,//角色界面
        ShowLevelForm=1002, // 第几关
        SelectSceneBuffForm=1001, // 选buff
        WaitToReliveForm=1004, // 复活指引弹窗
        BattleEndForm=1003, // 战斗结束弹窗
        PauseForm=1005,//暂停界面
    }
}
