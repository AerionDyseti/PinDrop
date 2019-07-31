using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PinDrop.Models.DataModels;
using PinDrop.Models.ViewModels;

namespace PinDrop.Test
{
    public class ViewModelFactoryTests
    {
        private List<FrameDataModel> FrameDataModels { get; set; }
        private GameDataModel GameDataModel { get; set; }
        private PlayerDataModel PlayerDataModel { get; set; }

        [SetUp]
        public void Setup()
        {
            GameDataModel = MockGameData();
            FrameDataModels = MockFrameData(GameDataModel.Id);
            PlayerDataModel = MockPlayerData();
        }

        /// <summary>
        ///     Tests that the ViewModelFactory is creating a correct PlayerViewModel
        /// </summary>
        [Test]
        public void ViewModelFactory_CreatesCorrectPlayerView()
        {
            PlayerViewModel result = ViewModelFactory.CreatePlayerViewModel(PlayerDataModel);
            Assert.AreEqual(PlayerDataModel.Id, result.Id);
            Assert.AreEqual(PlayerDataModel.Name, result.Name);
        }

        /// <summary>
        ///     Tests that the ViewModelFactory is creating a correct GameViewModel.
        /// </summary>
        [Test]
        public void ViewModelFactory_CreatesCorrectGameView()
        {
            GameViewModel result = ViewModelFactory.CreateGameViewModel(GameDataModel);
            Assert.AreEqual(GameDataModel.Id, result.Id);
            Assert.AreEqual(GameDataModel.CreationDate, result.GameStart);
            Assert.AreEqual(GameDataModel.Frames.Last().FrameNumber, result.CurrentFrame);
            Assert.AreEqual(GameDataModel.CurrentThrow, result.CurrentThrow);
            Assert.AreEqual(result.Frames.Sum(f => f.Score), result.TotalScore);
        }

        /// <summary>
        ///     Tests that the ViewModelFactory is creating the correct list of strings
        ///     to represent the throws within a given set of frames.
        /// </summary>
        [Test]
        public void ViewModelFactory_CreatesCorrectFrameStrings()
        {
            List<FrameViewModel> results = ViewModelFactory.CreateFrameViewModels(FrameDataModels).ToList();

            Assert.AreEqual(new List<char> {'5', '/'}, results.First().Throws);
            Assert.AreEqual(new List<char> {'X'}, results.ElementAt(1).Throws);
            Assert.AreEqual(new List<char> {'1', '8'}, results.ElementAt(2).Throws);
            Assert.AreEqual(new List<char> {'X'}, results.ElementAt(3).Throws);
            Assert.AreEqual(new List<char> {'X'}, results.ElementAt(4).Throws);
            Assert.AreEqual(new List<char> {'6'}, results.ElementAt(5).Throws);
        }

        /// <summary>
        ///     Tests that the ViewModelFactory is creating the correct scores
        ///     for a given set of frames.
        /// </summary>
        [Test]
        public void ViewModelFactory_CreatesCorrectFrameScores()
        {
            List<FrameViewModel> results = ViewModelFactory.CreateFrameViewModels(FrameDataModels).ToList();
            Assert.AreEqual(20, results.First().Score);
            Assert.AreEqual(19, results.ElementAt(1).Score);
            Assert.AreEqual(9, results.ElementAt(2).Score);
            Assert.AreEqual(26, results.ElementAt(3).Score);
            Assert.AreEqual(null, results.ElementAt(4).Score);
            Assert.AreEqual(null, results.ElementAt(5).Score);
        }

        /// <summary>
        ///     Tests that the ViewModelFactory is ordering the frames within a given set correctly.
        /// </summary>
        [Test]
        public void ViewModelFactory_OrdersFramesCorrectly()
        {
            List<FrameViewModel> results = ViewModelFactory.CreateFrameViewModels(FrameDataModels).ToList();
            Assert.AreEqual(1, results.First().FrameNumber);
            Assert.AreEqual(2, results.ElementAt(1).FrameNumber);
            Assert.AreEqual(3, results.ElementAt(2).FrameNumber);
            Assert.AreEqual(4, results.ElementAt(3).FrameNumber);
            Assert.AreEqual(5, results.ElementAt(4).FrameNumber);
            Assert.AreEqual(6, results.ElementAt(5).FrameNumber);
        }


        /// <summary>
        ///     Generates in-memory mock data for FrameDataModels.
        /// </summary>
        /// <returns></returns>
        private List<FrameDataModel> MockFrameData(Guid gameId)
        {
            FrameDataModel Frame1 = new FrameDataModel
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UnixEpoch,
                GameId = gameId,
                FrameNumber = 1,
                FirstThrow = 5,
                SecondThrow = 5,
                IsSpare = true,
                IsFinished = true
            };

            FrameDataModel Frame2 = new FrameDataModel
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UnixEpoch.AddMinutes(1),
                GameId = gameId,
                FrameNumber = 2,
                FirstThrow = 10,
                IsStrike = true,
                IsFinished = true
            };

            FrameDataModel Frame3 = new FrameDataModel
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UnixEpoch.AddMinutes(2),
                GameId = gameId,
                FrameNumber = 3,
                FirstThrow = 1,
                SecondThrow = 8,
                IsFinished = true
            };

            FrameDataModel Frame4 = new FrameDataModel
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UnixEpoch.AddMinutes(1),
                GameId = gameId,
                FrameNumber = 4,
                FirstThrow = 10,
                IsStrike = true,
                IsFinished = true
            };

            FrameDataModel Frame5 = new FrameDataModel
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UnixEpoch.AddMinutes(1),
                GameId = gameId,
                FrameNumber = 5,
                FirstThrow = 10,
                IsStrike = true,
                IsFinished = true
            };

            FrameDataModel Frame6 = new FrameDataModel
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UnixEpoch.AddMinutes(2),
                GameId = gameId,
                FrameNumber = 6,
                FirstThrow = 6
            };

            return new List<FrameDataModel> {Frame1, Frame2, Frame3, Frame4, Frame5, Frame6};
        }

        /// <summary>
        ///     Generates in-memory mock data for GameDataModels.
        /// </summary>
        /// <returns></returns>
        private GameDataModel MockGameData()
        {
            GameDataModel mock = new GameDataModel
            {
                Id = Guid.NewGuid(),
                Player = MockPlayerData(),
                CreationDate = DateTime.UnixEpoch
            };

            mock.Frames = MockFrameData(mock.Id);
            mock.CurrentFrameId = mock.Frames.Last().Id;
            mock.CurrentThrow = 1;

            return mock;
        }

        /// <summary>
        ///     Generates in-memory mock data for PlayerDataModels.
        /// </summary>
        /// <returns></returns>
        private PlayerDataModel MockPlayerData()
        {
            return new PlayerDataModel
            {
                Id = Guid.Empty,
                Name = "Player 1",
                CreationDate = DateTime.UnixEpoch
            };
        }
    }
}