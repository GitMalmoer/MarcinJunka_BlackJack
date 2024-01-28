using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcinJunka_BlackJack.DTOs
{
    class GameResolutionDTO
    {
        public int Id { get; set; }
        public string WinMessage { get; set; }
        public int PlayerSaldo { get; set; }
        public DateTime Date { get; set; }
        public int GameId { get; set; }
        public int PlacedBetId { get; set; }
    }
}
