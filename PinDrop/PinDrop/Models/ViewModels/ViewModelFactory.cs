using System;
using System.Collections.Generic;
using System.Linq;
using PinDrop.Models.DataModels;

namespace PinDrop.Models.ViewModels
{
    /// <summary>
    ///     Factory to create a view-model from the back-end data models.
    /// </summary>
    public static class ViewModelFactory
    {
        /// <summary>
        ///     Creates a GameViewModel using GameData.
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        public static GameViewModel CreateGameViewModel(GameDataModel gameData)
        {
            PlayerViewModel playerViewModel = CreatePlayerViewModel(gameData.Player);
            List<FrameViewModel> frameViewModels = CreateFrameViewModels(gameData.Frames).ToList();

            GameViewModel vm = new GameViewModel
            {
                Id = gameData.Id,
                GameStart = gameData.CreationDate,
                CurrentFrame = gameData.Frames.SingleOrDefault(f => f.Id == gameData.CurrentFrameId)?.FrameNumber,
                CurrentThrow = gameData.CurrentThrow,
                Player = playerViewModel,
                Frames = frameViewModels,
                TotalScore = frameViewModels.Sum(f => f.Score) ?? 0
            };

            return vm;
        }

        /// <summary>
        ///     Creates a new PlayerViewModel using the PlayerDataModel.
        /// </summary>
        /// <param name="playerData">The player data to construct a view model from.</param>
        /// <returns>Fully constructed player view model.</returns>
        public static PlayerViewModel CreatePlayerViewModel(PlayerDataModel playerData)
        {
            return new PlayerViewModel
            {
                Id = playerData.Id,
                Name = playerData.Name
            };
        }

        /// <summary>
        ///     Uses an enumerable list of frame data to create a list of FrameViewModels
        /// </summary>
        /// <param name="framesData"></param>
        /// <returns></returns>
        public static IEnumerable<FrameViewModel> CreateFrameViewModels(IEnumerable<FrameDataModel> framesData)
        {
            // Prevent duplicate enumeration.
            List<FrameDataModel> frameDataModels = framesData.ToList();
            List<FrameViewModel> output = new List<FrameViewModel>();

            for (Int32 i = 0; i < frameDataModels.Count; i++)
            {
                FrameDataModel thisFrame = frameDataModels.ElementAt(i);
                FrameDataModel nextFrame = frameDataModels.ElementAtOrDefault(i + 1);
                FrameDataModel nextNextFrame = frameDataModels.ElementAtOrDefault(i + 2);

                FrameViewModel newView = new FrameViewModel
                {
                    Throws = GetThrowCharacters(thisFrame),
                    FrameNumber = frameDataModels.IndexOf(thisFrame) + 1
                };

                if (CanScore(thisFrame, nextFrame, nextNextFrame))
                {
                    Int32 score = (thisFrame.FirstThrow ?? 0) + (thisFrame.SecondThrow ?? 0) +
                                  (thisFrame.ThirdThrow ?? 0);

                    // If this is a spare, add the first throw of next frame.
                    // Bonus points only count for non-final frames.
                    if (thisFrame.FrameNumber < 10 && thisFrame.IsSpare) score += nextFrame?.FirstThrow ?? 0;

                    // If this is a strike...
                    if (thisFrame.FrameNumber < 10 && thisFrame.IsStrike)
                    {
                        // For frame 1-8, next frame being strikes means add 10 to next-next's first throw.
                        if (thisFrame.FrameNumber < 9 && nextFrame != null && nextFrame.IsStrike)
                            score += 10 + (nextNextFrame?.FirstThrow ?? 0);

                        // Otherwise, use next's first and second throw.
                        else
                            score += (nextFrame?.FirstThrow ?? 0) + (nextFrame?.SecondThrow ?? 0);
                    }

                    newView.Score = score;
                }

                output.Add(newView);
            }

            return output;
        }


        /// <summary>
        ///     Determine whether or not this Frame (based on the next two frames) can be scored.
        /// </summary>
        /// <param name="thisFrame"></param>
        /// <param name="nextFrame"></param>
        /// <param name="nextNextFrame"></param>
        /// <returns></returns>
        private static bool CanScore(FrameDataModel thisFrame, FrameDataModel nextFrame, FrameDataModel nextNextFrame)
        {
            // If we are looking at a spare, we need the next Frame to have a value in order to score.
            if (thisFrame.IsSpare)
            {
                // Frame 10 will only depend on this frame's third throw.
                if (thisFrame.FrameNumber == 10) return thisFrame?.ThirdThrow != null;

                // Otherwise, we check for next frame's first throw.
                return nextFrame?.FirstThrow != null;
            }

            // If we are looking at a strike...
            if (thisFrame.IsStrike)
            {
                // Frame 10 will only ever be dependent on this frame.
                if (thisFrame.FrameNumber == 10) return thisFrame?.SecondThrow != null && thisFrame?.ThirdThrow != null;

                // Frame 9 will only ever be dependent on next frame.
                if (thisFrame.FrameNumber == 9) return nextFrame?.FirstThrow != null && nextFrame.SecondThrow != null;

                // If next frame is a strike, check if next-next has a value.
                if (nextFrame != null && nextFrame.IsStrike) return nextNextFrame?.FirstThrow != null;

                // Otherwise check that nextFrame has a Second Throw.
                return nextFrame?.SecondThrow != null;
            }

            // Otherwise, we can score when the frame is finished.
            return thisFrame.IsFinished;
        }


        /// <summary>
        ///     Gets the list of throw characters for this specific Frame.
        /// </summary>
        /// <param name="f">The frame to generate characters for.</param>
        /// <returns></returns>
        private static List<Char> GetThrowCharacters(FrameDataModel f)
        {
            List<char> throwCharacters = new List<Char>();

            if (f.FirstThrow.HasValue) throwCharacters.Add(f.FirstThrow == 10 ? 'X' : f.FirstThrow.ToString()[0]);

            if (f.SecondThrow.HasValue)
            {
                if (f.SecondThrow == 10)
                    throwCharacters.Add('X');
                else if (f.FirstThrow + f.SecondThrow == 10)
                    throwCharacters.Add('/');
                else
                    throwCharacters.Add(f.SecondThrow.ToString()[0]);
                ;
            }

            if (f.ThirdThrow.HasValue)
            {
                if (f.ThirdThrow == 10)
                    throwCharacters.Add('X');
                // If we weren't already a Spare, then mark Spare.
                else if (f.FirstThrow + f.SecondThrow != 10 && f.ThirdThrow + f.SecondThrow == 10)
                    throwCharacters.Add('/');
                else
                    throwCharacters.Add(f.SecondThrow.ToString()[0]);
                ;
            }

            return throwCharacters;
        }
    }
}