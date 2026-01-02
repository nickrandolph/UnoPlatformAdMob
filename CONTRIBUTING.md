# Contributing to Uno.AdMob

Thank you for your interest in contributing to Uno.AdMob! This document provides guidelines for contributing to the project.

## How to Contribute

### Reporting Issues

If you find a bug or have a feature request:

1. Check if the issue already exists in the [Issues](https://github.com/nickrandolph/UnoPlatformAdMob/issues) section
2. If not, create a new issue with:
   - A clear and descriptive title
   - Detailed steps to reproduce (for bugs)
   - Expected behavior vs actual behavior
   - Platform information (Android/iOS version, Uno Platform version, etc.)
   - Sample code if applicable

### Pull Requests

1. Fork the repository
2. Create a new branch for your feature or bug fix:
   ```bash
   git checkout -b feature/my-new-feature
   ```
3. Make your changes following the coding guidelines below
4. Test your changes on both Android and iOS if possible
5. Commit your changes with clear, descriptive commit messages
6. Push to your fork and submit a pull request

## Development Setup

### Prerequisites

- .NET 10 SDK or later
- Visual Studio 2022 or JetBrains Rider (optional but recommended)
- Android SDK (for Android development)
- Xcode (for iOS development, macOS only)

### Building the Library

```bash
cd src/Uno.AdMob
dotnet build
```

### Running the Sample App

```bash
cd samples/AdMobSample
dotnet build -f net10.0-browserwasm  # For WebAssembly
dotnet build -f net10.0-android      # For Android
dotnet build -f net10.0-ios          # For iOS (macOS only)
```

## Coding Guidelines

### Code Style

- Follow C# coding conventions
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and concise

### Platform-Specific Code

When adding platform-specific implementations:

1. Create partial class methods in the main class
2. Implement them in platform-specific files under `Platforms/{Platform}/`
3. Use conditional compilation symbols (`#if __ANDROID__`, `#if __IOS__`, etc.)
4. Provide stub implementations for unsupported platforms

Example:
```csharp
// In main class
public partial class MyControl : UserControl
{
    partial void InitializePlatform();
    
    public MyControl()
    {
        InitializePlatform();
    }
}

// In Platforms/Android/MyControl.Android.cs
#if __ANDROID__
public partial class MyControl
{
    partial void InitializePlatform()
    {
        // Android-specific implementation
    }
}
#endif

// In Platforms/Unsupported/MyControl.Unsupported.cs
#if __WASM__ || WINDOWS || __MACOS__
public partial class MyControl
{
    partial void InitializePlatform()
    {
        // No-op or placeholder
    }
}
#endif
```

### Testing

- Test your changes on multiple platforms when possible
- Ensure the sample app still works after your changes
- Add unit tests for new functionality if applicable

## Documentation

- Update the README.md if you add new features
- Add XML documentation comments to all public APIs
- Update the sample app to demonstrate new features

## Code Review Process

1. All submissions require review
2. Maintainers will review your PR and may request changes
3. Once approved, maintainers will merge your PR

## License

By contributing to Uno.AdMob, you agree that your contributions will be licensed under the MIT License.

## Questions?

Feel free to open an issue for any questions about contributing!
