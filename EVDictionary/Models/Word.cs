using System;
using System.Collections.Generic;

namespace EVDictionary.Models;

public partial class Word
{
    public int WordId { get; set; }

    public string WordText { get; set; } = null!;

    public int? WordTypeId { get; set; }

    public virtual ICollection<Definition> Definitions { get; set; } = new List<Definition>();

    public virtual WordType? WordType { get; set; }
}
