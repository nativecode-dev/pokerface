import { IPlayerModel } from './PlayerModel';
import { ICompletedRoundModel } from './CompletedRoundModel';

/**
 *  @external PokerFace.Models.Poker.CompletedGameModel
 */
export interface ICompletedGameModel {
    /**
     *  Players
     *  @return {PlayerModel[]}
     *  @external PokerFace.Models.Poker.CompletedGameModel.Players
     */
    players?: IPlayerModel[];

    /**
     *  Rounds
     *  @return {CompletedRoundModel[]}
     *  @external PokerFace.Models.Poker.CompletedGameModel.Rounds
     */
    rounds?: ICompletedRoundModel[];
}