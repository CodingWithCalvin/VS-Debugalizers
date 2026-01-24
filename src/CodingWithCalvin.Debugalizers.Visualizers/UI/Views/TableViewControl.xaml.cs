using System;
using CodingWithCalvin.Debugalizers.Core;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Controls;
using CodingWithCalvin.Debugalizers.Visualizers;
using CsvHelper;
using CsvHelper.Configuration;

namespace CodingWithCalvin.Debugalizers.UI.Views;

/// <summary>
/// A view that displays tabular data in a grid.
/// </summary>
public partial class TableViewControl : UserControl
{
    /// <summary>
    /// Initializes a new instance of the TableViewControl.
    /// </summary>
    /// <param name="content">The content to display.</param>
    /// <param name="type">The visualizer type.</param>
    public TableViewControl(string content, VisualizerType type)
    {
        InitializeComponent();

        try
        {
            var dataTable = type switch
            {
                VisualizerType.Csv => ParseCsv(content, ","),
                VisualizerType.Tsv => ParseCsv(content, "\t"),
                VisualizerType.Ini => ParseIni(content),
                VisualizerType.ConnectionString => ParseConnectionString(content),
                VisualizerType.QueryString => ParseQueryString(content),
                VisualizerType.Jwt => ParseJwt(content),
                VisualizerType.Uri => ParseUri(content),
                _ => ParseKeyValue(content)
            };

            DataTable.ItemsSource = dataTable.DefaultView;
        }
        catch (Exception ex)
        {
            var errorTable = new DataTable();
            errorTable.Columns.Add("Error");
            errorTable.Rows.Add(ex.Message);
            DataTable.ItemsSource = errorTable.DefaultView;
        }
    }

    private DataTable ParseCsv(string content, string delimiter)
    {
        var table = new DataTable();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = delimiter,
            HasHeaderRecord = true,
            MissingFieldFound = null
        };

        using (var reader = new StringReader(content))
        using (var csv = new CsvReader(reader, config))
        {
            using (var dr = new CsvDataReader(csv))
            {
                table.Load(dr);
            }
        }

        return table;
    }

    private DataTable ParseIni(string content)
    {
        var table = new DataTable();
        table.Columns.Add("Section");
        table.Columns.Add("Key");
        table.Columns.Add("Value");

        var currentSection = "";
        var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith(";") || trimmed.StartsWith("#"))
            {
                continue;
            }

            if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
            {
                currentSection = trimmed.Substring(1, trimmed.Length - 2);
            }
            else
            {
                var equalIndex = trimmed.IndexOf('=');
                if (equalIndex > 0)
                {
                    var key = trimmed.Substring(0, equalIndex).Trim();
                    var value = trimmed.Substring(equalIndex + 1).Trim();
                    table.Rows.Add(currentSection, key, value);
                }
            }
        }

        return table;
    }

    private DataTable ParseConnectionString(string content)
    {
        var table = new DataTable();
        table.Columns.Add("Property");
        table.Columns.Add("Value");

        var parts = content.Split(';');
        foreach (var part in parts)
        {
            var trimmed = part.Trim();
            if (string.IsNullOrEmpty(trimmed))
            {
                continue;
            }

            var equalIndex = trimmed.IndexOf('=');
            if (equalIndex > 0)
            {
                var key = trimmed.Substring(0, equalIndex).Trim();
                var value = trimmed.Substring(equalIndex + 1).Trim();
                table.Rows.Add(key, value);
            }
        }

        return table;
    }

    private DataTable ParseQueryString(string content)
    {
        var table = new DataTable();
        table.Columns.Add("Parameter");
        table.Columns.Add("Value");
        table.Columns.Add("Decoded Value");

        // Remove leading ? if present
        if (content.StartsWith("?"))
        {
            content = content.Substring(1);
        }

        var nameValues = HttpUtility.ParseQueryString(content);
        foreach (var key in nameValues.AllKeys)
        {
            var value = nameValues[key];
            var decoded = HttpUtility.UrlDecode(value);
            table.Rows.Add(key ?? "(empty)", value, decoded);
        }

        return table;
    }

    private DataTable ParseJwt(string content)
    {
        var table = new DataTable();
        table.Columns.Add("Claim");
        table.Columns.Add("Value");
        table.Columns.Add("Type");

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(content);

            // Header
            table.Rows.Add("Algorithm", token.Header.Alg, "Header");
            table.Rows.Add("Type", token.Header.Typ, "Header");

            // Standard claims
            if (token.Issuer != null)
            {
                table.Rows.Add("Issuer (iss)", token.Issuer, "Payload");
            }
            if (token.Audiences.Any())
            {
                table.Rows.Add("Audience (aud)", string.Join(", ", token.Audiences), "Payload");
            }
            if (token.Subject != null)
            {
                table.Rows.Add("Subject (sub)", token.Subject, "Payload");
            }
            table.Rows.Add("Issued At (iat)", token.IssuedAt.ToString("O"), "Payload");
            table.Rows.Add("Not Before (nbf)", token.ValidFrom.ToString("O"), "Payload");
            table.Rows.Add("Expires (exp)", token.ValidTo.ToString("O"), "Payload");

            // Calculate expiry status
            var now = DateTime.UtcNow;
            var isExpired = now > token.ValidTo;
            var expiresIn = token.ValidTo - now;
            table.Rows.Add("Status", isExpired ? "EXPIRED" : $"Valid (expires in {expiresIn:g})", "Info");

            // Custom claims
            foreach (var claim in token.Claims.Where(c =>
                !new[] { "iss", "aud", "sub", "iat", "nbf", "exp" }.Contains(c.Type)))
            {
                table.Rows.Add(claim.Type, claim.Value, "Custom");
            }
        }
        catch (Exception ex)
        {
            table.Rows.Add("Error", ex.Message, "Error");
        }

        return table;
    }

    private DataTable ParseUri(string content)
    {
        var table = new DataTable();
        table.Columns.Add("Component");
        table.Columns.Add("Value");

        try
        {
            var uri = new Uri(content);

            table.Rows.Add("Scheme", uri.Scheme);
            table.Rows.Add("Host", uri.Host);
            table.Rows.Add("Port", uri.Port);
            table.Rows.Add("Path", uri.AbsolutePath);
            table.Rows.Add("Query", uri.Query);
            table.Rows.Add("Fragment", uri.Fragment);
            table.Rows.Add("User Info", uri.UserInfo);
            table.Rows.Add("Is Default Port", uri.IsDefaultPort);
            table.Rows.Add("Is Loopback", uri.IsLoopback);

            // Parse query string
            if (!string.IsNullOrEmpty(uri.Query))
            {
                var nameValues = HttpUtility.ParseQueryString(uri.Query);
                foreach (var key in nameValues.AllKeys)
                {
                    table.Rows.Add($"Query: {key}", nameValues[key]);
                }
            }
        }
        catch (Exception ex)
        {
            table.Rows.Add("Error", ex.Message);
        }

        return table;
    }

    private DataTable ParseKeyValue(string content)
    {
        var table = new DataTable();
        table.Columns.Add("Key");
        table.Columns.Add("Value");

        var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var separatorIndex = line.IndexOfAny(new[] { '=', ':', '\t' });
            if (separatorIndex > 0)
            {
                var key = line.Substring(0, separatorIndex).Trim();
                var value = line.Substring(separatorIndex + 1).Trim();
                table.Rows.Add(key, value);
            }
            else
            {
                table.Rows.Add(line, "");
            }
        }

        return table;
    }
}
