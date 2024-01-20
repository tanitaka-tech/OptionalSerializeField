[![openupm](https://img.shields.io/npm/v/com.tanitaka.optional-serialize-field?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.tanitaka.state-variable/)
![license](https://img.shields.io/github/license/tanitaka-tech/OptionalSerializeField)

Unityで`SerializeField`を指定した変数がシーン実行時にnullの場合、エラーを出力します。

## Features 🚀
- `SerializeField`に`Optional`アトリビュートを付けることでエラーを出力しなくなります。
- ProjectSettingsからnamespace、アセンブリ名を指定することで、エラーから除外することができます。

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