using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PinDrop.Models.DataModels;

namespace PinDrop.Models.ViewModels
{
    public class ViewModelFactory
    {
        public static GameViewModel CreateGameViewModel(Guid gameId, DbContext context)
        {
            


            return new GameViewModel();
        }
    }
}
