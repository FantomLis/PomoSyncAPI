using System.ComponentModel.DataAnnotations;

namespace PomoSyncAPI.Backend.Areas.Authorization;

public class GenerateTokenInput
{
    [Required] 
    [MaxLength(64)]
    public string Password { get; set; }
}