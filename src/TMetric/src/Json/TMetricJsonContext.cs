using System.Text.Json.Serialization;

namespace TMetric.Json;

[JsonSerializable( typeof( Abstractions.V2.Client[] ) )]
[JsonSerializable( typeof( Abstractions.V3.ClientBasic[] ) )]
[JsonSerializable( typeof( Abstractions.V2.CreateInvoiceParameters ) )]
[JsonSerializable( typeof( Abstractions.V2.Invoice[] ) )]
[JsonSerializable( typeof( Abstractions.V2.Project ) )]
[JsonSerializable( typeof( Abstractions.V2.ProjectLite[] ) )]
[JsonSerializable( typeof( Abstractions.V3.TimeEntry[] ) )]
[JsonSourceGenerationOptions( Converters = [ typeof( DateOnlyConverter ) ] )]
public sealed partial class TMetricJsonContext : JsonSerializerContext;