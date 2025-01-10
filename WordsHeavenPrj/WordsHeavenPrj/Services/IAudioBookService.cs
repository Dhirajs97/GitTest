using System.Collections.Generic;
using System.Threading.Tasks;
using WordsHeavenPrj.Models;

namespace WordsHeavenPrj.Services
{
    public interface IAudioBookService
    {
        Task<AudioBook> AddAudioBookAsync(AudioBookDto audioBookDto); 
        Task<AudioBook> GetAudioBookByIdAsync(int id); 
        Task<IEnumerable<AudioBook>> GetAllAudioBooksAsync();
        Task<AudioBook> UpdateAudioBookAsync(int id, AudioBookDto audioBookDto); 
        Task<bool> DeleteAudioBookAsync(int id);
    }
}
