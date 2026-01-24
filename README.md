# Debugalizers

A Visual Studio extension providing custom debug visualizers with formatting, syntax highlighting, and specialized views for common string data types.

[![Build](https://github.com/CodingWithCalvin/VS-Debugalizers/actions/workflows/build.yml/badge.svg)](https://github.com/CodingWithCalvin/VS-Debugalizers/actions/workflows/build.yml)
[![Visual Studio Marketplace](https://img.shields.io/visual-studio-marketplace/v/CodingWithCalvin.Debugalizers)](https://marketplace.visualstudio.com/items?itemName=CodingWithCalvin.Debugalizers)

## Features

Debugalizers provides specialized debug visualizers for various string content types, making it easier to inspect and understand data during debugging.

### Data Formats

| Visualizer | Description | Views |
|------------|-------------|-------|
| **JSON** | API responses, config files | Formatted, Tree, Raw |
| **XML** | SOAP, config, SVG | Formatted, Tree, Raw |
| **HTML** | Web content, email templates | Formatted, Rendered, Tree, Raw |
| **YAML** | Docker, K8s, CI/CD configs | Formatted, Tree, Raw |
| **TOML** | Rust configs, pyproject.toml | Formatted, Tree, Raw |
| **CSV/TSV** | Tabular data exports | Table, Raw |
| **INI** | Legacy config files | Formatted, Table, Raw |
| **Markdown** | Documentation strings | Rendered, Raw |
| **SQL** | Database queries | Formatted, Syntax Highlighted, Raw |
| **GraphQL** | API queries | Formatted, Syntax Highlighted, Raw |

### Encoded Data

| Visualizer | Description | Views |
|------------|-------------|-------|
| **Base64** | Decode text from Base64 | Decoded, Hex, Raw |
| **Base64 Image** | Embedded images (data:image/...) | Image Preview, Raw |
| **URL Encoded** | Query strings, form data | Decoded, Raw |
| **HTML Entities** | `&amp;` to `&`, `&lt;` to `<` | Decoded, Raw |
| **Unicode Escape** | `\u0041` to `A` | Decoded, Raw |
| **Hex String** | `48656C6C6F` to `Hello` | Decoded, Hex, Raw |
| **GZip/Deflate** | Compressed payloads | Decompressed, Raw |

### Security/Auth Tokens

| Visualizer | Description | Views |
|------------|-------------|-------|
| **JWT** | Decode header, payload, expiry | Claims Table, Decoded, Raw |
| **SAML** | Decode assertions | XML Tree, Claims, Raw |
| **X.509 Certificate** | PEM/DER certificates | Details Table, Raw |

### Structured Strings

| Visualizer | Description | Views |
|------------|-------------|-------|
| **Connection String** | DB/service connections | Parsed Table, Raw |
| **URI/URL** | Full URL parsing | Parsed Parts, Query Params Table |
| **Query String** | `?foo=bar&baz=qux` | Key-Value Table, Raw |
| **Regex** | Pattern visualization | Pattern Breakdown, Raw |
| **Cron Expression** | Schedule expressions | Human Readable, Next Runs |

### Binary/Low-Level

| Visualizer | Description | Views |
|------------|-------------|-------|
| **Hex Dump** | Binary data as hex | Hex + ASCII |
| **GUID/UUID** | Format and version info | Formatted, Details |
| **Timestamp** | Unix epoch conversion | Human Readable, UTC/Local |
| **IP Address** | IPv4/IPv6 details | Formatted, CIDR Info |

## Installation

### Visual Studio Marketplace

1. Open Visual Studio 2022
2. Go to **Extensions** > **Manage Extensions**
3. Search for "Debugalizers"
4. Click **Download** and restart Visual Studio

### Manual Installation

1. Download the latest `.vsix` from [Releases](https://github.com/CodingWithCalvin/VS-Debugalizers/releases)
2. Double-click the `.vsix` file to install
3. Restart Visual Studio

## Usage

1. Set a breakpoint in your code where a string variable is in scope
2. When the debugger hits the breakpoint, hover over the string variable
3. Click the magnifying glass icon in the DataTip
4. Select the appropriate visualizer from the list (e.g., "Debugalizers: JSON")
5. The visualizer window opens with your content formatted and viewable

### Keyboard Shortcuts

- **Ctrl+F** - Search within content
- **Escape** - Close visualizer window

### Toolbar Actions

- **Copy** - Copy raw content to clipboard
- **Copy Formatted** - Copy formatted content to clipboard
- **Export** - Save content to a file
- **Word Wrap** - Toggle word wrapping

## Requirements

- Visual Studio 2022 (17.0 or later)
- .NET Framework 4.8

## Building from Source

```bash
# Clone the repository
git clone https://github.com/CodingWithCalvin/VS-Debugalizers.git
cd VS-Debugalizers

# Build
dotnet build src/CodingWithCalvin.Debugalizers.slnx

# Run tests
dotnet test src/CodingWithCalvin.Debugalizers.slnx

# Build Release
dotnet build src/CodingWithCalvin.Debugalizers.slnx --configuration Release
```

The VSIX package will be in `src/CodingWithCalvin.Debugalizers/bin/Release/`.

## Contributing

Contributions are welcome! Please read the following guidelines:

1. Fork the repository
2. Create a feature branch: `git checkout -b feat/your-feature`
3. Use [Conventional Commits](https://www.conventionalcommits.org/) for commit messages
4. Submit a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributors

<!-- readme: collaborators,contributors -start -->
<!-- readme: collaborators,contributors -end -->
