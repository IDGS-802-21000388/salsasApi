using System;
using System.Collections.Generic;

namespace SalsasAPI.Models;

public partial class LogsUser
{
    public int Id { get; set; }

    public string Procedimiento { get; set; } = null!;

    public DateTime? LastDate { get; set; }

    public int? IdUsuario { get; set; }
}
