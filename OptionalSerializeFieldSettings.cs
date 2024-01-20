using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TanitakaTech.OptionalSerializeField
{
    [Serializable]
    internal sealed class OptionalSerializeFieldSettings
    {
        private const string KEY = "OptionalSerializeField";

        [SerializeField] private bool   m_isEnable  = false;
        [SerializeField] private string m_logFormat = "参照が設定されていません：[GameObjectRootPath], [ComponentName], [FieldName]";
        [SerializeField] private List<string> m_ignoreNamespaces = new(){ "UnityEngine", "UnityEditor"};
        [SerializeField] private List<string> m_ignoreAssemblyNames = new();

        public bool IsEnable
        {
            get => m_isEnable;
            set => m_isEnable = value;
        }

        public string LogFormat
        {
            get => m_logFormat;
            set => m_logFormat = value;
        }
        
        public List<string> IgnoreNamespaces 
        { 
            get => m_ignoreNamespaces; 
            set => m_ignoreNamespaces = value;
        }
        
        public List<string> IgnoreAssemblyNames 
        { 
            get => m_ignoreAssemblyNames; 
            set => m_ignoreAssemblyNames = value;
        }

        public static OptionalSerializeFieldSettings LoadFromEditorPrefs()
        {
            var json = EditorPrefs.GetString( KEY );
            var settings = JsonUtility.FromJson<OptionalSerializeFieldSettings>( json ) ??
                           new OptionalSerializeFieldSettings();

            return settings;
        }

        public static void SaveToEditorPrefs( OptionalSerializeFieldSettings setting )
        {
            var json = JsonUtility.ToJson( setting );

            EditorPrefs.SetString( KEY, json );
        }
    }
}