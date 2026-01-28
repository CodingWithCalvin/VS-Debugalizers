// Sample application for testing Debugalizers extension
// Set breakpoints on lines with "// BREAKPOINT" comments and inspect the variables

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DebugalizersSampleApp;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Debugalizers Sample Application");
        Console.WriteLine("================================");
        Console.WriteLine();
        Console.WriteLine("Set breakpoints on lines marked with '// BREAKPOINT' comments,");
        Console.WriteLine("then use the magnifying glass icon in the debugger to visualize each variable.");
        Console.WriteLine();

        // Run all sample categories
        DataFormatSamples();
        EncodedDataSamples();
        SecuritySamples();
        StructuredStringSamples();
        BinaryAndLowLevelSamples();

        Console.WriteLine();
        Console.WriteLine("All samples completed!");
    }

    #region Data Format Samples (11 visualizers)

    private static void DataFormatSamples()
    {
        Console.WriteLine("=== Data Format Samples ===");

        // JSON - Pretty-prints and shows tree view
        var json = """
            {
                "name": "John Doe",
                "age": 30,
                "email": "john.doe@example.com",
                "address": {
                    "street": "123 Main St",
                    "city": "Springfield",
                    "state": "IL",
                    "zip": "62701"
                },
                "phoneNumbers": [
                    { "type": "home", "number": "555-1234" },
                    { "type": "work", "number": "555-5678" }
                ],
                "isActive": true,
                "balance": 1234.56
            }
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'json' variable

        // JSON Array
        var jsonArray = """
            [
                { "id": 1, "name": "Apple", "color": "red" },
                { "id": 2, "name": "Banana", "color": "yellow" },
                { "id": 3, "name": "Orange", "color": "orange" }
            ]
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'jsonArray' variable

        // XML - Pretty-prints and shows tree view
        var xml = """
            <?xml version="1.0" encoding="UTF-8"?>
            <bookstore>
                <book category="fiction">
                    <title lang="en">The Great Gatsby</title>
                    <author>F. Scott Fitzgerald</author>
                    <year>1925</year>
                    <price>10.99</price>
                </book>
                <book category="non-fiction">
                    <title lang="en">A Brief History of Time</title>
                    <author>Stephen Hawking</author>
                    <year>1988</year>
                    <price>15.99</price>
                </book>
            </bookstore>
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'xml' variable

        // HTML - Renders preview and shows formatted source
        var html = """
            <!DOCTYPE html>
            <html lang="en">
            <head>
                <meta charset="UTF-8">
                <title>Sample Page</title>
                <style>
                    body { font-family: Arial, sans-serif; margin: 20px; }
                    h1 { color: #333; }
                    .highlight { background-color: yellow; }
                </style>
            </head>
            <body>
                <h1>Welcome to Debugalizers</h1>
                <p>This is a <span class="highlight">sample HTML</span> document.</p>
                <ul>
                    <li>Item 1</li>
                    <li>Item 2</li>
                    <li>Item 3</li>
                </ul>
                <a href="https://example.com">Visit Example</a>
            </body>
            </html>
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'html' variable

        // YAML - Common configuration format
        var yaml = """
            # Application Configuration
            application:
              name: MyApp
              version: 1.0.0
              environment: production

            database:
              host: localhost
              port: 5432
              name: myapp_db
              credentials:
                username: admin
                password: secret123

            features:
              - name: authentication
                enabled: true
              - name: caching
                enabled: true
                ttl: 3600
              - name: logging
                enabled: true
                level: info
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'yaml' variable

        // TOML - Configuration format (common in Rust/Python)
        var toml = """
            # This is a TOML document

            title = "TOML Example"

            [owner]
            name = "Tom Preston-Werner"
            dob = 1979-05-27T07:32:00-08:00

            [database]
            enabled = true
            ports = [ 8000, 8001, 8002 ]
            data = [ ["delta", "phi"], [3.14] ]
            temp_targets = { cpu = 79.5, case = 72.0 }

            [servers]

            [servers.alpha]
            ip = "10.0.0.1"
            role = "frontend"

            [servers.beta]
            ip = "10.0.0.2"
            role = "backend"
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'toml' variable

        // CSV - Comma-separated values
        var csv = """
            Name,Age,Email,Department,Salary
            John Doe,30,john@example.com,Engineering,75000
            Jane Smith,28,jane@example.com,Marketing,65000
            Bob Johnson,35,bob@example.com,Sales,70000
            Alice Brown,32,alice@example.com,HR,60000
            Charlie Wilson,40,charlie@example.com,Engineering,85000
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'csv' variable

        // TSV - Tab-separated values
        var tsv = "Product\tPrice\tQuantity\tCategory\n" +
                  "Laptop\t999.99\t50\tElectronics\n" +
                  "Mouse\t29.99\t200\tAccessories\n" +
                  "Keyboard\t79.99\t150\tAccessories\n" +
                  "Monitor\t299.99\t75\tElectronics";
        Debugger.Break(); // BREAKPOINT - Inspect 'tsv' variable

        // INI - Configuration file format
        var ini = """
            [General]
            AppName=MyApplication
            Version=2.0.1
            Debug=false

            [Database]
            Server=localhost
            Port=3306
            Database=myapp
            User=root

            [Logging]
            Level=Warning
            Path=C:\Logs\app.log
            MaxSize=10MB
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'ini' variable

        // Markdown - Renders preview
        var markdown = """
            # Debugalizers Documentation

            Welcome to **Debugalizers**, a Visual Studio extension for debugging string content.

            ## Features

            - JSON visualization with tree view
            - XML pretty-printing
            - Base64 decoding
            - JWT token inspection

            ### Code Example

            ```csharp
            var json = "{ \"name\": \"test\" }";
            // Set breakpoint and use visualizer
            ```

            ## Installation

            1. Open Visual Studio
            2. Go to Extensions > Manage Extensions
            3. Search for "Debugalizers"
            4. Click Install

            > **Note:** Restart Visual Studio after installation.

            For more info, visit [our website](https://example.com).
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'markdown' variable

        // SQL - Syntax highlighting and formatting
        var sql = """
            SELECT
                u.id,
                u.username,
                u.email,
                COUNT(o.id) as order_count,
                SUM(o.total) as total_spent
            FROM users u
            LEFT JOIN orders o ON u.id = o.user_id
            WHERE u.created_at >= '2024-01-01'
                AND u.status = 'active'
            GROUP BY u.id, u.username, u.email
            HAVING COUNT(o.id) > 5
            ORDER BY total_spent DESC
            LIMIT 100;
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'sql' variable

        // GraphQL - Query language for APIs
        var graphql = """
            query GetUserWithOrders($userId: ID!, $limit: Int = 10) {
              user(id: $userId) {
                id
                name
                email
                profile {
                  avatar
                  bio
                }
                orders(first: $limit, orderBy: { field: CREATED_AT, direction: DESC }) {
                  edges {
                    node {
                      id
                      total
                      status
                      items {
                        product {
                          name
                          price
                        }
                        quantity
                      }
                    }
                  }
                  pageInfo {
                    hasNextPage
                    endCursor
                  }
                }
              }
            }
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'graphql' variable

        Console.WriteLine("  Data format samples ready for inspection");
    }

    #endregion

    #region Encoded Data Samples (8 visualizers)

    private static void EncodedDataSamples()
    {
        Console.WriteLine("=== Encoded Data Samples ===");

        // Base64 - Encoded text
        var base64Text = Convert.ToBase64String(Encoding.UTF8.GetBytes(
            "Hello, World! This is a secret message encoded in Base64."));
        Debugger.Break(); // BREAKPOINT - Inspect 'base64Text' variable

        // Base64 - Encoded JSON
        var base64Json = Convert.ToBase64String(Encoding.UTF8.GetBytes(
            """{"user":"admin","role":"superuser","permissions":["read","write","delete"]}"""));
        Debugger.Break(); // BREAKPOINT - Inspect 'base64Json' variable

        // Base64 Image - Small red dot PNG (data URI)
        var base64Image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFUlEQVR42mP8z8BQz0AEYBxVSF+FABJADq/cY1v6AAAAAElFTkSuQmCC";
        Debugger.Break(); // BREAKPOINT - Inspect 'base64Image' variable

        // Base64 Image - Small green square PNG
        var base64ImageGreen = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFUlEQVR42mNk+M9Qz0AEYBxVSF+FAAhKDq19sRgvAAAAAElFTkSuQmCC";
        Debugger.Break(); // BREAKPOINT - Inspect 'base64ImageGreen' variable

        // URL Encoded - Common in query strings
        var urlEncoded = "name=John%20Doe&email=john.doe%40example.com&message=Hello%2C%20World%21%20This%20is%20a%20test%20%26%20demo.";
        Debugger.Break(); // BREAKPOINT - Inspect 'urlEncoded' variable

        // URL Encoded - Full URL with encoded path
        var urlEncodedFull = "https%3A%2F%2Fexample.com%2Fapi%2Fusers%3Fname%3DJohn%20Doe%26filter%3Dactive";
        Debugger.Break(); // BREAKPOINT - Inspect 'urlEncodedFull' variable

        // HTML Entities - Encoded HTML content
        var htmlEntities = "&lt;div class=&quot;container&quot;&gt;&lt;p&gt;Hello &amp; Welcome!&lt;/p&gt;&lt;span&gt;Price: &pound;50 &mdash; Special &copy; 2024&lt;/span&gt;&lt;/div&gt;";
        Debugger.Break(); // BREAKPOINT - Inspect 'htmlEntities' variable

        // Unicode Escape - JavaScript-style escaped string
        var unicodeEscape = @"Hello \u0057\u006f\u0072\u006c\u0064! \u4f60\u597d \u0048\u0065\u006c\u006c\u006f";
        Debugger.Break(); // BREAKPOINT - Inspect 'unicodeEscape' variable

        // Hex String - Hexadecimal encoded data
        var hexString = "48656c6c6f2c20576f726c6421205468697320697320612068657820656e636f64656420737472696e672e";
        Debugger.Break(); // BREAKPOINT - Inspect 'hexString' variable

        // GZip Compressed - Base64 encoded gzip data
        var gzipData = CreateGzipBase64("This is compressed content using GZip compression algorithm.");
        Debugger.Break(); // BREAKPOINT - Inspect 'gzipData' variable

        // Deflate Compressed - Base64 encoded deflate data
        var deflateData = CreateDeflateBase64("This is compressed content using Deflate compression algorithm.");
        Debugger.Break(); // BREAKPOINT - Inspect 'deflateData' variable

        Console.WriteLine("  Encoded data samples ready for inspection");
    }

    private static string CreateGzipBase64(string content)
    {
        using var outputStream = new MemoryStream();
        using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            gzipStream.Write(bytes, 0, bytes.Length);
        }
        return Convert.ToBase64String(outputStream.ToArray());
    }

    private static string CreateDeflateBase64(string content)
    {
        using var outputStream = new MemoryStream();
        using (var deflateStream = new DeflateStream(outputStream, CompressionMode.Compress))
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            deflateStream.Write(bytes, 0, bytes.Length);
        }
        return Convert.ToBase64String(outputStream.ToArray());
    }

    #endregion

    #region Security Samples (3 visualizers)

    private static void SecuritySamples()
    {
        Console.WriteLine("=== Security Samples ===");

        // JWT - JSON Web Token (this is a sample token, not a real secret)
        var jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiZW1haWwiOiJqb2huQGV4YW1wbGUuY29tIiwicm9sZSI6ImFkbWluIiwiaWF0IjoxNTE2MjM5MDIyLCJleHAiOjE1MTYyNDI2MjJ9.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        Debugger.Break(); // BREAKPOINT - Inspect 'jwt' variable

        // JWT with more claims
        var jwtComplex = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImtleS0xMjMifQ.eyJpc3MiOiJodHRwczovL2F1dGguZXhhbXBsZS5jb20iLCJzdWIiOiJ1c2VyXzEyMzQ1Iiwi" +
                         "YXVkIjpbImFwaS5leGFtcGxlLmNvbSIsIm15YXBwLmV4YW1wbGUuY29tIl0sImV4cCI6MTcwNDA2NzIwMCwiaWF0IjoxNzA0MDYzNjAwLCJuYmYiOjE3MDQwNjM2MDAsImp0aSI6" +
                         "InVuaXF1ZS10b2tlbi1pZC0xMjMiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIiwicGVybWlzc2lvbnMiOlsicmVhZDp1c2VycyIsIndyaXRlOnVzZXJzIiwiZGVsZXRl" +
                         "OnVzZXJzIl19.signature_placeholder";
        Debugger.Break(); // BREAKPOINT - Inspect 'jwtComplex' variable

        // SAML Assertion (simplified example)
        var saml = """
            <saml:Assertion xmlns:saml="urn:oasis:names:tc:SAML:2.0:assertion"
                            ID="_abc123"
                            Version="2.0"
                            IssueInstant="2024-01-15T10:30:00Z">
                <saml:Issuer>https://idp.example.com</saml:Issuer>
                <saml:Subject>
                    <saml:NameID Format="urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress">
                        john.doe@example.com
                    </saml:NameID>
                    <saml:SubjectConfirmation Method="urn:oasis:names:tc:SAML:2.0:cm:bearer">
                        <saml:SubjectConfirmationData
                            NotOnOrAfter="2024-01-15T10:35:00Z"
                            Recipient="https://sp.example.com/acs"/>
                    </saml:SubjectConfirmation>
                </saml:Subject>
                <saml:Conditions NotBefore="2024-01-15T10:25:00Z" NotOnOrAfter="2024-01-15T10:35:00Z">
                    <saml:AudienceRestriction>
                        <saml:Audience>https://sp.example.com</saml:Audience>
                    </saml:AudienceRestriction>
                </saml:Conditions>
                <saml:AuthnStatement AuthnInstant="2024-01-15T10:30:00Z">
                    <saml:AuthnContext>
                        <saml:AuthnContextClassRef>
                            urn:oasis:names:tc:SAML:2.0:ac:classes:Password
                        </saml:AuthnContextClassRef>
                    </saml:AuthnContext>
                </saml:AuthnStatement>
                <saml:AttributeStatement>
                    <saml:Attribute Name="email">
                        <saml:AttributeValue>john.doe@example.com</saml:AttributeValue>
                    </saml:Attribute>
                    <saml:Attribute Name="firstName">
                        <saml:AttributeValue>John</saml:AttributeValue>
                    </saml:Attribute>
                    <saml:Attribute Name="lastName">
                        <saml:AttributeValue>Doe</saml:AttributeValue>
                    </saml:Attribute>
                    <saml:Attribute Name="roles">
                        <saml:AttributeValue>admin</saml:AttributeValue>
                        <saml:AttributeValue>user</saml:AttributeValue>
                    </saml:Attribute>
                </saml:AttributeStatement>
            </saml:Assertion>
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'saml' variable

        // X.509 Certificate (PEM format - this is a sample/test certificate)
        var certificate = """
            -----BEGIN CERTIFICATE-----
            MIIDXTCCAkWgAwIBAgIJAJC1HiIAZAiUMA0GCSqGSIb3Qw0teleqB0T1G9v3vHlb
            EcuF+t2Oie8li0bYO4K9hE6GhQNkwWG8KGYmE0A
            YkoCgkfXnBUmlBYVpG0kKDTlz7hqrYVr
            WLBXmyMDAwMBAf8wDQYJKoZIhvcNAQELBQADggEBAGIkGhP9aL4MY5CkZ4iJ9yA0
            F7NN91I5vDkvH1d8m4qP9l4Dq0YP5JzCniUl6pGnK9pB6VQNBQE8A4Acb8OVT1k9
            IyB3YNz1V2nV+PJSVY7ELdNOLzlKhU4CDReAxPB6FLSV9wM+xYJzMMDkVvFyLl8t
            lNjPrQvCLd8dGLfqBqnPdPeH8DkPx6J9Sx3qVJn7nXJHZqRm0qcE7kKoKtFnVmjy
            qkWvC9PKTqNgKjHPuGNc0F54Q0SE4P9VCK0sLr4l4txzqNVRhKGU5E/yCHhAPJCb
            pyDiOdDAy/elvFyLZi0bI9yGJnB7 IOuDPBwWeuGm7vJH9vU3pJ0KWv8jH4cA=
            -----END CERTIFICATE-----
            """;
        Debugger.Break(); // BREAKPOINT - Inspect 'certificate' variable

        Console.WriteLine("  Security samples ready for inspection");
    }

    #endregion

    #region Structured String Samples (5 visualizers)

    private static void StructuredStringSamples()
    {
        Console.WriteLine("=== Structured String Samples ===");

        // Connection String - SQL Server
        var connectionStringSql = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        Debugger.Break(); // BREAKPOINT - Inspect 'connectionStringSql' variable

        // Connection String - PostgreSQL
        var connectionStringPg = "Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=secret;SSL Mode=Require;Pooling=true;Maximum Pool Size=100;";
        Debugger.Break(); // BREAKPOINT - Inspect 'connectionStringPg' variable

        // Connection String - MongoDB
        var connectionStringMongo = "mongodb+srv://username:password@cluster0.mongodb.net/mydb?retryWrites=true&w=majority&maxPoolSize=50";
        Debugger.Break(); // BREAKPOINT - Inspect 'connectionStringMongo' variable

        // URI - HTTP with all components
        var uriHttp = "https://user:pass@api.example.com:8443/v2/users/123/profile?fields=name,email&include=avatar&format=json#section-1";
        Debugger.Break(); // BREAKPOINT - Inspect 'uriHttp' variable

        // URI - File path
        var uriFile = "file:///C:/Users/John/Documents/report.pdf";
        Debugger.Break(); // BREAKPOINT - Inspect 'uriFile' variable

        // URI - mailto
        var uriMailto = "mailto:support@example.com?subject=Help%20Request&body=I%20need%20assistance%20with...";
        Debugger.Break(); // BREAKPOINT - Inspect 'uriMailto' variable

        // Query String - Complex with arrays
        var queryString = "search=debugger&category=tools&tags[]=csharp&tags[]=dotnet&page=1&limit=25&sort=relevance&order=desc&filter[status]=active&filter[type]=extension";
        Debugger.Break(); // BREAKPOINT - Inspect 'queryString' variable

        // Regex - Email validation pattern
        var regexEmail = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        Debugger.Break(); // BREAKPOINT - Inspect 'regexEmail' variable

        // Regex - URL pattern
        var regexUrl = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";
        Debugger.Break(); // BREAKPOINT - Inspect 'regexUrl' variable

        // Regex - Phone number pattern
        var regexPhone = @"^(?:\+?1[-.\s]?)?\(?([0-9]{3})\)?[-.\s]?([0-9]{3})[-.\s]?([0-9]{4})$";
        Debugger.Break(); // BREAKPOINT - Inspect 'regexPhone' variable

        // Cron Expression - Every weekday at 9 AM
        var cronWeekday = "0 9 * * 1-5";
        Debugger.Break(); // BREAKPOINT - Inspect 'cronWeekday' variable

        // Cron Expression - Every 15 minutes
        var cronFifteenMin = "*/15 * * * *";
        Debugger.Break(); // BREAKPOINT - Inspect 'cronFifteenMin' variable

        // Cron Expression - First day of every month at midnight
        var cronMonthly = "0 0 1 * *";
        Debugger.Break(); // BREAKPOINT - Inspect 'cronMonthly' variable

        // Cron Expression - Complex schedule
        var cronComplex = "30 4 1,15 * 1-5";
        Debugger.Break(); // BREAKPOINT - Inspect 'cronComplex' variable

        Console.WriteLine("  Structured string samples ready for inspection");
    }

    #endregion

    #region Binary and Low-Level Samples (4 visualizers)

    private static void BinaryAndLowLevelSamples()
    {
        Console.WriteLine("=== Binary and Low-Level Samples ===");

        // GUID - Standard format
        var guid1 = "550e8400-e29b-41d4-a716-446655440000";
        Debugger.Break(); // BREAKPOINT - Inspect 'guid1' variable

        // GUID - Various formats (all same GUID)
        var guid2 = "6F9619FF-8B86-D011-B42D-00CF4FC964FF";
        Debugger.Break(); // BREAKPOINT - Inspect 'guid2' variable

        // GUID - As Guid object converted to string
        var guid3 = Guid.NewGuid().ToString();
        Debugger.Break(); // BREAKPOINT - Inspect 'guid3' variable

        // IP Address - IPv4
        var ipv4 = "192.168.1.100";
        Debugger.Break(); // BREAKPOINT - Inspect 'ipv4' variable

        // IP Address - IPv4 localhost
        var ipv4Localhost = "127.0.0.1";
        Debugger.Break(); // BREAKPOINT - Inspect 'ipv4Localhost' variable

        // IP Address - IPv6
        var ipv6 = "2001:0db8:85a3:0000:0000:8a2e:0370:7334";
        Debugger.Break(); // BREAKPOINT - Inspect 'ipv6' variable

        // IP Address - IPv6 compressed
        var ipv6Compressed = "2001:db8:85a3::8a2e:370:7334";
        Debugger.Break(); // BREAKPOINT - Inspect 'ipv6Compressed' variable

        // IP Address - IPv6 localhost
        var ipv6Localhost = "::1";
        Debugger.Break(); // BREAKPOINT - Inspect 'ipv6Localhost' variable

        // Unix Timestamp - Seconds (10 digits) - January 1, 2024 00:00:00 UTC
        var timestampSeconds = "1704067200";
        Debugger.Break(); // BREAKPOINT - Inspect 'timestampSeconds' variable

        // Unix Timestamp - Milliseconds (13 digits)
        var timestampMillis = "1704067200000";
        Debugger.Break(); // BREAKPOINT - Inspect 'timestampMillis' variable

        // Unix Timestamp - Current time
        var timestampNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        Debugger.Break(); // BREAKPOINT - Inspect 'timestampNow' variable

        // Hex Dump - Binary data represented for hex view
        var hexDumpData = "The quick brown fox jumps over the lazy dog. 0123456789 !@#$%^&*()";
        Debugger.Break(); // BREAKPOINT - Inspect 'hexDumpData' variable

        // Binary data as string for hex dump viewing
        var binaryString = Encoding.UTF8.GetString(new byte[] {
            0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F,
            0x72, 0x6C, 0x64, 0x21, 0x00, 0x01, 0x02, 0x03,
            0xFF, 0xFE, 0xFD, 0xFC, 0x0A, 0x0D, 0x09, 0x20
        });
        Debugger.Break(); // BREAKPOINT - Inspect 'binaryString' variable

        Console.WriteLine("  Binary and low-level samples ready for inspection");
    }

    #endregion
}
