[![openupm](https://img.shields.io/npm/v/com.tanitaka.optional-serialize-field?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.tanitaka.state-variable/)
![license](https://img.shields.io/github/license/tanitaka-tech/OptionalSerializeField)

**Docs** ([English](README.md), [日本語](README_JA.md))

In Unity, when `SerializeField` is `None` during scene execution, it will now output an error.

This prevents `SerializeField` from unintentionally becoming `None`.

## How to allow None
- Adding the `Optional` attribute will exclude that `SerializeField` from error checking.
```
[SerializeField, Optional] SomeBehaviour _someBehaviour;
```
- By specifying namespaces and assembly names in ProjectSettings, MonoBehaviours included in the specification will be excluded from error checking.
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
This library was created with reference to the following library:
- https://github.com/baba-s/UniNotNullChecker
