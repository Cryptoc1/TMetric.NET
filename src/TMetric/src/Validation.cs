using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace TMetric;

internal static class Validation
{
    public static void ThrowIfInvalid<[DynamicallyAccessedMembers( DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.PublicProperties )] T>( T argument, [CallerArgumentExpression( "argument" )] string? paramName = default )
    {
        ArgumentNullException.ThrowIfNull( argument, paramName );

        try
        {
#pragma warning disable IL2026
            Validator.ValidateObject( argument, new( argument ) );
#pragma warning restore IL2026
        }
        catch( ValidationException exception )
        {
            throw new ArgumentException( "The argument is not valid.", paramName, exception );
        }
    }
}