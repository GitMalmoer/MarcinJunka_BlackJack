using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class PlacedBet
    {
        [Key]
        public int Id { get; set; }
        public int BetAmount { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        public int GameId { get; set; }

    }
}
