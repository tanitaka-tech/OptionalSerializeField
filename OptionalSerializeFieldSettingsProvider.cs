using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TanitakaTech.OptionalSerializeField
{
    /// <summary>
    /// ProjectSettings における設定画面を管理するクラス
    /// </summary>
    internal sealed class OptionalSerializeFieldSettingsProvider : SettingsProvider
    {
        private readonly ReorderableList _ignoreNamespacesList;
        private readonly ReorderableList _ignoreAssemblyNameList;
        private bool _needSave = false;
        private OptionalSerializeFieldSettings _settings;
        
        public OptionalSerializeFieldSettingsProvider( string path, SettingsScope scope )
            : base( path, scope )
        {
            _settings = OptionalSerializeFieldSettings.LoadFromEditorPrefs();
            _ignoreNamespacesList = new ReorderableList(_settings.IgnoreNamespaces, typeof(string), true, true, true, true)
                {
                    drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                    {
                        _settings.IgnoreNamespaces[index] = EditorGUI.TextField(rect, _settings.IgnoreNamespaces[index]);
                    },
                    onAddCallback = (list) =>
                    {
                        _settings.IgnoreNamespaces.Add("");
                        _needSave = true;
                    },
                    onRemoveCallback = (list) =>
                    {
                        _settings.IgnoreNamespaces.RemoveAt(list.index);
                        _needSave = true;
                    }
                };
            _ignoreAssemblyNameList = new ReorderableList(_settings.IgnoreAssemblyNames, typeof(string), true, true, true, true)
                {
                    drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                    {
                        _settings.IgnoreAssemblyNames[index] = EditorGUI.TextField(rect, _settings.IgnoreAssemblyNames[index]);
                    },
                    onAddCallback = (list) =>
                    {
                        _settings.IgnoreAssemblyNames.Add("");
                        _needSave = true;
                    },
                    onRemoveCallback = (list) =>
                    {
                        _settings.IgnoreAssemblyNames.RemoveAt(list.index);
                        _needSave = true;
                    }
                };
        }
        
        public override void OnGUI( string searchContext )
        {
            using var checkScope = new EditorGUI.ChangeCheckScope();
            _settings.IsEnable  = EditorGUILayout.Toggle( "Enabled", _settings.IsEnable );
            _settings.LogFormat = EditorGUILayout.TextField( "Log Format", _settings.LogFormat );

            EditorGUILayout.HelpBox( "Log Format で使用できるタグ", MessageType.Info );

            EditorGUILayout.TextArea
            (
                @"[GameObjectName]
[GameObjectRootPath]
[ComponentName]
[FieldName]"
            );
            
            EditorGUILayout.LabelField("Ignore Namespaces");
            _ignoreNamespacesList.DoLayoutList();
            
            EditorGUILayout.LabelField("Ignore Assembly Names");
            _ignoreAssemblyNameList.DoLayoutList();

            if ( GUILayout.Button( "Use Default" ) )
            {
                _settings = new OptionalSerializeFieldSettings();
                OptionalSerializeFieldSettings.SaveToEditorPrefs( _settings );
            }
            
            if ( checkScope.changed || _needSave)
            {
                OptionalSerializeFieldSettings.SaveToEditorPrefs( _settings );
                _needSave = false;
            }
        }

        //================================================================================
        // 関数(static)
        //================================================================================
        /// <summary>
        /// Preferences にメニューを追加します
        /// </summary>
        [SettingsProvider]
        private static SettingsProvider Create()
        {
            var path     = "TanitakaTech/OptionalSerializeField";
            var provider = new OptionalSerializeFieldSettingsProvider( path, SettingsScope.Project );

            return provider;
        }
    }
}