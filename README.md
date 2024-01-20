[![openupm](https://img.shields.io/npm/v/com.tanitaka.optional-serialize-field?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.tanitaka.state-variable/)
![license](https://img.shields.io/github/license/tanitaka-tech/OptionalSerializeField)

Unityで`SerializeField`を指定したフィールドがシーン実行時にnullの場合、エラーを出力します。

## Features 🚀
- `SerializeField`に`Optional`アトリビュートを付けることでエラーを出力しなくなります。
```
[SerializeField, Optional] SomeBehaviour _someBehaviour;
```
- ProjectSettingsからエラーから除外するnamespace、アセンブリ名を指定できます。
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
