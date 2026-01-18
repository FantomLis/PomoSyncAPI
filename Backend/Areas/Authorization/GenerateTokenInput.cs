using System.ComponentModel.DataAnnotations;

namespace PomoSyncAPI.Backend.Areas.Authorization;

/// <summary>
/// JSON-object for using GenerateToken
/// </summary>
public class GenerateTokenInput
{
    /// <summary>
    /// User's password. 
    /// </summary>
    [Required] 
    [MaxLength(64)]
    public string Password { get; set; }
}