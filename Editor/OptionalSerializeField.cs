using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TanitakaTech.OptionalSerializeField
{
    [InitializeOnLoad]
    internal static class OptionalSerializeField
    {
        /// <summary>
        /// null なパラメータの情報を管理するクラス
        /// </summary>
        private sealed class NullData
        {
            /// <summary>
            /// 参照が設定されていないパラメータを所持するコンポーネント
            /// </summary>
            public Component Component { get; }

            /// <summary>
            /// 参照が設定されていないパラメータを所持するゲームオブジェクトの名前
            /// </summary>
            public string GameObjectName { get; }

            /// <summary>
            /// 参照が設定されていないパラメータを所持するゲームオブジェクトのルートからのパス
            /// </summary>
            public string GameObjectRootPath { get; }

            /// <summary>
            /// 参照が設定されていないパラメータを所持するコンポーネントの名前
            /// </summary>
            public string ComponentName { get; }

            /// <summary>
            /// 参照が設定されていないパラメータの名前
            /// </summary>
            public string FieldName { get; }

            public NullData
            (
                Component component,
                string    gameObjectName,
                string    gameObjectRootPath,
                string    componentName,
                string    fieldName
            )
            {
                Component      = component;
                GameObjectName = gameObjectName;
                GameObjectRootPath       = gameObjectRootPath;
                ComponentName  = componentName;
                FieldName      = fieldName;
            }
        }
        
        static OptionalSerializeField()
        {
            EditorApplication.playModeStateChanged += OnChange;
        }
        
        /// <summary>
		/// Unity のプレイモードの状態が変化した時に呼び出されます
		/// </summary>
		private static void OnChange( PlayModeStateChange state )
		{
			if ( state != PlayModeStateChange.ExitingEditMode ) return;
            
			if ( !OptionalSerializeFieldSettings.IsEnable ) return;

			var list = Validate().ToArray();

			if ( list.Length <= 0 ) return;

			var logFormat = OptionalSerializeFieldSettings.LogFormat;

			foreach ( var n in list )
			{
				var message = logFormat;
				message = message.Replace( "[GameObjectName]", n.GameObjectName );
				message = message.Replace( "[GameObjectRootPath]", n.GameObjectRootPath );
				message = message.Replace( "[ComponentName]", n.ComponentName );
				message = message.Replace( "[FieldName]", n.FieldName );

				Debug.LogError( message, n.Component );
			}

			EditorApplication.isPlaying = false;
		}

		/// <summary>
		/// 参照が設定されていない NotNull なパラメータの一覧を返します
		/// </summary>
		private static IEnumerable<NullData> Validate()
		{
			var gameObjects = Resources
					.FindObjectsOfTypeAll<GameObject>()
					.Where( c => c.scene.isLoaded )
					.Where( c => c.hideFlags == HideFlags.None )
				;

			foreach ( var gameObject in gameObjects )
			{
				var components = gameObject.GetComponents<Component>();

				foreach ( var component in components )
				{
					if ( component == null ) continue;

					var type   = component.GetType();
					var fields = type.GetFields( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );

                    if (type.Namespace != null)
                    {
                        if (OptionalSerializeFieldSettings.IgnoreNamespaces.Any(n => (type.Namespace.StartsWith(n))))
                        {
                            continue;
                        }
                    }
                    var assemblyName = type.Assembly.GetName().Name;
                    if (OptionalSerializeFieldSettings.IgnoreAssemblyNames.Any(n => (assemblyName == n)))
                    {
                        continue;
                    }
                    
					foreach ( var field in fields )
					{
                        var hasSerializeFieldAttribute = field.GetCustomAttributes(typeof(SerializeField), true).Any();
                        var hasOptionalAttribute = field.GetCustomAttributes(typeof(OptionalAttribute), true).Any();

                        if (!hasSerializeFieldAttribute || hasOptionalAttribute)
                        {
                            continue;
                        }

						var value = field.GetValue( component );

						if ( value != null && value.ToString() != "null" ) continue;

						var data = new NullData
						(
							component: component,
							gameObjectName: component.gameObject.name,
							gameObjectRootPath: component.gameObject.GetRootPath(),
							componentName: component.GetType().Name,
							fieldName: field.Name
						);

						yield return data;
					}
				}
			}
		}

		/// <summary>
		/// ゲームオブジェクトのルートからのパスを返します
		/// </summary>
		private static string GetRootPath( this GameObject gameObject )
		{
			var path   = gameObject.name;
			var parent = gameObject.transform.parent;

			while ( parent != null )
			{
				path   = parent.name + "/" + path;
				parent = parent.parent;
			}

			return path;
		}
    }
}

