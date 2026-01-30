<p align="center">
  <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/logo.png" alt="Debugalizers Logo" width="128" height="128">
</p>

<h1 align="center">Debugalizers</h1>

<p align="center">
  <strong>A powerful collection of debug visualizers for Visual Studio with beautiful formatting and syntax highlighting</strong>
</p>

<p align="center">
  <a href="https://github.com/CodingWithCalvin/VS-Debugalizers/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/CodingWithCalvin/VS-Debugalizers?style=for-the-badge" alt="License">
  </a>
  <a href="https://github.com/CodingWithCalvin/VS-Debugalizers/actions/workflows/build.yml">
    <img src="https://img.shields.io/github/actions/workflow/status/CodingWithCalvin/VS-Debugalizers/build.yml?style=for-the-badge" alt="Build Status">
  </a>
</p>

<p align="center">
  <a href="https://marketplace.visualstudio.com/items?itemName=CodingWithCalvin.VS-Debugalizers">
    <img src="https://img.shields.io/visual-studio-marketplace/v/CodingWithCalvin.VS-Debugalizers?style=for-the-badge" alt="Marketplace Version">
  </a>
  <a href="https://marketplace.visualstudio.com/items?itemName=CodingWithCalvin.VS-Debugalizers">
    <img src="https://img.shields.io/visual-studio-marketplace/i/CodingWithCalvin.VS-Debugalizers?style=for-the-badge" alt="Marketplace Installations">
  </a>
  <a href="https://marketplace.visualstudio.com/items?itemName=CodingWithCalvin.VS-Debugalizers">
    <img src="https://img.shields.io/visual-studio-marketplace/d/CodingWithCalvin.VS-Debugalizers?style=for-the-badge" alt="Marketplace Downloads">
  </a>
  <a href="https://marketplace.visualstudio.com/items?itemName=CodingWithCalvin.VS-Debugalizers">
    <img src="https://img.shields.io/visual-studio-marketplace/r/CodingWithCalvin.VS-Debugalizers?style=for-the-badge" alt="Marketplace Rating">
  </a>
</p>

---

## Updated Interface

<p align="center">
  <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/updated-interface.png" alt="Updated Debugalizers Interface" width="700">
  <br>
  <em>The new modern Debugalizers interface</em>
</p>

> **Note:** The screenshots below were captured before the UI overhaul. The updated interface shown above features a modernized design with improved theming, rounded corners, and a cleaner layout. All functionality remains the same.

## Screenshots

<p align="center">
  <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/debugalizers-list.png" alt="Debugalizers List" width="400">
  <br>
  <em>Choose from 30+ specialized visualizers</em>
</p>

<table>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/json-formatted.png" alt="JSON Formatted View" width="400">
      <br>
      <em>JSON - Formatted View</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/xml-pretty.png" alt="XML Pretty View" width="400">
      <br>
      <em>XML - Pretty Printed</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/html-formatted.png" alt="HTML Formatted View" width="400">
      <br>
      <em>HTML - Formatted View</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/html-rendered.png" alt="HTML Rendered View" width="400">
      <br>
      <em>HTML - Rendered Preview</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/markdown-rendered.png" alt="Markdown Rendered View" width="400">
      <br>
      <em>Markdown - Rendered Preview</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/jwt-claims.png" alt="JWT Claims View" width="400">
      <br>
      <em>JWT - Claims Table</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/base64-image.png" alt="Base64 Image View" width="400">
      <br>
      <em>Base64 - Image Preview</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/uri-table.png" alt="URI Table View" width="400">
      <br>
      <em>URI - Parsed Components</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/connectionstring-table.png" alt="Connection String Table View" width="400">
      <br>
      <em>Connection String - Parsed Table</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/json-raw.png" alt="JSON Raw View" width="400">
      <br>
      <em>JSON - Raw View</em>
    </td>
  </tr>
</table>

## Features

- **30+ Visualizers** - JSON, XML, JWT, Base64, images, and many more
- **Multiple Views** - Raw, Formatted, Tree, Table, Hex, Rendered, and Image
- **Syntax Highlighting** - Beautiful code highlighting via AvalonEdit
- **Search** - Find text within large content (Ctrl+F)
- **Copy & Export** - Copy raw/formatted content or export to file
- **Multi-Architecture** - Supports both x64 and ARM64 systems

## Visualizer Catalog

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
| **HTML Entities** | `&amp;` → `&`, `&lt;` → `<` | Decoded, Raw |
| **Unicode Escape** | `\u0041` → `A` | Decoded, Raw |
| **Hex String** | `48656C6C6F` → `Hello` | Decoded, Hex, Raw |
| **GZip/Deflate** | Compressed payloads | Decompressed, Raw |

### Security & Auth Tokens

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

### Binary & Low-Level

| Visualizer | Description | Views |
|------------|-------------|-------|
| **Hex Dump** | Binary data as hex | Hex + ASCII |
| **GUID/UUID** | Format and version info | Formatted, Details |
| **Timestamp** | Unix epoch conversion | Human Readable, UTC/Local |
| **IP Address** | IPv4/IPv6 details | Formatted, CIDR Info |

## Installation

### Visual Studio Marketplace

1. Open Visual Studio 2022 or 2026
2. Go to **Extensions > Manage Extensions**
3. Search for "Debugalizers"
4. Click **Download** and restart Visual Studio

### Manual Installation

Download the latest `.vsix` from the [Releases](https://github.com/CodingWithCalvin/VS-Debugalizers/releases) page and double-click to install.

## Usage

1. Set a breakpoint where a string variable is in scope
2. When the debugger hits the breakpoint, hover over the variable
3. Click the **magnifying glass** icon in the DataTip
4. Select the appropriate visualizer (e.g., "**Debugalizers: JSON**")
5. The visualizer window opens with beautifully formatted content!

<p align="center">
  <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/debugalizers-list.png" alt="Selecting a Visualizer" width="500">
  <br>
  <em>Select a visualizer from the list</em>
</p>

### Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| `Ctrl+F` | Search within content |
| `Escape` | Close visualizer window |

### Toolbar Actions

| Button | Action |
|--------|--------|
| **Copy** | Copy raw content to clipboard |
| **Copy Formatted** | Copy formatted content to clipboard |
| **Export** | Save content to a file |
| **Word Wrap** | Toggle word wrapping |

## Requirements

- Visual Studio 2022 (17.8) or later
- .NET Framework 4.8

## Technology Stack

| Component | Technology |
|-----------|------------|
| Syntax Highlighting | AvalonEdit |
| JSON Parsing | Newtonsoft.Json |
| YAML Parsing | YamlDotNet |
| TOML Parsing | Tomlyn |
| JWT Decoding | System.IdentityModel.Tokens.Jwt |
| Markdown Rendering | Markdig |
| Cron Parsing | NCrontab |
| CSV Parsing | CsvHelper |

## Contributing

Contributions are welcome! Whether it's bug reports, feature requests, or pull requests - all feedback helps make this extension better.

### Development Setup

1. Clone the repository
2. Open the solution in Visual Studio 2022 or 2026
3. Ensure you have the "Visual Studio extension development" workload installed
4. Press F5 to launch the experimental instance

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## Contributors

<!-- readme: contributors -start -->
<!-- readme: contributors -end -->

---

<p align="center">
  Made with ❤️ by <a href="https://github.com/CodingWithCalvin">Coding With Calvin</a>
</p>
