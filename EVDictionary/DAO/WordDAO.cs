using EVDictionary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDictionary.DAO
{
    public class WordDAO
    {
        public static List<Word> SearchWords(string searchTerm)
        {
            using var db = new EvdictionaryContext();

            return db.Words
                .Include(w => w.WordType)       // Include word type details
                .Include(w => w.Definitions)    // Include definitions
                .Where(w => w.WordText.ToLower().Contains( searchTerm.ToLower())) // Exact match, case-insensitive
                .ToList();
        }
        public static List<Word> GetWords()
        {
            var words = new List<Word>();
            try
            {
                using var db = new EvdictionaryContext();
                // Bao gồm WordType và Definitions khi lấy dữ liệu Word
                words = db.Words
                    .Include(w => w.WordType)        // Bao gồm thông tin loại từ
                    .Include(w => w.Definitions)     // Bao gồm danh sách định nghĩa
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return words;
        }
        public static void AddWords(Word w)
        {
            try
            {
                using var db = new EvdictionaryContext();

                // Kiểm tra nếu WordText đã tồn tại trong cơ sở dữ liệu
                var existingWord = db.Words.FirstOrDefault(word => word.WordText.ToLower() == w.WordText.ToLower());

               

                // Nếu không tồn tại, thêm từ mới
                db.Words.Add(w);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateWords(Word w)
        {
            try
            {
                using var db = new EvdictionaryContext();

                var existingWord = db.Words
                    .Include(word => word.Definitions) // Ensure that Definitions are included
                    .FirstOrDefault(word => word.WordId == w.WordId);

                if (existingWord != null)
                {
                    // Update properties of the existing word
                    existingWord.WordText = w.WordText;
                    existingWord.WordTypeId = w.WordTypeId;

                    // Clear existing definitions and add new ones
                    existingWord.Definitions.Clear();
                    foreach (var definition in w.Definitions)
                    {
                        existingWord.Definitions.Add(definition); // Manually adding each definition
                    }

                    db.SaveChanges(); // Save the changes
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteWords(int wordId)
        {
            try
            {
                using var db = new EvdictionaryContext();

                // Find the word by ID
                var wordToDelete = db.Words
                    .Include(w => w.Definitions) // Include definitions if needed
                    .FirstOrDefault(w => w.WordId == wordId);

                if (wordToDelete != null)
                {
                    // Remove associated definitions if necessary
                    db.Definitions.RemoveRange(wordToDelete.Definitions);

                    // Remove the word itself
                    db.Words.Remove(wordToDelete);

                    db.SaveChanges(); // Commit changes to the database
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
