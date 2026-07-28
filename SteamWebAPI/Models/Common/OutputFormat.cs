using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Common;

/// <summary>
/// The wire format steamwebapi.com should render a response in. All typed client methods request
/// <see cref="Json"/> internally; this is exposed only for the rare case where a caller wants the raw
/// export payload for a format-sensitive endpoint via a lower-level overload.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<OutputFormat>))]
public enum OutputFormat
{
    /// <summary>Standard JSON (default).</summary>
    [EnumMember(Value = "json")]
    Json = 0,

    /// <summary>Gzip-compressed JSON.</summary>
    [EnumMember(Value = "gzip")]
    Gzip,

    /// <summary>Zip-compressed JSON.</summary>
    [EnumMember(Value = "zip")]
    Zip,

    /// <summary>Comma-separated values.</summary>
    [EnumMember(Value = "csv")]
    Csv,

    /// <summary>XML.</summary>
    [EnumMember(Value = "xml")]
    Xml,

    /// <summary>HTML table.</summary>
    [EnumMember(Value = "html")]
    Html,

    /// <summary>Newline-delimited JSON (streaming).</summary>
    [EnumMember(Value = "ndjson")]
    NdJson,

    /// <summary>MySQL INSERT statements.</summary>
    [EnumMember(Value = "mysql")]
    Mysql,

    /// <summary>MySQL INSERT statements including a CREATE TABLE.</summary>
    [EnumMember(Value = "mysql_with_table")]
    MysqlWithTable,

    /// <summary>PostgreSQL INSERT statements.</summary>
    [EnumMember(Value = "pgsql")]
    Pgsql,

    /// <summary>PostgreSQL INSERT statements including a CREATE TABLE.</summary>
    [EnumMember(Value = "pgsql_with_table")]
    PgsqlWithTable,

    /// <summary>MongoDB insert documents.</summary>
    [EnumMember(Value = "mongo")]
    Mongo,
}
