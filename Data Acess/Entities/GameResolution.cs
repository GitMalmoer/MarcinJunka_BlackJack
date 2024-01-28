using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data_Access.Entities;

namespace Data_Acess.Entities
{
    /// <summary>
    /// this class carries information about the result of a single game.
    /// </summary>
    public class GameResolution
    {
        [Key]
        public int Id { get; set; }
        public string WinMessage { get; set; }
        public int PlayerSaldo { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }
        [Required]
        public int GameId { get; set; }

        [ForeignKey("PlacedBetId")]
        public PlacedBet PlacedBet { get; set; }
        public int PlacedBetId { get; set; }
    }
}
