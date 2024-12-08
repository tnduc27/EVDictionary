using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Services
{
    public interface IWordTypeService
    {
        List<WordType> GetWordTypes();
    }
}
