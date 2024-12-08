using EVDictionary.Models;
using EVDictionary.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.Repositories
{
    public class WordRepository : IWordRepository
    {
        public List<Word> SearchWords(string searchTerm) => WordDAO.SearchWords(searchTerm);
        public List<Word> GetWords() => WordDAO.GetWords();
        public void AddWords(Word w) => WordDAO.AddWords(w);
        public void UpdateWords(Word w) => WordDAO.UpdateWords(w);
        public void DeleteWords(int wordId) => WordDAO.DeleteWords(wordId);
    }
}
