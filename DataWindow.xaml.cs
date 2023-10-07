using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Data_Acess;
using Data_Acess.Entities;
using MarcinJunka_BlackJack.DTOs;

namespace MarcinJunka_BlackJack
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        public IAppDbContext _dbContext;
        public DataWindow(IAppDbContext dbContext)
        {
            _dbContext = dbContext;

            InitializeComponent();
            this.dataGridGames.ItemsSource = GetAllData();
        }
        private List<GameResolutionDTO> GetAllData()
        {
            var items = _dbContext.GameResolutions.Select(x =>
                new GameResolutionDTO()
                {
                    Id = x.Id,
                    Date = x.Date,
                    GameId = x.GameId,
                    PlacedBetId = x.PlacedBetId,
                    PlayerSaldo = x.PlayerSaldo,
                    WinMessage = x.WinMessage
                }).ToList();

            return items;
        }

        private List<GameResolution> GetDataByGameId(int id)
        {
            var gameList = _dbContext.GameResolutions.Where(gr => gr.GameId == id).ToList();
            return gameList;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            bool searchText = int.TryParse(txtSearchByGameId.Text,out int searchId);
            this.dataGridGames.ItemsSource = GetDataByGameId(searchId);
        }

        private void btnShowAllData_Click(object sender, RoutedEventArgs e)
        {
            this.dataGridGames.ItemsSource = GetAllData();
        }

        private void btnDeleteAllData_Click(object sender, RoutedEventArgs e)
        {
            var allItems = _dbContext.GameResolutions;

            _dbContext.GameResolutions.RemoveRange(allItems);
            _dbContext.SaveChanges();
            dataGridGames.ItemsSource = GetAllData();
        }
    }
}
