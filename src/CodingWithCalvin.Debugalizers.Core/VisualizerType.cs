namespace CodingWithCalvin.Debugalizers.Core;

/// <summary>
/// Defines the type of content being visualized.
/// </summary>
public enum VisualizerType
{
    // Data Formats
    Json,
    Xml,
    Html,
    Yaml,
    Toml,
    Csv,
    Tsv,
    Ini,
    Markdown,
    Sql,
    GraphQl,

    // Encoded Data
    Base64,
    Base64Image,
    UrlEncoded,
    HtmlEntities,
    UnicodeEscape,
    HexString,
    GZip,
    Deflate,

    // Security/Auth
    Jwt,
    Saml,
    Certificate,

    // Structured Strings
    ConnectionString,
    Uri,
    QueryString,
    Regex,
    Cron,

    // Binary/Low-Level
    HexDump,
    Guid,
    Timestamp,
    IpAddress
}
