<p align="center">
  <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/logo.png" alt="Debugalizers Logo" width="128" />
</p>

<h1 align="center">Debugalizers</h1>

<p align="center">
  <a href="https://visualstudio.microsoft.com/"><img src="https://img.shields.io/badge/Visual%20Studio-2022%20%7C%202026-purple?style=for-the-badge&logo=visualstudio&logoColor=white" alt="Visual Studio 2022"></a>
  <a href="https://dotnet.microsoft.com/"><img src="https://img.shields.io/badge/.NET%20Framework-4.8-blue?style=for-the-badge&logo=dotnet" alt=".NET Framework"></a>
  <a href="LICENSE"><img src="https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge" alt="License: MIT"></a>
</p>

<p align="center">
  <a href="https://marketplace.visualstudio.com/items?itemName=CodingWithCalvin.VS-Debugalizers"><img src="https://img.shields.io/visual-studio-marketplace/v/CodingWithCalvin.VS-Debugalizers?style=for-the-badge&label=Marketplace" alt="Marketplace Version"></a>
  <a href="https://marketplace.visualstudio.com/items?itemName=CodingWithCalvin.VS-Debugalizers"><img src="https://img.shields.io/visual-studio-marketplace/i/CodingWithCalvin.VS-Debugalizers?style=for-the-badge&label=Installs" alt="Marketplace Installs"></a>
  <a href="https://github.com/CodingWithCalvin/VS-Debugalizers/actions/workflows/build.yml"><img src="https://img.shields.io/github/actions/workflow/status/CodingWithCalvin/VS-Debugalizers/build.yml?style=for-the-badge&label=Build" alt="Build Status"></a>
</p>

---

A **powerful collection** of debug visualizers for Visual Studio, providing beautiful formatting, syntax highlighting, and specialized views for common string data types. Stop squinting at raw JSON in the debugger!

---

## Updated Interface

<p align="center">
  <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/updated-interface.png" alt="Updated Debugalizers Interface" width="700" />
  <br />
  <em>The new modern Debugalizers interface</em>
</p>

> **Note:** The screenshots below were captured before the UI overhaul. The updated interface shown above features a modernized design with improved theming, rounded corners, and a cleaner layout. All functionality remains the same.

---

## Screenshots

<p align="center">
  <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/debugalizers-list.png" alt="Debugalizers List" width="400" />
  <br />
  <em>Choose from 30+ specialized visualizers</em>
</p>

<table>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/json-formatted.png" alt="JSON Formatted View" width="400" />
      <br />
      <em>JSON - Formatted View</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/xml-pretty.png" alt="XML Pretty View" width="400" />
      <br />
      <em>XML - Pretty Printed</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/html-formatted.png" alt="HTML Formatted View" width="400" />
      <br />
      <em>HTML - Formatted View</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/html-rendered.png" alt="HTML Rendered View" width="400" />
      <br />
      <em>HTML - Rendered Preview</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/markdown-rendered.png" alt="Markdown Rendered View" width="400" />
      <br />
      <em>Markdown - Rendered Preview</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/jwt-claims.png" alt="JWT Claims View" width="400" />
      <br />
      <em>JWT - Claims Table</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/base64-image.png" alt="Base64 Image View" width="400" />
      <br />
      <em>Base64 - Image Preview</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/uri-table.png" alt="URI Table View" width="400" />
      <br />
      <em>URI - Parsed Components</em>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/connectionstring-table.png" alt="Connection String Table View" width="400" />
      <br />
      <em>Connection String - Parsed Table</em>
    </td>
    <td align="center">
      <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/json-raw.png" alt="JSON Raw View" width="400" />
      <br />
      <em>JSON - Raw View</em>
    </td>
  </tr>
</table>

---

## Features

- **30+ Visualizers** - JSON, XML, JWT, Base64, images, and many more
- **Multiple Views** - Raw, Formatted, Tree, Table, Hex, Rendered, and Image
- **Syntax Highlighting** - Beautiful code highlighting via AvalonEdit
- **Search** - Find text within large content (Ctrl+F)
- **Copy & Export** - Copy raw/formatted content or export to file
- **Multi-Architecture** - Supports both x64 and ARM64 systems

---

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
| **HTML Entities** | `&amp;` to `&`, `&lt;` to `<` | Decoded, Raw |
| **Unicode Escape** | `\u0041` to `A` | Decoded, Raw |
| **Hex String** | `48656C6C6F` to `Hello` | Decoded, Hex, Raw |
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

---

## Installation

### From Visual Studio Marketplace

[![VS Marketplace](https://img.shields.io/badge/VS%20Marketplace-Debugalizers-purple?style=for-the-badge&logo=visualstudio&logoColor=white)](https://marketplace.visualstudio.com/items?itemName=CodingWithCalvin.VS-Debugalizers)

1. Open Visual Studio 2022
2. Go to **Extensions** > **Manage Extensions**
3. Search for "**Debugalizers**"
4. Click **Download** and restart Visual Studio

### From Source

```bash
# 1. Clone the repository
git clone https://github.com/CodingWithCalvin/VS-Debugalizers.git

# 2. Build the solution
dotnet build src/CodingWithCalvin.Debugalizers.slnx

# 3. Run tests
dotnet test src/CodingWithCalvin.Debugalizers.slnx

# 4. VSIX will be created in bin/Debug or bin/Release
```

---

## Usage

1. Set a breakpoint where a string variable is in scope
2. When the debugger hits the breakpoint, hover over the variable
3. Click the **magnifying glass** icon in the DataTip
4. Select the appropriate visualizer (e.g., "**Debugalizers: JSON**")
5. The visualizer window opens with beautifully formatted content!

<p align="center">
  <img src="https://raw.githubusercontent.com/CodingWithCalvin/VS-Debugalizers/main/resources/debugalizers-list.png" alt="Selecting a Visualizer" width="500" />
  <br />
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

---

## Requirements

- Visual Studio 2022 (17.8) or later
- .NET Framework 4.8

---

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

---

## Contributing

Contributions are welcome! Feel free to submit issues and pull requests.

1. Fork the repository
2. Create a feature branch (`git checkout -b feat/amazing-feature`)
3. Commit your changes (`git commit -m 'feat: add amazing feature'`)
4. Push to the branch (`git push origin feat/amazing-feature`)
5. Open a Pull Request

---

## License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## Contributors

<!-- readme: contributors -start -->
[![CalvinAllen](https://avatars.githubusercontent.com/u/41448698?v=4&s=64)](https://github.com/CalvinAllen) [![lennartb-](https://avatars.githubusercontent.com/u/5563601?v=4&s=64)](https://github.com/lennartb-)
<!-- readme: contributors -end -->

---

<div align="center">

### If you find Debugalizers useful, please consider giving it a star!

*Made with care for the Visual Studio community from Coding With Calvin*

</div>
