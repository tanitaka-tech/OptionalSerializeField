using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEditorInternal;
using UnityEngine;

namespace TanitakaTech.OptionalSerializeField
{
    /// <summary>
    /// ProjectSettings の設定画面を管理するクラス
    /// </summary>
    [PublicAPI]
    public static class OptionalSerializeFieldSettingsManager
    {
        private static ReorderableList _ignoreNamespacesList;
        private static ReorderableList _ignoreAssemblyNameList;
        
        public class ProjectSetting<T> : UserSetting<T>
        {
            internal ProjectSetting(string key, T value) : base(Settings, key, value, SettingsScope.Project)
            {
            }
        }

        private const string MenuPrefixBase = "TanitakaTech/Optional Serialize Field";
        private const string SettingsMenuPath = "Project/" + MenuPrefixBase;
        internal const string SettingsMenuPathForDisplay = "Project Settings/" + MenuPrefixBase;
        private static readonly Assembly ContainingAssembly = typeof(OptionalSerializeFieldSettingsManager).Assembly;
        private static readonly Settings Settings = new Settings("com.tanitaka-tech.optional-serialize-field");
        
        [SettingsProvider]
        private static SettingsProvider CreateUserSettingsProvider() => new UserSettingsProvider(SettingsMenuPath, Settings, new[] { ContainingAssembly }, SettingsScope.Project);
        
        [PublicAPI]
        public static class Log
        {
            private const string CategoryName = "0_Log";

            [field: UserSetting(
                category: CategoryName, 
                title: "IsEnable",
                tooltip: "")]
            public static ProjectSetting<bool> IsEnableSetting { get; }
                = new("IsEnable", true);
            
            [field: UserSetting(
                category: CategoryName,
                title: "LogFormat",
                tooltip: "")]
            public static ProjectSetting<string> LogFormatSetting { get; }
                = new("LogFormat", "参照が設定されていません：[GameObjectRootPath], [ComponentName], [FieldName]");
            
            [UserSettingBlock(CategoryName)]
            private static void Draw(string searchContext)
            {
                EditorGUILayout.HelpBox( "Log Format で使用できるタグ", MessageType.Info );

                EditorGUILayout.TextArea
                (
                    @"[GameObjectName]
[GameObjectRootPath]
[ComponentName]
[FieldName]"
                );
            }
        }
        
        [PublicAPI]
        public static class IgnoreNameSpaces
        {
            private const string CategoryName = "1_IgnoreNamespaces";
            
            [field: UserSetting]
            public static ProjectSetting<List<string>> IgnoreNameSpacesSetting { get; }
                = new("IgnoreNamespaces", new() { "UnityEngine", "UnityEditor" });
            

            [UserSettingBlock(CategoryName)]
            private static void Draw(string searchContext)
            {
                _ignoreNamespacesList = DrawReorderableList(
                    IgnoreNameSpacesSetting.value, 
                    (rect, index) => IgnoreNameSpacesSetting.value[index] = EditorGUI.TextField(rect, IgnoreNameSpacesSetting.value[index]), 
                    () => IgnoreNameSpacesSetting.ApplyModifiedProperties());
            }
        }
        
        [PublicAPI]
        public static class IgnoreAssemblies
        {
            private const string CategoryName = "2_IgnoreAssemblies";
            
            [field: UserSetting]
            public static ProjectSetting<List<string>> IgnoreAssembliesSetting { get; }
                = new("IgnoreAssemblies", new());
            
            
            [UserSettingBlock(CategoryName)]
            private static void Draw(string searchContext)
            {
                _ignoreAssemblyNameList = DrawReorderableList(
                    IgnoreAssembliesSetting.value, 
                    (rect, index) => IgnoreAssembliesSetting.value[index] = EditorGUI.TextField(rect, IgnoreAssembliesSetting.value[index]), 
                    () => IgnoreAssembliesSetting.ApplyModifiedProperties());
            }
        }
        
        private static ReorderableList DrawReorderableList<T>(List<T> displayList, Action<Rect, int> onDrawElementCallback, Action onModifyListCallback)
        {
            ReorderableList ret = null;
            ret = new ReorderableList(displayList, typeof(T), true, false, true, true)
            {
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    onDrawElementCallback?.Invoke(rect, index);
                },
                onAddCallback = (list) =>
                {
                    displayList.Add(default);
                    onModifyListCallback?.Invoke();
                    ret.DoLayoutList();
                },
                onRemoveCallback = (list) =>
                {
                    displayList.RemoveAt(list.index);
                    onModifyListCallback?.Invoke();
                    ret.DoLayoutList();
                },
                onSelectCallback = (list) =>
                {
                    list.Select(list.index);
                }, 
                onReorderCallbackWithDetails	 = (list, oldIndex, newIndex) =>
                {
                    (displayList[newIndex], displayList[oldIndex]) = (displayList[oldIndex], displayList[newIndex]);
                    onModifyListCallback?.Invoke();
                    ret.DoLayoutList();
                },
                    
            };
            ret.DoLayoutList();
            return ret;
        }
    }
}