using System;
using System.Collections.Generic;

namespace EVDictionary.Models;

public partial class Definition
{
    public int DefinitionId { get; set; }

    public int? WordId { get; set; }

    public string DefinitionText { get; set; } = null!;

    public string? ExampleEnglish { get; set; }

    public string? ExampleVietnamese { get; set; }

    public virtual Word? Word { get; set; }
}
