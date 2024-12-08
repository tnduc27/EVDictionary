using EVDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Services
{
    public interface IWordService
    {
        List<Word> SearchWords(string searchTerm);
        List<Word> GetWords();
        void AddWords(Word w);
        void UpdateWords(Word w);
        void DeleteWords(int wordId);
    }
}
