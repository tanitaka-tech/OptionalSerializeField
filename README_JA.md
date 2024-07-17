[![openupm](https://img.shields.io/npm/v/com.tanitaka.optional-serialize-field?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.tanitaka.state-variable/)
![license](https://img.shields.io/github/license/tanitaka-tech/OptionalSerializeField)

**Docs** ([English](README.md), [日本語](README_JA.md))

Unityで`SerializeField`がシーン実行時に`None`の場合、エラーを出力するようになります。

これによって、意図せず`SerializeField`が`None`になることを防ぎます。

## Noneを許容する方法
- `Optional`アトリビュートを付けると、その`SerializeField`はエラー対象から除外されます。
```
[SerializeField, Optional] SomeBehaviour _someBehaviour;
```
- ProjectSettingsからnamespace、アセンブリ名を指定すると、指定に含まれるMonoBehaviourはエラー対象から除外されます。
  <img width="773" alt="Screenshot 2024-01-20 at 14 47 43" src="https://github.com/tanitaka-tech/OptionalSerializeField/assets/78785830/ff46afde-8585-42ca-9da7-293bf2353cba">


## Installation ☘️

### Install via git URL
1. Open the Package Manager
1. Press [＋▼] button and click Add package from git URL...
1. Enter the following:
    - https://github.com/tanitaka-tech/OptionalSerializeField.git

### ~~Install via OpenUPM~~ (not yet)
```sh
openupm add com.tanitaka-optional-serialize-field
```

## special thanks 🙏
このライブラリは下記のライブラリを参考にして作られました。
- https://github.com/baba-s/UniNotNullChecker
