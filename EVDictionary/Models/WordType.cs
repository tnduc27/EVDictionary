using System;
using System.Collections.Generic;

namespace EVDictionary.Models;

public partial class WordType
{
    public int WordTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
