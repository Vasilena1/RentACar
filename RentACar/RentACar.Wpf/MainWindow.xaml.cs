using System.Windows;
using Microsoft.EntityFrameworkCore;
using RentACar.Model.Data;
using RentACar.Model.UnitOfWork;
using RentACar.Model.Interfaces;
using RentACar.ViewModel.ViewModels;
using RentACar.Model.Entities;

namespace RentACar.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            var optionsBuilder = new DbContextOptionsBuilder<RentACarDbContext>();
            optionsBuilder.UseSqlite("Data Source=rentacar.db");

            var context = new RentACarDbContext(optionsBuilder.Options);

            context.Database.EnsureCreated();

            _unitOfWork = new UnitOfWork(context);
            _mainViewModel = new MainViewModel(_unitOfWork);
            DataContext = _mainViewModel;

            Loaded += async (_, __) =>
            {
                await _mainViewModel.CarPage.Search.SearchAsync();
                await _mainViewModel.CustomerPage.LoadAsync();
            };
        }
    }
}
